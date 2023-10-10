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
        public CreateMainFilter()
        {
            InitializeComponent();
            Init();
        }

        private void Init() {
            Grid grid = new Grid();
            CreateTable(grid);
            
            Content = grid;
        }


        private void CreateFilter() {
            ComboBox comboBox = new ComboBox() {
                Width = 100,
                Height = 50,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            List<string> strings = DataBase.ColumnNameProducts();
            foreach(string s in strings)
            {
                comboBox.Items.Add(new ComboBoxItem() { Content = s});
            }

            TextBox textBox = new TextBox() {
                Width = 100,
                Height = 50,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };


            Button button = new Button() {
                Width = 100,
                Height = 50,
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center

            };
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
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

        }
    }
}