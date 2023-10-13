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
        

        public static bool loginChecker(string login, string pass) {
            string query = $"SELECT COUNT(*) FROM Login.Пользователь WHERE Логин='{login}' AND Пароль='{pass}'";
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString)) {

                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                /*cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", pass);*/
                var count = cmd.ExecuteScalar();
                connection.Close();
                if ((int)count == 1)
                {
                    RoleSearch(login);
                    NameSearch(login);
                }
                return (int)count == 1 ? true : false;
            }

        }


        private static void RoleSearch(string login) {
            string query = "SELECT r.Название FROM Login.Пользователь as p " + "inner join Login.Роль as r on r.id = p.idРоли " + "WHERE Логин='@login'";
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@login", login);
                IDatabaseService.Role = (string)cmd.ExecuteScalar();
                connection.Close();
            }
        }
        private static void NameSearch(string login)
        {
            string query = $"SELECT ФИО FROM Login.Пользователь WHERE Логин='{login}'";
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString)) { 
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                //cmd.Parameters.AddWithValue("@login", login);
                IDatabaseService.Name = (string)cmd.ExecuteScalar();
                connection.Close();
            }
        }

        public static List<string> ColumnNameProducts() {
            List<string> strings = new List<string>();
            var filter = new Dictionary<string, string>();
            string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Товар'";
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString))
            {
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
        }

        public static List <IDatabaseService.Product> ProductData() {
            var products = new List<IDatabaseService.Product>();
            string query = CreateQueryFilter();
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    IDatabaseService.Product product = new IDatabaseService.Product()
                    {
                        id = reader.GetString(0),
                        name = reader.GetString(1),
                        unit = reader.GetString(2),
                        price = Decimal.ToDouble(reader.GetDecimal(3)),
                        manufacturer = reader.GetString(5),
                        provider = reader.GetString(6),
                        category = reader.GetString(7),
                        sale = reader.GetInt32(8).ToString(),
                        quantity = reader.GetInt32(9).ToString(),
                        description = reader.GetString(10),
                        // image = reader.GetString(11)
                    };
                    products.Add(product);
                }

                return products;
            }
        }

        private static string CreateQueryFilter() {
            string query = "select * from Login.Товар", querySub = " where ", queryFilter = "";
            if (IDatabaseService.queryFilter == null) return query;
            foreach (KeyValuePair<string, string> kvp in IDatabaseService.queryFilter)
            {
                if (kvp.Value != "") queryFilter += querySub;
                queryFilter += $" {kvp.Key} = '{kvp.Value}' ";
                if (queryFilter.Contains("where")) querySub = "and";
            }


            return query + queryFilter;
        }

        public static List<string> SearchUniqItems(string columnName) {
            List <string> items = new List<string>();
            string query = $"SELECT DISTINCT {columnName} from Login.Товар";
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString)) {
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    switch (columnName) {
                        case "Стоимость":
                            items.Add((Decimal.ToDouble(reader.GetDecimal(0))).ToString());
                            break;
                        case "Скидка":
                            items.Add(reader.GetInt32(0).ToString());
                            break;
                        case "[Кол-во на складе]":
                            items.Add(reader.GetInt32(0).ToString());
                            break;
                        default:
                            items.Add(reader.GetString(0));
                            break;
                    }
                }
            }
            return items;
        }

    }
}
