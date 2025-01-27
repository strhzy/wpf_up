using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Drawing.Printing;
using Microsoft.WindowsAPICodePack.Dialogs;
using WPF_UP.Support;

namespace WPF_UP.Pages;

public partial class ReportPage : Page
{
    public ReportPage()
    {
        InitializeComponent();
    }

    private void ExportBtn_OnClick(object sender, RoutedEventArgs e)
    {
        string file;
        string model;
        CommonOpenFileDialog fileDialog = new CommonOpenFileDialog() {IsFolderPicker = true};
        byte[] excelBytes;
        switch (typeBox.SelectedIndex)
        {
            case 0:
                model = $"reports/income/excel?startDate={fromDate.Date:yyyy-MM-dd}&endDate={toDate.Date:yyyy-MM-dd}";
                
                file = "\\Income.xlsx";
               excelBytes = ApiHelper.GetFile(model);
                File.WriteAllBytes(file, excelBytes);
                try
                {
                    // Открываем файл в приложении по умолчанию
                    Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });

                    Console.WriteLine("Файл успешно открыт.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
                }
                break;
            case 1:
                model = "reports/spareparts/stock/excel";
                file = "\\Sparepart.xlsx";
                excelBytes = ApiHelper.GetFile(model);
                File.WriteAllBytes(file, excelBytes);
                try
                {
                    // Открываем файл в приложении по умолчанию
                    Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });

                    Console.WriteLine("Файл успешно открыт.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
                }
                break;
            case 2:
                model = "reports/operations/popular/excel";
                file = "\\popularOper.xlsx";
                excelBytes = ApiHelper.GetFile(model);
                File.WriteAllBytes(file, excelBytes);
                try
                {
                    // Открываем файл в приложении по умолчанию
                    Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });

                    Console.WriteLine("Файл успешно открыт.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
                }
                break;
            case 3:
                model = "reports/spareparts/popular/excel";
                file = "\\popelarParts.xlsx";
                excelBytes = ApiHelper.GetFile(model);
                File.WriteAllBytes(file, excelBytes);
                try
                {
                    // Открываем файл в приложении по умолчанию
                    Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });

                    Console.WriteLine("Файл успешно открыт.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
                }
                break;
        }
    }

    private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
    {
        string file;
        string model;
        byte[] excelBytes;

        try
        {
            switch (typeBox.SelectedIndex)
            {
                case 0:
                    model = $"reports/income/excel?startDate={fromDate.Date:yyyy-MM-dd}&endDate={toDate.Date:yyyy-MM-dd}";
                    file = "Income.xlsx";
                    break;
                case 1:
                    model = "reports/spareparts/stock/excel";
                    file = "Sparepart.xlsx";
                    break;
                case 2:
                    model = "reports/operations/popular/excel";
                    file = "popularOper.xlsx";
                    break;
                case 3:
                    model = "reports/spareparts/popular/excel";
                    file = "popelarParts.xlsx";
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    return;
            }

            // Получение данных файла и запись на диск
            excelBytes = ApiHelper.GetFile(model);
            File.WriteAllBytes(file, excelBytes);

            // Печать файла
            Process printProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = file,
                    Verb = "print",
                    CreateNoWindow = true,
                    UseShellExecute = true
                }
            };

            printProcess.Start();

            Console.WriteLine("Файл успешно отправлен на печать.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}