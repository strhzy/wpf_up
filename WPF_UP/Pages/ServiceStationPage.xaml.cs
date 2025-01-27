using System.Windows;
using System.Windows.Controls;
using WPF_UP.Models;
using WPF_UP.Support;

namespace WPF_UP.Pages;

public partial class ServiceStationPage : Page
{
    public ServiceStationPage()
    {
        InitializeComponent();
        DataContext = this;
        updateData();
    }
    
    public List<ServiceStation> ServiceStations { get; set; } = new List<ServiceStation>();

    private void updateData()
    {
        ServiceStations = ApiHelper.Get<List<ServiceStation>>("servicestation");
    }

    private void AddSTBtn_OnClick(object sender, RoutedEventArgs e)
    {
        ServiceStation serviceStation = new ServiceStation();
        serviceStation.Address = AddressBox.Text;
        serviceStation.TelephoneNumber = PhoneBox.Text;
        serviceStation.Email = EmailBox.Text;
        serviceStation.QuantityWorkPlaces = int.Parse(QuantityBox.Text);
        bool check = ApiHelper.Post<ServiceStation>(Serdeser.Serialize(serviceStation), "servicestation");
        if (check)
        {
            MessageBox.Show(check.ToString());
        }
        else
        {
            MessageBox.Show(check.ToString());
        }
        updateData();
    }

    private void EditSTBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (StationGrid.SelectedItem != null)
        {
            ServiceStation serviceStation = StationGrid.SelectedItem as ServiceStation;
            serviceStation.Address = AddressBox.Text;
            serviceStation.TelephoneNumber = PhoneBox.Text;
            serviceStation.Email = EmailBox.Text;
            serviceStation.QuantityWorkPlaces = int.Parse(QuantityBox.Text);
            bool check = ApiHelper.Put<ServiceStation>(Serdeser.Serialize(serviceStation), "servicestation", serviceStation.Id);
            if (check)
            {
                MessageBox.Show(check.ToString());
            }
            else
            {
                MessageBox.Show(check.ToString());
            }
            updateData();
        }
        else
        {
            MessageBox.Show("Элемент не выбран");
        }
    }

    private void DelSTBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (StationGrid.SelectedItem != null)
        {
            ServiceStation serviceStation = StationGrid.SelectedItem as ServiceStation;
            bool check = ApiHelper.Delete<ServiceStation>("servicestation", serviceStation.Id);
            updateData();
        }
    }

    private void StationGrid_OnSelectionChangedGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ServiceStation serviceStation = StationGrid.SelectedItem as ServiceStation;
        AddressBox.Text = serviceStation.Address;
        PhoneBox.Text = serviceStation.TelephoneNumber;
        EmailBox.Text = serviceStation.Email;
        QuantityBox.Text = serviceStation.QuantityWorkPlaces.ToString();
    }
}