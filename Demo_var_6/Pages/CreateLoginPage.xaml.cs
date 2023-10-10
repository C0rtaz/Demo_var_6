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
            Grid myGrid = new Grid() {
                Name = "MainGrid"
            };
            IDatabaseService.Login = "Гость";
            IDatabaseService.Role = "Гость";
            CreateColumn(myGrid);
            CreateRows(myGrid);
            CreateLabel(myGrid);
            CreateTextBox(myGrid);
            CreateButton(myGrid);
            Content = myGrid;
        }


        private void CreateButton(Grid grid)
        {
            Button ButtonLogin = new Button() {
                Content = "Войти",
                FontSize = 14,
                Width = 100,
                Height = 30,
            };

            Button buttonGuest = new Button() {
                Content = "Войти как гость",
                FontSize = 14,
                Width = 150,
                Height = 30,
                
            };
            grid.Children.Add(buttonGuest);
            grid.Children.Add(ButtonLogin);
            buttonGuest.Click += (sender, e) =>
            {
                Window parentWindow = Window.GetWindow(this);
                parentWindow.Close();
            };

            ButtonLogin.Click += (sender, e) =>
            {
                TextBox? loginBox = LogicalTreeHelper.FindLogicalNode(grid, "loginBox") as TextBox;
                PasswordBox? passBox = LogicalTreeHelper.FindLogicalNode(grid, "passBox") as PasswordBox;
                if (!(loginBox != null && passBox != null) || string.IsNullOrEmpty(loginBox.Text) || string.IsNullOrEmpty(passBox.Password))
                {
                    MessageBox.Show("Пожалуйста, заполните поля или зайдите как гость.");
                    return;
                }
                if (DataBase.loginChecker(loginBox.Text, passBox.Password))
                {
                    IDatabaseService.Login = loginBox.Text;
                    Window parentWindow = Window.GetWindow(this);
                    parentWindow.Close();
                }
                else {
                    MessageBox.Show("Неверный логин или пароль.");
                    return;
                }
            };


            Grid.SetRow(buttonGuest, 3);
            Grid.SetColumn(ButtonLogin, 1);
            Grid.SetRow(ButtonLogin, 3);

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
