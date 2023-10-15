﻿using Demo_var_6.Classes;
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
    /// Логика взаимодействия для CreateMainFilter.xaml
    /// </summary>
    public partial class CreateMainFilter : UserControl
    {
        public PanelProducts panelProducts = new PanelProducts();

        
        public CreateMainFilter()
        {
            InitializeComponent();
            Init();
        }

        private void Init() {
            Grid mainGrid = new Grid() { Name = "grid" };
            CreateTable(mainGrid);
            mainGrid.Children.Add(panelProducts);
            Label label = CreateGreetings();
            CreateSearch(mainGrid);
            CreateFilter(mainGrid);
            mainGrid.Children.Add(label);
            Grid.SetRow(label, 0);
            Grid.SetRow(panelProducts, 1);
            Content = mainGrid;
        }




        private Label CreateGreetings() {
            Label greetingLabel = new Label() {
                Content = "Здраствуйте, " + IDatabaseService.Name,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
            };
            return greetingLabel;
        }

        public void CreateSearch(Grid grid) {
            ComboBox comboBox = new ComboBox()
            {
                Width = 160,
                Height = 25,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Name = "searchComboBox",
                
            };
            List<string> strings = DataBase.ColumnNameProducts();
            IDatabaseService.querySearch = new Dictionary<string, string>(strings.Select(s => new KeyValuePair<string, string>(s, "")));
            IDatabaseService.queryFilter = new Dictionary<string, string>(strings.Select(s => new KeyValuePair<string, string>(s, "")));

            foreach (string s in strings)
            {
                comboBox.Items.Add(new ComboBoxItem() { Content = s });
            }
            TextBox textBox = new TextBox() {
                Text = "Введите текст для поиска",
                FontSize = 16,
                Height = 25,
                Width = 160,
                Margin = new Thickness(170, 0,0,0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Name ="txtBoxSearch"
            };
            textBox.TextChanged += TextCh;
            grid.Children.Add(comboBox);
            grid.Children.Add(textBox);
        }

        private void TextCh(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            Grid parentGrid = textBox.Parent as Grid;
            if (parentGrid == null) return;

            ComboBox comboBox = FindChild<ComboBox>(parentGrid, "searchComboBox");
            if (comboBox == null) return;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (selectedItem == null) return;
            string? key = "[" + selectedItem.Content.ToString() + "]";
            if (key == null) return;

            string? value;
            if (IDatabaseService.querySearch == null)
            {
                return;
            }
            if (IDatabaseService.querySearch.TryGetValue(key, out value) && !string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(textBox.Text))
            {
                IDatabaseService.querySearch[key] = textBox.Text;
            }
            else
            {
                IDatabaseService.querySearch.Add(key, textBox.Text);
            }

            panelProducts.Render();
        }

        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T && ((FrameworkElement)child).Name == childName)
                {
                    return (T)child;
                }
                
            }

            return null;
        }
        private void CreateFilter(Grid grid) {
            ComboBox comboBoxPrice = new ComboBox() {
                Width = 100,
                Height = 50,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            comboBoxPrice.Items.Add("Возрастанию");
            comboBoxPrice.Items.Add("Убыванию");

            comboBoxPrice.SelectionChanged += (sender, e) => {
                ComboBox? comboBox = sender as ComboBox;
                if (comboBox == null) return;
                IDatabaseService.Sort = comboBox.SelectedItem.ToString() == "Возрастанию" ? "asc" : "desc";
                panelProducts.Render();
            };

            ComboBox comboBoxFilt = new ComboBox()
            {
                Width = 100,
                Height = 50,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom

            };
            List<string> strings = DataBase.SearchUniqItems("Произовдитель");
            foreach (string s in strings) {
                comboBoxFilt.Items.Add(s);
            }
            comboBoxPrice.SelectionChanged += (sender, e) => {
                ComboBox? comboBox = sender as ComboBox;
                if (comboBox == null) return;
                IDatabaseService.manufacturerSort = comboBox.SelectedItem.ToString();
                if (IDatabaseService.manufacturerSort == null) return;
                panelProducts.Render();
            };
            grid.Children.Add(comboBoxPrice);
            grid.Children.Add(comboBoxFilt);
        }
        
        private void CreateTable(Grid grid) {
            RowDefinition rowDefinition1 = new RowDefinition()
            {
                Height = new GridLength(20, GridUnitType.Star)
            };
            RowDefinition rowDefinition2 = new RowDefinition()
            {
                Height = new GridLength(80, GridUnitType.Star)
            };
            ColumnDefinition columnDefinition1 = new ColumnDefinition
            {
                Width = new GridLength(80, GridUnitType.Star)
            };
            ColumnDefinition columnDefinition2 = new ColumnDefinition
            {
                Width = new GridLength(20, GridUnitType.Star)
            };
            grid.RowDefinitions.Add(rowDefinition1);
            grid.RowDefinitions.Add(rowDefinition2);
            grid.ColumnDefinitions.Add(columnDefinition1);
            grid.ColumnDefinitions.Add(columnDefinition2);

        }
    }
}
