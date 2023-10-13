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
            public string id, name, unit, manufacturer, provider, category, sale, quantity, description, image;
            public double price;
        };

        public static string? Login { get; set; }
        public static string? Role { get; set; }

        public static string? Name { get; set; }

        public static string connectionString = "Data Source=DESKTOP-36G6NKF;Initial Catalog=DemoEx;Integrated Security=True";

        public static List<Product>? products;

        public static Dictionary<string, string>? queryFilter;

    }
}
