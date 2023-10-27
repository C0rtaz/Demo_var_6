using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Shapes;
using System.Windows;
using static Demo_var_6.Classes.IDatabaseService;
using System.Reflection.PortableExecutable;

namespace Demo_var_6.Classes
{
    internal class DataBase : IDatabaseService
    {


        public static bool loginChecker(string login, string pass)
        {
            string query = $"SELECT COUNT(*) FROM Login.Пользователь WHERE Логин='{login}' AND Пароль='{pass}'";
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString))
            {

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


        private static void RoleSearch(string login)
        {
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
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                IDatabaseService.Name = (string)cmd.ExecuteScalar();
                connection.Close();
            }
        }

        public static List<string> ColumnNameProducts()
        {
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
        //Message
        public static List<IDatabaseService.Product> ProductData()
        {
            var products = new List<IDatabaseService.Product>();
            string query = CreateQueryFilter();
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                //MessageBox.Show(query);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    IDatabaseService.Product product = new IDatabaseService.Product()
                    {
                        id = reader.GetString(0),
                        name = reader.GetString(1),
                        unit = reader.GetString(2),
                        price = Decimal.ToDouble(reader.GetDecimal(3)),
                        maxSale = reader.GetInt32(4).ToString(),
                        manufacturer = reader.GetString(5),
                        provider = reader.GetString(6),
                        category = reader.GetString(7),
                        sale = reader.GetInt32(8).ToString(),
                        quantity = reader.GetInt32(9).ToString(),
                        description = reader.GetString(10),
                        image = reader.IsDBNull(11) ? IDatabaseService.defaultPath + IDatabaseService.defaultImage : IDatabaseService.defaultPath + IDatabaseService.defaultPathForDataImage + reader.GetString(11)
                    };
                    products.Add(product);
                }

                return products;
            }
        }

        private static string CreateQueryFilter()
        {
            string query = "select * from Login.Товар", querySub = " where ", queryFilter = "";
            if (IDatabaseService.queryFilter == null) return query;
            foreach (KeyValuePair<string, string> kvp in IDatabaseService.queryFilter)
            {
                if (kvp.Value != null && kvp.Value != "")
                {
                    queryFilter += querySub;
                    queryFilter += $" {kvp.Key} = '{kvp.Value}' ";
                }
                if (queryFilter.Contains("where")) querySub = "and";
            }


            return query + queryFilter + ManufacturerQuery(queryFilter) + QuerySearch(queryFilter) + SortingQuery();
        }

        private static string ManufacturerQuery(string queryFilter)
        {
            string? query = IDatabaseService.manufacturerSort, querySub = " where ", queryMan = " Производитель = ";
            //IDatabaseService.manufacturerSort = "";
            if (query == "" || query == "---") return "";
            if (queryFilter.Contains("where"))
            {
                //if (QuerySearch(queryFilter) != "") queryNext = " and ";
                return " and " + queryMan + "'" + query + "'";// + queryNext;
            }
            else
            {
                // if (QuerySearch(queryFilter) != "") queryNext = " and ";
                return querySub + queryMan + "'" + query + "'"; //+ queryNext;
            }
            //return "";
        }

        private static string SortingQuery()
        {
            string querySub = " order by Стоимость ", query = IDatabaseService.Sort;
            //IDatabaseService.Sort = "";
            if (query == "") return "";
            return querySub + query;
        }

        private static string QuerySearch(string queryFilter)
        {
            string query = "", querySub = " where ", queryEmpty = "";
            if (IDatabaseService.querySearch == null) return query;
            if (ManufacturerQuery(queryFilter) != "") query += " and ";
            foreach (KeyValuePair<string, string> kvp in IDatabaseService.querySearch)
            {

                if (kvp.Value != null && kvp.Value != "")
                {
                    if (queryFilter.Contains("where") || ManufacturerQuery(queryFilter).Contains("where")) querySub = "and";
                    else
                    {
                        query += querySub;
                    }
                    if (query.Contains("like"))
                    {
                        query += querySub;
                        query += $" {kvp.Key} like '%{kvp.Value}%' ";
                    }
                    else
                    {
                        if (queryFilter != "" && ManufacturerQuery(queryFilter) != "") { query += " and"; }
                        query += " ( ";
                        query += $" {kvp.Key} like '%{kvp.Value}%' ";
                    }
                }
            }
            if (query.Length == 5) query = queryEmpty;
            if (query != "")
            {
                query += ")";
            }
            return query;
        }

        public static List<string> SearchUniqItems(string columnName)
        {
            List<string> items = new List<string>();
            string query = $"SELECT DISTINCT [{columnName}] from Login.Товар";
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    items.Add(reader.GetString(0));
                }
                connection.Close();
            }
            return items;
        }

        public static void AddData(IDatabaseService.Product product)
        {
            string query = $"insert into Login.Товар values ('{product.id}','{product.name}','{product.unit}','{product.price}','{product.sale}','{product.manufacturer}','{product.provider}','{product.category}','{product.sale}','{product.quantity}','{product.description}','{product.image}')";
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void UpdData(IDatabaseService.Product product)
        {
            string query = $"UPDATE Login.Товар SET Наименование = '{product.name}', [Ед. измерения] = '{product.unit}', Стоимость = '{product.price}', [Кол-во на складе] = '{product.quantity}', Производитель = '{product.manufacturer}', Поставщик = '{product.provider}', Категория = '{product.category}', Скидка = '{product.sale}', Размер_максимально_возможной_скидки = '{product.sale}', Описание = '{product.description}', Изображение = '{(product.image == "" ? null : product.image)}' WHERE Артикул = '{product.id}'";

            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString)) 
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void DelData(IDatabaseService.Product product)
        {
            string query = $"delete Login.Товар where Артикул = '{product.id}'";
            using (SqlConnection connection = new SqlConnection(IDatabaseService.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
