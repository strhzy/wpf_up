using System.Windows;
using WPF_UP.Models;
using WPF_UP.Pages;
using WPF_UP.Support;

namespace WPF_UP.View;

public partial class MainView : Window
{
    public MainView(EmployeeAccount employeeAccount)
    {
        InitializeComponent();
        _employeeAccount = employeeAccount;
        ShowNeededButtons(employeeAccount.RoleId);
    }
    EmployeeAccount _employeeAccount;

    private void OrdersButton_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new OrdersPage();
        MainButton.Visibility = Visibility.Visible;
        HideAllButtons();
    }

    private void MainButton_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = null;
        MainButton.Visibility = Visibility.Collapsed;
        ShowNeededButtons(_employeeAccount.RoleId);
    }

    private void HideAllButtons()
    {
        ordersButton.Visibility = Visibility.Collapsed;
        clientsButton.Visibility = Visibility.Collapsed;
        storeButton.Visibility = Visibility.Collapsed;
        scheduleButton.Visibility = Visibility.Collapsed;
        reportsButton.Visibility = Visibility.Collapsed;
        serviceButton.Visibility = Visibility.Collapsed;
        employeeButton.Visibility = Visibility.Collapsed;
    }

    private void ShowNeededButtons(int role)
    {
        switch (role)
        {
            case 1:
                ordersButton.Visibility = Visibility.Visible;
                clientsButton.Visibility = Visibility.Visible;
                storeButton.Visibility = Visibility.Visible;
                break;
            case 2:
                ordersButton.Visibility = Visibility.Visible;
                clientsButton.Visibility = Visibility.Visible;
                scheduleButton.Visibility = Visibility.Visible;
                break;
            case 3:
                ordersButton.Visibility = Visibility.Visible;
                reportsButton.Visibility = Visibility.Visible;
                break;
            case 4:
                employeeButton.Visibility = Visibility.Visible;
                serviceButton.Visibility = Visibility.Visible;
                scheduleButton.Visibility = Visibility.Visible;
                break;
            case 5:
                OrdersMasterPage pg = new OrdersMasterPage();
                MainFrame.Content = pg;
                break;
        }
    }

    private void StoreButton_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new StorePage();
        MainButton.Visibility = Visibility.Visible;
        HideAllButtons();
    }

    private void ClientsButton_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new ClientsPage();
        MainButton.Visibility = Visibility.Visible;
        HideAllButtons();
    }

    private void ReportsButton_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new ReportPage();
        MainButton.Visibility = Visibility.Visible;
        HideAllButtons();
    }

    private void ServiceButton_OnClick(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new ServiceStationPage();
        MainButton.Visibility = Visibility.Visible;
        HideAllButtons();
    }
}