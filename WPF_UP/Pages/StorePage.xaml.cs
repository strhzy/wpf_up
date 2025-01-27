using System.Windows;
using System.Windows.Controls;
using WPF_UP.Models;
using WPF_UP.Support;

namespace WPF_UP.Pages;

public partial class StorePage : Page
{
    public StorePage()
    {
        InitializeComponent();
        DataContext = this;
        updateData();
    }
    public List<SparePart> spareParts { get; set; } = new List<SparePart>();

    private void DataGrib_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (StoreGrid.SelectedItem is SparePart selectedPart)
        {
            partNameBox.Text = selectedPart.PartName;
            articulBox.Text = selectedPart.Articul;
            quantityBox.Text = selectedPart.Quantity.ToString();
            priceBox.Text = selectedPart.Price.ToString();
        }
        else
        {
            // Если выделения нет, очищаем текстовые поля (опционально)
            partNameBox.Text = string.Empty;
            articulBox.Text = string.Empty;
            quantityBox.Text = string.Empty;
            priceBox.Text = string.Empty;
        }
    }

    private void AddPartBtn_OnClick(object sender, RoutedEventArgs e)
    {
        SparePart sparePart = new SparePart();
        sparePart.PartName = partNameBox.Text;
        sparePart.Articul = articulBox.Text;
        sparePart.Quantity = int.Parse(quantityBox.Text);
        sparePart.Price = decimal.Parse(priceBox.Text);
        if (spareParts.FirstOrDefault(sp => sp.Articul == articulBox.Text) != null)
        {
            MessageBox.Show("Данный артикул занят");
            articulBox.Text = "";
        }
        else if(sparePart!=null)
        {
            ApiHelper.Post<SparePart>(Serdeser.Serialize(sparePart), "sparepart");
            updateData();
        }
    }

    private void DelPartBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (StoreGrid.SelectedItem != null)
        {
            ApiHelper.Delete<SparePart>("sparepart", (StoreGrid.SelectedItem as SparePart).Id);
            updateData();
        }
        else
        {
            MessageBox.Show("Не выбран элемент");
        }
    }

    private void EditPartBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (StoreGrid.SelectedItem != null)
        {
            var selected = StoreGrid.SelectedItem as SparePart;
            SparePart sparePart = new SparePart();
            sparePart.PartName = partNameBox.Text;
            sparePart.Articul = articulBox.Text;
            sparePart.Quantity = int.Parse(quantityBox.Text);
            sparePart.Price = decimal.Parse(priceBox.Text);
            if(sparePart!=null)
            {
                bool check = ApiHelper.Put<SparePart>(Serdeser.Serialize(sparePart), "sparepart", selected.Id);
                if (!check)
                {
                    MessageBox.Show("Ошибка добавления");
                    
                }
                else
                {
                    updateData();
                }
                
            }
        }
        else
        {
            MessageBox.Show("Не выбран элемент");
        }
        
    }

    private void updateData()
    {
        spareParts = ApiHelper.Get<List<SparePart>>("sparepart");
    }
}