using System.Windows;
using System.Windows.Controls;
using WPF_UP.Models;
using WPF_UP.Support;

namespace WPF_UP.Pages;

public partial class OrdersMasterPage : Page
{
    public OrdersMasterPage()
    {
        InitializeComponent();
        DataContext = this;
        DataGrib.ItemsSource = GetOrders();

    }

    public List<OrderMasterDisplay> GetOrders()
    {
        var clients = ApiHelper.Get<List<Client>>("client");

        var employees = ApiHelper.Get<List<Employee>>("employee");

        var orders = ApiHelper.Get<List<Order>>("order");
        
        var servicestations = ApiHelper.Get<List<ServiceStation>>("servicestation");
        
        var statuses = ApiHelper.Get<List<Status>>("status");

        var orderDisplays = orders.Select(order => new OrderMasterDisplay
        {
            OrderId = order.Id,
            ClientInfo = $"{clients.First(c => c.Id == order.ClientId).Surname} {clients.First(c => c.Id == order.ClientId).ClientName}",
            DateReference = order.DateReference,
            RepairDate = order.RepairDate,
            StatusName = $"{statuses.First(s => s.Id == order.StatusId).StatusName}",
            Price = order.Price,
            EmployeeInfo = $"{employees.First(e => e.Id == order.EmployeeId).Surname} {employees.First(e => e.Id == order.EmployeeId).EmployeeName}",
            WorkName = order.Description,
            ServiceStationAddress = servicestations.First(s => s.Id == employees.First(e => e.Id == order.EmployeeId).ServiceStationId).Address 
        }).ToList();

        return orderDisplays;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        bool sel = DataGrib.SelectedItem != null;
        if (sel)
        {
            Order order = ApiHelper.Get<Order>("order", (DataGrib.SelectedItem as OrderMasterDisplay).OrderId);
            order.StatusId = 3;
            if (ApiHelper.Put<Order>(Serdeser.Serialize(order), "order", order.Id))
            {
                MessageBox.Show("Данные обновлены");
            }
            else{MessageBox.Show("Ошибка");}
            DataGrib.ItemsSource = GetOrders();
        }
        else
        {
            MessageBox.Show("Элемент не выбран");
        }
    }
}

public partial class OrderMasterDisplay
{
    public long OrderId { get; set; }
    public string ClientInfo { get; set; }
    public DateTime DateReference { get; set; }
    public DateTime? RepairDate { get; set; }
    public decimal Price { get; set; }
    public string StatusName { get; set; }
    public string EmployeeInfo { get; set; }
    public string WorkName { get; set; }
    public string ServiceStationAddress { get; set; } // Новое поле для адреса станции
}