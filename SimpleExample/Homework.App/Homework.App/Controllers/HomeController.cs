using System;
using System.Data;
using System.Web.Mvc;

namespace Homework.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CreateDbFile();
            AddNewRecord("HK","Eng","10");
            GetAllRecords();
            return View();
        }

        private static void CreateDbFile()
        {
            if(System.IO.File.Exists("HomeworkDB.db3")) return;

            const string createTableQuery = @"CREATE TABLE IF NOT EXISTS [data] (
                                  [Name] VARCHAR(50),
                                  [Book] VARCHAR(1000),
                                  [Page] VARCHAR(1000),
                                  [Date] DATE
                                  )";
            System.Data.SQLite.SQLiteConnection.CreateFile("HomeworkDB.db3");
            using (var con = new System.Data.SQLite.SQLiteConnection("data source=HomeworkDB.db3"))
            {
                using (var com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();

                    com.CommandText = createTableQuery;
                    com.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        private static void AddNewRecord(string name, string book, string page)
        {
            using (var con = new System.Data.SQLite.SQLiteConnection("data source=HomeworkDB.db3"))
            {
                using (var com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();

                    var dateTemp = DateTime.Now.ToString("s");
                    var addRecordQuery = String.Format("INSERT INTO data (Name, Book, Page, Date) " +
                                                          "values ('{0}','{1}', '{2}', '{3}')", name, book, page, dateTemp);
                    com.CommandText = addRecordQuery;
                    com.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        private static void GetAllRecords()
        {
            using (var con = new System.Data.SQLite.SQLiteConnection("data source=HomeworkDB.db3"))
            {
                using (var com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();

                    com.CommandText = "Select * FROM data";
                    using (var reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name1 = reader["Name"];
                            var book1 = reader["Book"];
                            var page1 = reader["Page"];
                            var date1 = (DateTime)reader["Date"];
                        }
                    }

                    con.Close();
                }
            }
        }
    }
}