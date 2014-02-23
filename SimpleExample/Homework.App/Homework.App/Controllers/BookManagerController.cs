using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Homework.App.Models;

namespace Homework.App.Controllers
{
    public class BookManagerController : Controller
    {
        public ActionResult Index()
        {
            return View(GetAllBooks());
        }

        public ActionResult AddNewBook(string bookName, string pageCount)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HanHomeworkConnectStr"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var addRecordQuery = String.Format("INSERT INTO book (bookName, pageCount) " +
                                                        "values ('{0}','{1}')", bookName, pageCount);
                var command = new SqlCommand(addRecordQuery, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        private List<Book> GetAllBooks()
        {
            var books = new List<Book>();
            var connectionString = ConfigurationManager.ConnectionStrings["HanHomeworkConnectStr"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                const string commandText = "SELECT * FROM book";
                var command = new SqlCommand(commandText, connection);

                connection.Open();
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        books.Add(new Book());
                        books.Last().Id = (int)dataReader["Id"];
                        books.Last().BookName = (string)dataReader["bookName"];
                        books.Last().PageCount = (string)dataReader["pageCount"];
                    }
                }
            }

            return books;            
        } 
	}
}