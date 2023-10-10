using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Demo_var_6.Classes
{
    internal class DataBase : IDatabaseService
    {
        static SqlConnection connection = new SqlConnection(IDatabaseService.connectionString);

        public static bool loginChecker(string login, string pass) {
            string query = "SELECT COUNT(*) FROM Login.Пользователь WHERE Логин='@login' AND Пароль='@password'";
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@password", pass);
            int count = (int)cmd.ExecuteScalar();
            connection.Close();
            if (count == 1) {
                RoleSearch(login);
                NameSearch(login);
            }
            return count == 1 ? true : false;

        }


        private static void RoleSearch(string login) {
            string query = "SELECT r.Название FROM Login.Пользователь as p " + "inner join Login.Роль as r on r.id = p.idРоли " + "WHERE Логин='@login'";
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@login", login);
            IDatabaseService.Role = (string)cmd.ExecuteScalar();
            connection.Close();
        }
        private static void NameSearch(string login)
        {
            string query = "SELECT ФИО FROM Login.Пользователь WHERE Логин='@login'";
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@login", login);
            IDatabaseService.Name = (string)cmd.ExecuteScalar();
            connection.Close();
        }

        public static List<string> ColumnNameProducts() {
            List<string> strings = new List<string>();
            var filter = new Dictionary<string, string>();
            string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Товар'";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strings.Add(reader.GetString(0));
                filter.Add(reader.GetString(0), "");
            }
            IDatabaseService.queryFilter = filter;
            return strings;
        }

        public static List <IDatabaseService.Product> ProductData() {
            var products = new List<IDatabaseService.Product>();
            string query = CreateQueryFilter();
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                IDatabaseService.Product product = new IDatabaseService.Product() {
                    id = reader.GetString(0),
                    name = reader.GetString(1),
                    unit = reader.GetString(2),
                    price = reader.GetString(3),
                    manufacturer = reader.GetString(5),
                    provider = reader.GetString(6),
                    category = reader.GetString(7),
                    sale = reader.GetString(8),
                    quantity = reader.GetString(9),
                    description = reader.GetString(10),
                    image = reader.GetString(11)
                };
                products.Add(product);
            }

            return products;
        }

        private static string CreateQueryFilter() {
            string query = "select * from Login.Товар", querySub = " where ", queryFilter = "";

            foreach (KeyValuePair<string, string> kvp in IDatabaseService.queryFilter)
            {
                if (kvp.Value != "") queryFilter += querySub;
                queryFilter += $" {kvp.Key} = '{kvp.Value}' ";
                if (queryFilter.Contains("where")) querySub = "and";
            }


            return query;
        } 

        

    }
}
