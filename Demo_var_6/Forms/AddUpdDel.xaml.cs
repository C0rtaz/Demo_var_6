using Demo_var_6.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.Win32;
using static Demo_var_6.Classes.IDatabaseService;

namespace Demo_var_6.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddUpdDel.xaml
    /// </summary>
    public partial class AddUpdDel : Window
    {
        IDatabaseService.Product prod;

        public string imgPath = "";
        public AddUpdDel()
        {
            InitializeComponent();
        }

        private void Init() {
            if (IDatabaseService.AddUpd)
            {
                InitForAdd();
            }
            else {
                InitForAdd();
            }
        }

        private void InitForAdd() {
            System.Windows.Controls.Image? img = FindName("img") as System.Windows.Controls.Image;
            imgPath = IDatabaseService.defaultPath + IDatabaseService.defaultImage;
            img.Source = new BitmapImage(new Uri(imgPath));
            prod = new IDatabaseService.Product();
        }

        private void InitForUPd() {
            (FindName("IDBox") as TextBox).Text = IDatabaseService.updProduct.id;
            (FindName("nameBox") as TextBox).Text = IDatabaseService.updProduct.name;
            (FindName("UnitBox") as TextBox).Text = IDatabaseService.updProduct.unit;
            (FindName("CostBox") as TextBox).Text = IDatabaseService.updProduct.price.ToString();
            (FindName("ManBox") as TextBox).Text = IDatabaseService.updProduct.manufacturer;
            (FindName("providerBox") as TextBox).Text = IDatabaseService.updProduct.provider;
            (FindName("categoryBox") as TextBox).Text = IDatabaseService.updProduct.category;
            (FindName("QuantityBox") as TextBox).Text = IDatabaseService.updProduct.quantity;
            (FindName("DescBox") as TextBox).Text = IDatabaseService.updProduct.description;
            (FindName("img") as System.Windows.Controls.Image).Source = new BitmapImage(new Uri(IDatabaseService.updProduct.image));
        }

        private void ProdFill() {
            prod = IDatabaseService.updProduct;
            prod.id = (FindName("IDBox") as TextBox).Text;
            prod.name = (FindName("nameBox") as TextBox).Text ;
            prod.unit = (FindName("UnitBox") as TextBox).Text;
            prod.price= Double.Parse((FindName("CostBox") as TextBox).Text);
            prod.manufacturer = (FindName("ManBox") as TextBox).Text;
            prod.provider = (FindName("providerBox") as TextBox).Text;
            prod.category = (FindName("categoryBox") as TextBox).Text;
            prod.quantity = (FindName("QuantityBox") as TextBox).Text;
            prod.description = (FindName("DescBox") as TextBox).Text = prod.description;
            prod.image = imgPath; 

        }

        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                imgPath = openFileDialog.FileName;
                bitmap.UriSource = new Uri(imgPath);
                bitmap.EndInit();
                (FindName("img") as System.Windows.Controls.Image).Source = bitmap;
            }
        }

        private void EventBrn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDatabaseService.AddUpd)
                {
                    DataBase.AddData(prod);
                }
                else {
                    DataBase.UpdData(prod);
                }
                Close();
            }
            catch {
            
            }
        }
    }
}
