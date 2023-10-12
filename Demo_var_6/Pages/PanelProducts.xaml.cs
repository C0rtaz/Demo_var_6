using Demo_var_6.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo_var_6.Pages
{
    /// <summary>
    /// Логика взаимодействия для PanelProducts.xaml
    /// </summary>
    public partial class PanelProducts : UserControl
    {
        public PanelProducts()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            int i = 0;
            IDatabaseService.products = DataBase.ProductData();
            ScrollViewer scrollViewer = new ScrollViewer() {

            };
            Grid parentGrid = new Grid() {
                Margin = new Thickness(20),
            };
            scrollViewer.Content = parentGrid;
            foreach (IDatabaseService.Product product in IDatabaseService.products) {
                RowDefinition rowDefinition = new RowDefinition() {
                    
                };
                Grid grid = new Grid()
                {
                    Margin = new Thickness(20),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 150,
                };
                parentGrid.RowDefinitions.Add(rowDefinition);
                CreateItem(grid, product);
                Grid.SetRow(grid, ++i);
                parentGrid.Children.Add(grid);
            }
            Content = parentGrid;
        }

        private void CreateItem(Grid grid, IDatabaseService.Product product) {
            Grid imgGrid = new Grid() { 
                Margin = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            Grid infoGrid = new Grid()
            {
                Margin = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,

            };

            Grid quantityGrid = new Grid() {
                Margin = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            infoGrid.Children.Add(InfoForGrid(product));
            quantityGrid.Children.Add(QuantityInfoForGrid(product));

            grid.Children.Add(imgGrid);
            grid.Children.Add(infoGrid);
            grid.Children.Add(quantityGrid);
        }


        private Grid QuantityInfoForGrid(IDatabaseService.Product product) {
            Label label = new Label() {
                FontSize = 14,
                Content = product.quantity
            };
            Grid grid = new Grid()
            {
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,

            };
            grid.Children.Add(label);
            return grid;
        }

        private ScrollViewer InfoForGrid(IDatabaseService.Product product) {
            ScrollViewer scrollViewer = new ScrollViewer();
            Grid grid = new Grid
            {
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,

            };
            Label labelName = new Label() {
                FontSize = 14,
                Content = product.name,
                FontWeight = FontWeights.Bold,
            };
            Label labelDesc = new Label() {
                FontSize = 14,
                Content = product.description,
            };
            Label labelManuf = new Label() {
                FontSize = 14,
                Content = "Производитель: " + product.manufacturer,
            };
            Label labelPrice = new Label()
            {
                FontSize = 14,
                Content = "Цена: " + product.price,
            };
            grid.Children.Add(labelName);
            grid.Children.Add(labelDesc);
            grid.Children.Add(labelManuf);
            grid.Children.Add(labelPrice);

            scrollViewer.Content = grid;

            return scrollViewer;
        }


    }
}
