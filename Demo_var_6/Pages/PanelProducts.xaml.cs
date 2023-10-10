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
        }

        private void Init()
        {
            ScrollViewer scrollViewer = new ScrollViewer();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Content = scrollViewer;

            Grid grid = new Grid() {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 150,
            };


        }
    }
}
