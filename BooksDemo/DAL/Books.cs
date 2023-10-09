using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using BooksDemo.Models;
using System.Collections.Generic;
using PagedList;
/// <summary>
/// Summary description for Books
/// </summary>
public class Books
{
    #region Variable Declaration
    //Variable to store database object to interact with database
    private Database db;
    #endregion

    #region Constructors
    //Initialize a new instance of Books class
    public Books()
    {
        //
        // TODO: Add constructor logic here
        //
        this.db = DatabaseFactory.CreateDatabase();
    }

    //Initialize a new instance of Books Class
    public Books(int bookId)
    {
        this.db = DatabaseFactory.CreateDatabase();
        this.BookId = bookId;
    }
    #endregion

    #region Properties
    //Get and Set property for BookId
    public int BookId
    {
        get; set;
    }

    //Get and Set property for BookName
    public string BookName
    {
        get; set;
    }

    //Get and Set property for CategoryId
    public int CategoryId
    {
        get; set;
    }

    //Get and Set property for IsActive
    public bool IsActive
    {
        get; set;
    }

    //Get and Set property for CreatedBy
    public int CreatedBy
    {
        get; set;
    }

    //Get and Set property for CreatedOn
    public DateTime CreatedOn
    {
        get; set;
    }

    //Get and Set property for ModifiedBy
    public int ModifiedBy
    {
        get; set;
    }

    //Get and Set property for ModifiedOn
    public DateTime ModifiedOn
    {
        get; set;
    }

    #endregion

    #region Actions
    #region Load Details for Books
    //Loads the details for Books
    //returns True is opreation is successful else False
    public bool Load()
    {
        try
        {
            if (this.BookId != 0)
            {
                DbCommand com = this.db.GetStoredProcCommand("BooksGetDetails");
                this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);
                DataSet ds = this.db.ExecuteDataSet(com);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    this.BookId = Convert.ToInt32(dt.Rows[0]["BookId"]);
                    this.BookName = Convert.ToString(dt.Rows[0]["BookName"]);
                    this.CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
                    this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    this.CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]);
                    this.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                    this.ModifiedBy = Convert.ToInt32(dt.Rows[0]["ModifiedBy"]);
                    this.ModifiedOn = Convert.ToDateTime(dt.Rows[0]["ModifiedOn"]);
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            //To Do: Handle Exception
            return false;
        }
    }
    #endregion

    #region Insert Details For Books
    //Insert Details for Books if BookId = 0
    //Else Update Details for Books
    //Returns True if Operation is successful else False
    public bool Save()
    {
        if (this.BookId == 0)
        {
            return this.Insert();
        }
        else
        {
            if (this.BookId > 0)
            {
                return this.Update();
            }
            else
            {
                this.BookId = 0;
                return false;
            }
        }
    }

    //Insert Details for new Books
    //Saves newly created Id in BookId
    //Returns True if operation is successful else False

    private bool Insert()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("BooksInsert");
            this.db.AddOutParameter(com, "BookId", DbType.Int32, 1024);

            if (!String.IsNullOrEmpty(this.BookName))
            {
                this.db.AddInParameter(com, "BookName", DbType.String, this.BookName);
            }
            else
            {
                this.db.AddInParameter(com, "BookName", DbType.String, DBNull.Value);
            }

            if (this.CategoryId > 0)
            {
                this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);
            }
            else
            {
                this.db.AddInParameter(com, "CategoryId", DbType.Int32, DBNull.Value);
            }

            this.db.AddInParameter(com, "IsActive", DbType.Boolean, this.IsActive);

            if (this.CreatedBy > 0)
            {
                this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CategoryId);
            }
            else
            {
                this.db.AddInParameter(com, "CreatedBy", DbType.Int32, DBNull.Value);
            }

            if (this.CreatedOn > DateTime.MinValue)
            {
                this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, this.CreatedOn);
            }
            else
            {
                this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, DBNull.Value);
            }

            if (this.ModifiedBy > 0)
            {
                this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
            }
            else
            {
                this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, DBNull.Value);
            }

            if (this.ModifiedOn > DateTime.MinValue)
            {
                this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, this.ModifiedOn);
            }
            else
            {
                this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, DBNull.Value);
            }
            this.db.ExecuteNonQuery(com);
            this.BookId = Convert.ToInt32(this.db.GetParameterValue(com, "BookId"));
        }
        catch (Exception ex)
        {
            // To Do: Handle Exception
            return false;
        }
        return this.BookId > 0; // Return Whether Id is returned
    }
    #endregion

    #region Update Details For Books
    //Update Details For Books
    //Returns True if operation is successful else False
    private bool Update()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("BooksUpdate");
            this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);

            if (!String.IsNullOrEmpty(this.BookName))
            {
                this.db.AddInParameter(com, "BookName", DbType.String, this.BookName);
            }
            else
            {
                this.db.AddInParameter(com, "BookName", DbType.String, DBNull.Value);
            }

            if (this.CategoryId > 0)
            {
                this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);
            }
            else
            {
                this.db.AddInParameter(com, "CategoryId", DbType.Int32, DBNull.Value);
            }

            this.db.AddInParameter(com, "IsActive", DbType.Boolean, this.IsActive);

            if (this.ModifiedBy > 0)
            {
                this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
            }
            else
            {
                this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, DBNull.Value);
            }

            if (this.ModifiedOn > DateTime.MinValue)
            {
                this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, this.ModifiedOn);
            }
            else
            {
                this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, DBNull.Value);
            }
            this.db.ExecuteNonQuery(com);
        }
        catch (Exception ex)
        {
            //To Do: Handle Exception
            return false;
        }
        return true;
    }
    #endregion

    #region Delete Record From Books
    //Deletes record from Books for provided BookId
    //Returns True if operation is successful else False

    public bool Delete()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("BooksDelete");
            this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);
            this.db.ExecuteNonQuery(com);
        }
        catch (Exception ex)
        {
            //To Do: Handle Exception
            return false;
        }
        return true;
    }
    #endregion

    #region Get List of Books
    //Get List of Books
    //Returns Dataset of Result

    public List<BooksView> GetList(BooksView model)
    {
        DataSet ds = null;
        List<BooksView> books = new List<BooksView>();
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("BooksGetList");
            if (!String.IsNullOrEmpty(model.BookName))
            {
                this.db.AddInParameter(com, "BookName", DbType.String, model.BookName);
            }
            else
            {
                this.db.AddInParameter(com, "BookName", DbType.String, DBNull.Value);
            }

            if (model.CategoryId > 0)
            {
                this.db.AddInParameter(com, "CategoryId", DbType.Int32, model.CategoryId);
            }
            else
            {
                this.db.AddInParameter(com, "CategoryId", DbType.Int32, DBNull.Value);
            }
            if(model.PageNumber > 0)
            {
                this.db.AddInParameter(com, "PageNumber", DbType.Int32, model.PageNumber);
            }
            else
            {
                this.db.AddInParameter(com, "PageNumber", DbType.Int32, DBNull.Value);
            }
            if (model.PageSize > 0)
            {
                this.db.AddInParameter(com, "PageSize", DbType.Int32, model.PageSize);
            }
            else
            {
                this.db.AddInParameter(com, "PageSize", DbType.Int32, DBNull.Value);
            }
            if(model.PublisherId > 0)
            {
                this.db.AddInParameter(com, "PublisherId", DbType.Int32, model.PublisherId);
            }
            else
            {
                this.db.AddInParameter(com, "PublisherId", DbType.Int32, DBNull.Value);
            }
            ds = db.ExecuteDataSet(com);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                books.Add(new BooksView
                {
                    BookId = Convert.ToInt32(row["BookId"]),
                    BookName = Convert.ToString(row["BookName"]),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    CategoryName = Convert.ToString(row["CategoryName"]),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedBy = Convert.ToInt32(row["IsActive"]),
                    CreatedOn = Convert.ToString(row["CreatedOn"]),
                    ModifiedBy = Convert.ToInt32(row["ModifiedBy"]),
                    ModifiedOn = Convert.ToString(row["ModifiedOn"]),
                    TotalCount = Convert.ToInt32(row["NoOfRows"]),
                    PublisherBook = Convert.ToString(row["Publisher"])
                });
            }
        }
        catch(Exception ex)
        {
            //To Do: Handle Exception
        }

        return books;
    }
    #endregion

    #endregion
}
