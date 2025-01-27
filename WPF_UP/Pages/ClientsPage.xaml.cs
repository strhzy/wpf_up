using System.Windows;
using System.Windows.Controls;
using WPF_UP.Models;
using WPF_UP.Support;

namespace WPF_UP.Pages;

public partial class ClientsPage : Page
{
    public ClientsPage()
    {
        InitializeComponent();
        DataContext = this;
        updateData();
    }
    
    public List<Client> Clients { get; set; } = new List<Client>();

    private void AddClientBtn_OnClick(object sender, RoutedEventArgs e)
    {
        Client client = new Client();
        string[] name = new string[3];
        name = ClientNameBox.Text.Split(" " , StringSplitOptions.RemoveEmptyEntries);
        client.Surname = name[0];
        client.ClientName = name[1];
        client.Patronymic = name[2];
        client.TelephoneNumber = PhoneBox.Text;
        client.CarBrand = CarBrandBox.Text;
        client.CarModel = CarModelBox.Text;
        client.GovNumber = GovNumBox.Text;
        client.LastVisitDate = null;
        bool check = ApiHelper.Post<Client>(Serdeser.Serialize(client), "client");
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

    private void EditClientBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (ClientsGrid.SelectedItem != null)
        {
            Client client = ClientsGrid.SelectedItem as Client;
            string[] name = new string[3];
            name = ClientNameBox.Text.Split(" " , StringSplitOptions.RemoveEmptyEntries);
            client.Surname = name[0];
            client.ClientName = name[1];
            client.Patronymic = name[2];
            client.TelephoneNumber = PhoneBox.Text;
            client.CarBrand = CarBrandBox.Text;
            client.CarModel = CarModelBox.Text;
            client.GovNumber = GovNumBox.Text;
            bool check = ApiHelper.Put<Client>(Serdeser.Serialize(client), "client", client.Id);
            updateData();
        }
        else
        {
            MessageBox.Show("Элемент не выбран");
        }
    }

    private void DelClientBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (ClientsGrid.SelectedItem!=null)
        {
            Client client = ClientsGrid.SelectedItem as Client;
            bool check = ApiHelper.Delete<Client>("client", client.Id);
            updateData();
        }
        else
        {
            MessageBox.Show("Элемент не выбран");
        }
    }

    private void ClientsGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ClientsGrid.SelectedItem is Client selectedClient)
        {
            ClientNameBox.Text = selectedClient.Surname + " " + selectedClient.ClientName + " " + selectedClient.Patronymic;
            PhoneBox.Text = selectedClient.TelephoneNumber;
            CarBrandBox.Text = selectedClient.CarBrand;
            CarModelBox.Text = selectedClient.CarModel;
            GovNumBox.Text = selectedClient.GovNumber;
        }
        else
        {
            ClientNameBox.Text = string.Empty;
            PhoneBox.Text = string.Empty;
            CarBrandBox.Text = string.Empty;
            CarModelBox.Text = string.Empty;
            GovNumBox.Text = string.Empty;
        }
    }
    
    private void updateData()
    {
        Clients = ApiHelper.Get<List<Client>>("client");
    }
}