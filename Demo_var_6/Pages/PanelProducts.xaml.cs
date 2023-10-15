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
        ScrollViewer scrollViewer = new ScrollViewer();
        Grid parentGrid = new Grid();
        public PanelProducts()
        {
            InitializeComponent();
            scrollViewer.Content = parentGrid;
            Init(parentGrid);
        }

        public void Render() {
            parentGrid.Children.Clear();
            while (parentGrid.RowDefinitions.Count != 0) {
                parentGrid.RowDefinitions.RemoveAt(0);
            }
            Init(parentGrid);
        }

        private void Init(Grid parentGrid)
        {
            IDatabaseService.products = DataBase.ProductData();

            foreach (IDatabaseService.Product product in IDatabaseService.products)
            {
                RowDefinition rowDefinition = new RowDefinition()
                {
                    Height = new GridLength(80),
                    Name = $"rowDefinition{parentGrid.RowDefinitions.Count}",
                };
                parentGrid.RowDefinitions.Add(rowDefinition);
                Border border = new Border() {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(2),
                    Background = int.Parse(product.quantity) == 0 ? Brushes.LightGray : Brushes.White,
                };

                Grid grid = new Grid()
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 150,
                    Name = $"grid{parentGrid.RowDefinitions.Count - 1}"
                };
                RadioButton radioButton = new RadioButton()
                {
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Name = $"rb{parentGrid.RowDefinitions.Count - 1}",
                    GroupName = "Products"
                };

                grid.Children.Add(radioButton);
                border.Child = grid;
                CreateItem(grid, product);
                parentGrid.Children.Add(border);
                Grid.SetRow(border, parentGrid.RowDefinitions.Count - 1);
            }
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Content = scrollViewer;
        }

        

        private void CreateItem(Grid grid, IDatabaseService.Product product)
        {


            Grid imgGrid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 75,
            };

            Grid infoGrid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 300

            };

            Grid quantityGrid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 300
            };

            
            imgGrid.Children.Add(ImageCreator(product));
            infoGrid.Children.Add(InfoForGrid(product));
            quantityGrid.Children.Add(QuantityInfoForGrid(product));
            
            grid.Children.Add(imgGrid);
            grid.Children.Add(infoGrid);
            grid.Children.Add(quantityGrid);
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(25, GridUnitType.Star) });
            Grid.SetColumn(imgGrid, 0);
            Grid.SetColumn(infoGrid, 1);
            Grid.SetColumn(quantityGrid, 2);
        }

        private Image ImageCreator(IDatabaseService.Product product) {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(product.image));
            return img;
        }
        private ScrollViewer QuantityInfoForGrid(IDatabaseService.Product product) {
            ScrollViewer scrollViewer = new ScrollViewer();
            Label label = new Label() {
                //Margin = new Thickness(5, 0, 0, 0),
                FontSize = 14,
                Content = "Количество: " + product.quantity,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            Grid grid = new Grid()
            {
                //Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,

            };
            grid.Children.Add(label);
            scrollViewer.Content = grid;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            return scrollViewer;
        }

        private ScrollViewer InfoForGrid(IDatabaseService.Product product)
        {
            ScrollViewer scrollViewer = new ScrollViewer();
            Grid grid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,

            };
            Label labelName = new Label()
            {
                Margin = new Thickness(0, 30, 0, 0),
                FontSize = 14,
                Content = product.name,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            TextBlock labelDesc = new TextBlock()
            {
                Margin = new Thickness(5),
                FontSize = 14,
                Text = product.description,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                TextWrapping = TextWrapping.Wrap,
            };
            Label labelManuf = new Label()
            {
                Margin = new Thickness(5),
                FontSize = 14,
                Content = "Производитель: " + product.manufacturer,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            Label labelPrice = new Label()
            {
                Margin = new Thickness(5),
                FontSize = 14,
                Content = "Цена: " + product.price,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            grid.Children.Add(labelName);
            grid.Children.Add(labelDesc);
            grid.Children.Add(labelManuf);
            grid.Children.Add(labelPrice);
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10, GridUnitType.Star) });
            Grid.SetRow(labelName, 0);
            Grid.SetRow(labelDesc, 1);
            Grid.SetRow(labelManuf, 2);
            Grid.SetRow(labelPrice, 3);
            scrollViewer.Content = grid;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            return scrollViewer;
        }


    }
}
