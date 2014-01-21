using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Homework.App.Models;

namespace Homework.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(GetAllRecords());
        }

        public ActionResult AddNewRecord(string name, string book, string page)
        {
            const string connectionString = "Data Source=(local); Initial Catalog=HanHomework; User ID=handb; Password=123456;Integrated Security=false";

            using (var connection = new SqlConnection(connectionString))
            {
                var dateTemp = DateTime.Now.ToString("s");
                var addRecordQuery = String.Format("INSERT INTO data (Name, Book, Page, Date) " +
                                                        "values ('{0}','{1}', '{2}', '{3}')", name, book, page, dateTemp);
                var command = new SqlCommand(addRecordQuery, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        private List<Record> GetAllRecords()
        {
            var records = new List<Record>();

            const string connectionString = "Data Source=(local); Initial Catalog=HanHomework; User ID=handb; Password=123456;Integrated Security=false";

            using (var connection = new SqlConnection(connectionString))
            {
                const string commandText = "SELECT * FROM data";
                var command = new SqlCommand(commandText, connection);

                connection.Open();
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        records.Add(new Record());
                        records.Last().Name = (string) dataReader["Name"];
                        records.Last().Book = (string) dataReader["Book"];
                        records.Last().Page = (string) dataReader["Page"];
                        records.Last().Date = (DateTime)dataReader["Date"];

                    }
                }
            }

            return records;
        }
    }
}