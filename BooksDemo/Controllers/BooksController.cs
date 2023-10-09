using BooksDemo.Models;
using System;
using System.Web.Mvc;

namespace BooksDemo.Controllers
{
    public class BooksController : Controller
    {
        // GET: Library Management System
        #region Index Method
        public ActionResult Index()
        {
            Categories categories = new Categories();
            Publishers publishers = new Publishers();
            BooksView bookCategory = (BooksView)Session["BooksData"];
            if (bookCategory == null)
            {
                bookCategory = new BooksView
                {
                    BookName = "",
                    CategoryId = 0,
                    PageNumber = 1,
                    PageSize = 3,
                    PublisherId = 0
                };
            }
            bookCategory.CategoryModel = categories.GetList();
            bookCategory.PublishersModel = publishers.GetList();
            return View(bookCategory);
        }
        #endregion

        #region Get Books Method
        public JsonResult GetBooks(BooksView model)
        {
            Books book = new Books();
            model.BooksViewModel = book.GetList(model);
            if (model.BooksViewModel.Count == 0)
            {
                model.TotalCount = 0;
            }
            else
            {
                model.TotalCount = model.BooksViewModel[0].TotalCount;
                model.TotalCount = Convert.ToInt32(Math.Ceiling((double)model.TotalCount / model.PageSize));
            }
            Session["BooksData"] = model;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add or Update Book
        public JsonResult GetData(int id = 0)
        {
            Books book = new Books();
            if (id != 0)
            {
                book.BookId = Convert.ToInt32(id);
                book.Load();
            }
            TempData["BooksView"] = new BooksView(book);
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddBook(int Id = 0)
        {
            GetData(Id);
            BooksView model = new BooksView();
            Categories category = new Categories();
            model = (BooksView)TempData["BooksView"];
            model.CategoryModel = category.GetList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUpdateBook(BooksView model)
        {
            Books book = new Books();
            book.BookId = model.BookId;
            book.BookName = model.BookName;
            book.CategoryId = model.CategoryId;
            book.IsActive = model.IsActive;
            book.Save();
            return Json(model.BookId > 0 ? "Book Updated Successfully" : "Book Inserted Successfully");
        }
        #endregion

        #region Delete Book
        [HttpPost]
        public ActionResult DeleteBook(int BookId)
        {
            bool CheckDelete = false;
            Books book = new Books();
            if (BookId != 0)
            {
                book.BookId = Convert.ToInt32(BookId);
                CheckDelete = book.Delete();
            }
            return Json(CheckDelete);
        }
        #endregion

        #region Reset Session
        [HttpPost]
        public ActionResult ResetSession()
        {
            Session["BooksData"] = null;
            BooksView book = new BooksView()
            {
                BookName = "",
                CategoryId = 0,
                PageNumber = 1,
                PageSize = 3,
                PublisherId = 0
            };
            return Json(book, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}