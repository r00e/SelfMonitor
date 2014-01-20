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
//            CreateDbFile();
//            AddNewRecord("HK","Eng","10");
//            AddNewRecord("YR","Chinese","15");
            return View(GetAllRecords());
        }

//        private static void CreateDbFile()
//        {
//            if(System.IO.File.Exists("HomeworkDB.db3")) return;
//
//            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS [data] (
//                                  [Name] VARCHAR(50),
//                                  [Book] VARCHAR(1000),
//                                  [Page] VARCHAR(1000),
//                                  [Date] DATE
//                                  )";
//            System.Data.SQLite.SQLiteConnection.CreateFile("HomeworkDB.db3");
//            using (var con = new System.Data.SQLite.SQLiteConnection("data source=HomeworkDB.db3"))
//            {
//                using (var com = new System.Data.SQLite.SQLiteCommand(con))
//                {
//                    con.Open();
//
//                    com.CommandText = createTableQuery;
//                    com.ExecuteNonQuery();
//
//                    con.Close();
//                }
//            }
//        }

        public ActionResult AddNewRecord(string name, string book, string page)
        {
            const string connectionString = "Data Source=(local); Initial Catalog=HanHomework; Integrated Security=True";

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

            const string connectionString = "Data Source=(local); Initial Catalog=HanHomework; Integrated Security=True";

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