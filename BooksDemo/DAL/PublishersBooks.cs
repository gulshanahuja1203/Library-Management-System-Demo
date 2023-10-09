using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for PublishersBooks
/// </summary>
public class PublishersBooks
{
    #region Basic Functionality

    #region Variable Declaration

    //Variable to store Database object to interact with database
    private Database db;

    #endregion

    #region Constructors
    //Initialize new instance of PublishersBooks class

    public PublishersBooks()
    {
        this.db = DatabaseFactory.CreateDatabase();
    }

    public PublishersBooks(int publisherBookId)
    {
        this.db = DatabaseFactory.CreateDatabase();
        this.PublisherBookId = publisherBookId;
    }

    #endregion

    #region Properties
    //Get and Set
    public int PublisherBookId
    {
        get; set;
    }

    public int PublisherId
    {
        get; set;
    }

    public int BookId
    {
        get; set;
    }

    public bool IsActive
    {
        get; set;
    }

    public int CreatedBy
    {
        get; set;
    }

    public DateTime CreatedOn
    {
        get; set;
    }

    public int ModifiedBy
    {
        get; set;
    }

    public DateTime ModifiedOn
    {
        get; set;
    }
    #endregion

    #region Actions

    #region Load Details For PublishersBooks
    //Load Details for PublishersBooks
    //Returns True if Load Operation is successful else False
    public bool Load()
    {
        try
        {
            if (this.PublisherBookId != 0)
            {
                DbCommand com = this.db.GetStoredProcCommand("PublishersBooksGetDetails");
                this.db.AddInParameter(com, "PublisherBookId", DbType.Int32, this.PublisherBookId);
                DataSet ds = this.db.ExecuteDataSet(com);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    this.PublisherBookId = Convert.ToInt32(dt.Rows[0]["PublisherBookId"]);
                    this.PublisherId = Convert.ToInt32(dt.Rows[0]["PublisherId"]);
                    this.BookId = Convert.ToInt32(dt.Rows[0]["BookId"]);
                    this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    this.CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]);
                    this.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                    this.ModifiedBy = Convert.ToInt32(dt.Rows[0]["ModifiedBy"]);
                    this.ModifiedOn = Convert.ToDateTime(dt.Rows[0]["ModifiedOn"]);
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

    #region Insert Details for PublishersBooks
    //Insert Details for PublishersBooks if PublisherBookId = 0
    //Else Update Details for PublishersBooks
    //Returns True if Operation is successful else False
    public bool Save()
    {
        if (this.PublisherBookId == 0)
        {
            return this.Insert();
        }
        else
        {
            if (this.PublisherBookId > 0)
            {
                return this.Update();
            }
            else
            {
                this.PublisherBookId = 0;
                return false;
            }
        }
    }

    //Insert Details for PublishersBooks
    //Save Newly Created Id in PublisherBookId
    //Return True if operation is successful else False
    private bool Insert()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("PublishersBooksInsert");
            this.db.AddOutParameter(com, "PublisherBookId", DbType.Int32, 1024);

            if(this.PublisherId > 0)
            {
                this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);
            }
            else
            {
                this.db.AddInParameter(com, "PublisherId", DbType.Int32, DBNull.Value);
            }

            if(this.BookId > 0)
            {
                this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);
            }
            else
            {
                this.db.AddInParameter(com, "BookId", DbType.Int32, DBNull.Value);
            }

            this.db.AddInParameter(com, "IsActive",DbType.Boolean, this.IsActive);

            if (this.CreatedBy > 0)
            {
                this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
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
            this.PublisherBookId = Convert.ToInt32(this.db.GetParameterValue(com, "PublisherBookId"));
        }
        catch (Exception ex)
        {
            //To Do: Handle Exception
            return false;
        }
        return this.PublisherBookId > 0; //Return whether Id was returned
    }

    #endregion

    #region Update Details For PublishersBooks
    private bool Update()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("PublishersBooksUpdate");
            this.db.AddInParameter(com, "PublisherBookId", DbType.Int32, this.PublisherBookId);

            if(this.PublisherId > 0)
            {
                this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);
            }
            else
            {
                this.db.AddInParameter(com, "PublisherId", DbType.Int32, DBNull.Value);
            }

            if(this.BookId > 0)
            {
                this.db.AddInParameter(com, "BookId", DbType.Int32, this.BookId);
            }
            else
            {
                this.db.AddInParameter(com, "BookId", DbType.Int32, DBNull.Value);
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

    #region Delete Details of PublishersBooks for provided PublisherBookId
    //Delete Details of PublishersBooks for provided PublisherBookId
    //Return True if operation is successful else False
    public bool Delete()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("PublishersBooksDelete");
            this.db.AddInParameter(com, "PublisherBookId", DbType.Int32, this.PublisherBookId);
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

    #region Get List of PublishersBooks
    //get list of PublishersBooks
    //Returns DataSet of result
    public DataSet GetList()
    {
        DataSet ds = null;
        try
        {
            DbCommand com = db.GetStoredProcCommand("PublishersBooksGetList");
            ds = db.ExecuteDataSet(com);
        }
        catch (Exception ex)
        {

        }
        return ds;
    }
    #endregion

    #endregion

    #endregion
}
