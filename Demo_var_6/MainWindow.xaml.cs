using Demo_var_6.Pages;
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

namespace Demo_var_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static CreateMainFilter mainpage = new CreateMainFilter();
        public MainWindow()
        {
            LoginFormOpener();
            InitializeComponent();
            
            Page();
        }

         public void LoginFormOpener() {
            Forms.Login login = new Forms.Login();
            login.ShowDialog();

         }

        public void Page() {
            Content = mainpage;
        }
    }
}
