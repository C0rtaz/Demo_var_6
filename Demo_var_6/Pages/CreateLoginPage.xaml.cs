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
            this.Content = myGrid;
        }



        private void CreateLabel(Grid grid) {
            Label label = new Label()
            {
                FontSize = 36,
                FontWeight = FontWeights.Bold,
                Content = "Авторизация",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            grid.Children.Add(label);
            Grid.SetColumnSpan(label, 2);
        
        }
        private void CreateRows(Grid grid) {
            for (int i = 0; i < 4; ++i) {
                RowDefinition rowDefinition = new RowDefinition();
                grid.RowDefinitions.Add(rowDefinition);
            }
        }

        private void CreateColumn(Grid grid) {
            for (int i = 0; i < 2; ++i)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                grid.ColumnDefinitions.Add(colDef);
            }
        }

    }
}
