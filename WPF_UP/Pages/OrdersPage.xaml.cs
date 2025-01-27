using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_UP.Models;
using WPF_UP.Support;

namespace WPF_UP.Pages;

public partial class OrdersPage : Page
{
    public OrdersPage()
    {
        InitializeComponent();InitializeComponent();
        DataContext = this;
        DataGrib.ItemsSource = GetOrders();
        Parts = ApiHelper.Get<List<SparePart>>("sparepart");
        
        PartsListView.ItemsSource = PartItems;
    }
    public List<SparePart> Parts { get; set; } = new List<SparePart>();
    public List<PartItem> PartItems { get; set; } = new List<PartItem>();

    public List<OrderDisplay> GetOrders()
    {
        var clients = ApiHelper.Get<List<Client>>("client");

        var employees = ApiHelper.Get<List<Employee>>("employee");

        var orders = ApiHelper.Get<List<Order>>("order");
        
        var servicestations = ApiHelper.Get<List<ServiceStation>>("servicestation");
        
        var statuses = ApiHelper.Get<List<Status>>("status");
        
        var works = ApiHelper.Get<List<Operation>>("operation");

        var orderDisplays = orders.Select(order => new OrderDisplay
        {
            OrderId = order.Id,
            ClientInfo = $"{clients.First(c => c.Id == order.ClientId).Surname} {clients.First(c => c.Id == order.ClientId).ClientName}",
            DateReference = order.DateReference,
            RepairDate = order.RepairDate,
            StatusName = $"{statuses.First(s => s.Id == order.StatusId).StatusName}",
            Price = order.Price,
            WorkName = $"{works.First((w=>w.Id == order.OperationId)).OperationName}",
            EmployeeInfo = $"{employees.First(e => e.Id == order.EmployeeId).Surname} {employees.First(e => e.Id == order.EmployeeId).EmployeeName}",
            ServiceStationAddress = servicestations.First(s => s.Id == employees.First(e => e.Id == order.EmployeeId).ServiceStationId).Address,
            Description = order.Description
        }).ToList();
        
        

        return orderDisplays;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        bool sel = DataGrib.SelectedItem != null;
        if (sel)
        {
            Order order = ApiHelper.Get<Order>("order", (DataGrib.SelectedItem as OrderDisplay).OrderId);
            order.StatusId = 3;
            if (ApiHelper.Put<Order>(Serdeser.Serialize(order), "order", order.Id))
            {
                MessageBox.Show("Данные обновлены");
            }
            else{ MessageBox.Show("Ошибка"); }
            DataGrib.ItemsSource = GetOrders();
        }
        else
        {
            MessageBox.Show("Элемент не выбран");
        }
    }

    private void DataGrib_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        DescriptBox.Visibility = Visibility.Visible;
        DescriptBox.Text = (DataGrib.SelectedItem as OrderDisplay).Description;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        PartItems.Add(new PartItem { Parts = Parts, SelectedPart = 0 });
        PartsListView.ItemsSource = PartItems;
    }
}

public partial class OrderDisplay
{
    public long OrderId { get; set; }
    public string ClientInfo { get; set; }
    public DateTime DateReference { get; set; }
    public DateTime? RepairDate { get; set; }
    public decimal Price { get; set; }
    public string StatusName { get; set; }
    public string EmployeeInfo { get; set; }
    public string WorkName { get; set; }
    public string ServiceStationAddress { get; set; }
    public string Description { get; set; }
}
public class PartItem
{
    public List<SparePart> Parts { get; set; }
    public int SelectedPart { get; set; }
}

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

    public void Execute(object parameter) => _execute(parameter);

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}