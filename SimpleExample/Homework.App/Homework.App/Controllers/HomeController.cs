using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Homework.App.Models;

namespace Homework.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CreateDbFile();
//            AddNewRecord("HK","Eng","10");
//            AddNewRecord("YR","Chinese","15");
            return View(GetAllRecords());
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

        public ActionResult AddNewRecord(string name, string book, string page)
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
                }
            }

            return RedirectToAction("Index");
        }

        private List<Record> GetAllRecords()
        {
            var records = new List<Record>();

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
                            records.Add(new Record());
                            records.Last().Name = (string) reader["Name"];
                            records.Last().Book = (string) reader["Book"];
                            records.Last().Page = (string) reader["Page"];
                            records.Last().Date = (DateTime)reader["Date"];
                        }
                    }
                }
            }

            return records;
        }
    }
}