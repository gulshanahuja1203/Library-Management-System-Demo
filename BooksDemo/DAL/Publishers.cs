using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
/// <summary>
/// Summary description for Class1
/// </summary>
public class Publishers
{
    #region Basic Functionality

    #region Variable Declaration

    //Variable to store Database object to interact with database
    private Database db;

    #endregion

    #region Constructors
    //Initialize new instance of Publishers class

    public Publishers()
    {
        this.db = DatabaseFactory.CreateDatabase();
    }

    public Publishers(int publisherId)
    {
        this.db = DatabaseFactory.CreateDatabase();
        this.PublisherId = publisherId;
    }

    #endregion

    #region Properties
    //Get and Set 
    public int PublisherId
    {
        get; set;
    }

    public String PublisherName
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
    #region Load Details For Publishers
    //Load Details for Publishers
    //Returns True if Load Operation is successful else False
    public bool Load()
    {
        try
        {
            if (this.PublisherId != 0)
            {
                DbCommand com = this.db.GetStoredProcCommand("PublishersGetDetails");
                this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);
                DataSet ds = this.db.ExecuteDataSet(com);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    this.PublisherId = Convert.ToInt32(dt.Rows[0]["PublisherId"]);
                    this.PublisherName = Convert.ToString(dt.Rows[0]["PublisherName"]);
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

    #region Insert Details for Publishers
    //Insert Details for Publishers if Publisherid = 0
    //Else Update Details for Publishers
    //Returns True if Operation is successful else False
    public bool Save()
    {
        if (this.PublisherId == 0)
        {
            return this.Insert();
        }
        else
        {
            if (this.PublisherId > 0)
            {
                return this.Update();
            }
            else
            {
                this.PublisherId = 0;
                return false;
            }
        }
    }

    //Insert Details for Publishers
    //Save Newly Created Id in PublisherId
    //Return True if operation is successful else False
    private bool Insert()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("PublishersInsert");
            this.db.AddOutParameter(com, "PublisherId", DbType.Int32, 1024);

            if (!String.IsNullOrEmpty(this.PublisherName))
            {
                this.db.AddInParameter(com, "PublisherName", DbType.String, this.PublisherName);
            }
            else
            {
                this.db.AddInParameter(com, "PublisherName", DbType.String, DBNull.Value);
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
            this.PublisherId = Convert.ToInt32(this.db.GetParameterValue(com, "PublisherId"));
        }
        catch (Exception ex)
        {
            //To Do: Handle Exception
            return false;
        }
        return this.PublisherId > 0; //Return whether Id was returned
    }

    #endregion

    #region Update Details For Publishers
    private bool Update()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("PublishersUpdate");
            this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);

            if (!String.IsNullOrEmpty(this.PublisherName))
            {
                this.db.AddInParameter(com, "PublisherName", DbType.String, this.PublisherName);
            }
            else
            {
                this.db.AddInParameter(com, "PublisherName", DbType.String, DBNull.Value);
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

    #region Delete Details of Publishers for provided PublisherId
    //Delete Details of Publishers for provided PublisherId
    //Return True if operation is successful else False
    public bool Delete()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("PublishersDelete");
            this.db.AddInParameter(com, "PublisherId", DbType.Int32, this.PublisherId);
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

    #region Get List of Publishers
    //get list of Publishers
    //Returns DataSet of result
    public List<Publishers> GetList()
    {
        DataSet ds = null;
        List<Publishers> publishers = new List<Publishers>();
        try
        {

            DbCommand com = db.GetStoredProcCommand("PublishersGetList");
            ds = db.ExecuteDataSet(com);
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                publishers.Add(new Publishers
                {
                    PublisherId = Convert.ToInt32(row["PublisherId"]),
                    PublisherName = Convert.ToString(row["PublisherName"])
                });
            }
        }
        catch (Exception ex)
        {

        }
        return publishers;
    }
    #endregion

    #endregion

    #endregion
}
