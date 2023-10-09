using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
namespace BooksDemo.Models
{
    public class BooksView
    {
        public BooksView() { }
        #region Properties
        //Get and Set property for BookId
        [Display(Name = "Id")]
        public int BookId
        {
            get; set;
        }

        //Get and Set property for BookName
        [Display(Name = "Book Name")]
        [Required(ErrorMessage = "Please Enter The Book Name")]
        public string BookName
        {
            get; set;
        }

        //Get and Set property for CategoryId
        [Display(Name = "Category Id")]
        [Required(ErrorMessage = "Please Select One Category")]
        public int CategoryId
        {
            get; set;
        }

        //Get and Set property for CategoryName
        [Display(Name = "Category")]
        [Required]
        public string CategoryName
        {
            get; set;
        }

        //Get and Set property for IsActive
        [Display(Name = "Status")]
        public bool IsActive
        {
            get; set;
        }

        //Get and Set property for CreatedBy
        [Display(Name = "Created By")]
        public int CreatedBy
        {
            get; set;
        }

        //Get and Set property for CreatedOn
        [Display(Name = "Created On")]
        public string CreatedOn
        {
            get; set;
        }

        //Get and Set property for ModifiedBy
        [Display(Name = "Modified By")]
        public int ModifiedBy
        {
            get; set;
        }

        //Get and Set property for ModifiedOn
        [Display(Name = "Modified On")]
        public string ModifiedOn
        {
            get; set;
        }
        [Display(Name = "Publisher")]
        public string PublisherBook { get; set; }

        public int PublisherId { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public List<int> PageSizeDropDown { get; set; } = new List<int> { 3, 5, 10 };

        public int TotalCount { get; set; }

        public List<BooksView> BooksViewModel { get; set; }

        public List<Categories> CategoryModel { get; set; }

        public List<Publishers> PublishersModel { get; set; }

        public BooksView(Books book)
        {
            this.BookId = book.BookId;
            this.BookName = book.BookName;
            this.CategoryId = book.CategoryId;
            this.IsActive = book.IsActive;
        }
        #endregion
    }
}