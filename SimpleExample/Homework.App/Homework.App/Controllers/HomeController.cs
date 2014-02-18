using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Helpers;
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
            var connectionString = ConfigurationManager.ConnectionStrings["HanHomeworkConnectStr"].ConnectionString;

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

        [HttpPost]
        public Record UpdateRecord(Record record, string type)
        {
            string value = null;
            switch (type)
            {
                case "name":
                    value = record.Name;
                    break;
                case "book":
                    value = record.Book;
                    break;
                case "page":
                    value = record.Page;
                    break;
            }

            var connectionString = ConfigurationManager.ConnectionStrings["HanHomeworkConnectStr"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var deleteRecord = String.Format("UPDATE [hanhomework].[dbo].[data] SET {0} = '{1}' WHERE Id = {2} ", type, value, record.Id);

                var command = new SqlCommand(deleteRecord, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return record;
        }
        
        [HttpPost]
        public int DeleteRecord(Record record)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HanHomeworkConnectStr"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var deleteRecord = String.Format("DELETE FROM data WHERE Id = {0} ", record.Id);

                var command = new SqlCommand(deleteRecord, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return record.Id;
        }


        private List<Record> GetAllRecords()
        {
            var records = new List<Record>();
            var connectionString = ConfigurationManager.ConnectionStrings["HanHomeworkConnectStr"].ConnectionString;

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
                        records.Last().Id = (int) dataReader["Id"];
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