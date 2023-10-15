using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Demo_var_6.Classes
{
    internal interface IDatabaseService
    {
        struct Product
        {
            public string id, name, unit, manufacturer, provider, category, sale, quantity, description, image, maxSale;
            public double price;
        };

        public static string Name = "Гость";
        public static string? Login { get; set; }
        public static string? Role { get; set; }

        public static string defaultPathForDataImage = "Images/";

        public static string defaultImage = "picture.png";

        public static string defaultPath = "pack://application:,,,/Demo_var_6;component/Resources/";

        public static bool AddUpd = false;

        public static Product updProduct;

       public static string Sort = "";

        public static string? manufacturerSort = "";

        public static string connectionString = "Data Source=DESKTOP-36G6NKF;Initial Catalog=DemoEx;Integrated Security=True";

        public static List<Product>? products;

        public static Dictionary<string, string>? querySearch;

        public static Dictionary<string, string>? queryFilter;

    }
}
