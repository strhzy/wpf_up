using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Security.Cryptography;
using System.Text;
using WPF_UP.Models;
using WPF_UP.Support;

namespace WPF_UP.View;

public partial class AuthView : Window
{
    public AuthView()
    {
        InitializeComponent();
    }
    
    private void Authentificate(object sender, RoutedEventArgs e)
    {
        List<EmployeeAccount> accounts = ApiHelper.Get<List<EmployeeAccount>>("employeeaccount");
        string hashedPassword = ComputeSha256Hash(password.Password);
        EmployeeAccount realaccount = (from acc in accounts
            where acc.Login == login.Text && acc.Password == hashedPassword
            select acc).FirstOrDefault();
        if (realaccount!=null)
        {
            
            MainView mainView = new MainView(realaccount);
            mainView.Show();
            this.Close();
        }
        else
        {
            MessageBox.Show("Ошибка входа " + password.Password);
        }
    }
    
    static string ComputeSha256Hash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}