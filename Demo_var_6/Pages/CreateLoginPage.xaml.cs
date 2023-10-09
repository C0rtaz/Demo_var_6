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
    /// Логика взаимодействия для CreateLoginPage.xaml
    /// </summary>
    public partial class CreateLoginPage : UserControl
    {
        public CreateLoginPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init() {
            Grid myGrid = new Grid();
            CreateColumn(myGrid);
            CreateRows(myGrid);
            CreateLabel(myGrid);
            CreateTextBox(myGrid);
            CreateButton(myGrid);
            this.Content = myGrid;
        }


        private void CreateButton(Grid grid)
        {
            Button myButton = new Button() {
                Content = "Войти",
                FontSize = 14,
                Width = 100,
                Height = 30,
            };
            grid.Children.Add(myButton);
            Grid.SetColumn(myButton,1);
            Grid.SetRow(myButton, 3);

        }

        private void CreateTextBox(Grid grid) {
            TextBox textBoxLogin = new TextBox() {
                FontSize = 24,
                Name = "loginBox",
                Height = 35,
                Width = 160,
                MaxLength = 12,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
            };

            PasswordBox textBoxPassword = new PasswordBox()
            {
                FontSize = 24,
                Name = "passBox",
                Height = 35,
                Width = 160,
                MaxLength = 12,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
            };

            grid.Children.Add(textBoxLogin);
            grid.Children.Add(textBoxPassword);
            Grid.SetColumn(textBoxLogin, 1);
            Grid.SetColumn(textBoxPassword, 1);
            Grid.SetRow(textBoxLogin, 1);
            Grid.SetRow(textBoxPassword, 2);
        }

        private void CreateLabel(Grid grid) {
            Label labelAuto = new Label()
            {
                FontSize = 36,
                FontWeight = FontWeights.Bold,
                Content = "Авторизация",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            Label labelLogin = new Label() {
                FontSize = 16,
                Content = "Логин",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            Label labelPassword = new Label() {
                FontSize = 16,
                Content = "Пароль",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                
            };



            grid.Children.Add(labelPassword);
            grid.Children.Add(labelLogin);
            grid.Children.Add(labelAuto);
            Grid.SetRow(labelLogin, 1);
            Grid.SetRow(labelPassword,2);
            Grid.SetColumnSpan(labelAuto, 2);
            
        }
        private void CreateRows(Grid grid) {
            for (int i = 0; i < 4; ++i) {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void CreateColumn(Grid grid) {
            for (int i = 0; i < 2; ++i){
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

    }
}
