using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
/// <summary>
/// Summary description for Categories
/// </summary>
public class Categories
{
    #region Basic Functionality

    #region Variable Declaration
    //Variable to store Database object to interact with database
    private Database db;
    #endregion

    #region Constructors
    //Initialize new instance of Categories class

    public Categories()
    {
        this.db = DatabaseFactory.CreateDatabase();
    }

    public Categories(int categoryId)
    {
        this.db = DatabaseFactory.CreateDatabase();
        this.CategoryId = categoryId;
    }

    #endregion

    #region Properties
    //Get and Set for CategoryId
    public int CategoryId
    {
        get; set;
    }

    public string CategoryName
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
    
    #region Load Details For Categories
    //Load Details for Categories
    //Returns True if Load Operation is successful else False
    public bool Load()
    {
        try
        {
            if (this.CategoryId != 0)
            {
                DbCommand com = this.db.GetStoredProcCommand("CategoriesGetDetails");
                this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);
                DataSet ds = this.db.ExecuteDataSet(com);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    this.CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
                    this.CategoryName = Convert.ToString(dt.Rows[0]["CategoryName"]);
                    this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    this.CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]);
                    this.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                    this.ModifiedBy = Convert.ToInt32(dt.Rows[0]["ModifiedBy"]);
                    this.ModifiedOn = Convert.ToDateTime(dt.Rows[0]["ModifiedOn"]);
                }
            }
        return false;
        }
        catch(Exception ex)
        {
            //To Do: Handle Exception
            return false;
        }

    }
    #endregion

    #region Insert Details for Categories
    //Insert Details for Categories if CategoryId = 0
    //Else Update Details for Categories
    //Returns True if Operation is successful else False
    public bool Save()
    {
        if (this.CategoryId == 0)
        {
            return this.Insert();
        }
        else
        {
            if (this.CategoryId > 0)
            {
                return this.Update();
            }
            else
            {
                this.CategoryId = 0;
                return false;
            }
        }
    }

    //Insert Details for Categories
    //Save Newly Created Id in CategoryId
    //Return True if operation is successful else False
    private bool Insert()
    {
        try {
            DbCommand com = this.db.GetStoredProcCommand("CategoriesInsert");
            this.db.AddOutParameter(com, "CategoryId", DbType.Int32, 1024);

            if (!String.IsNullOrEmpty(this.CategoryName))
            {
                this.db.AddInParameter(com, "CategoryName", DbType.String, this.CategoryName);
            }
            else
            {
                this.db.AddInParameter(com, "CategoryName", DbType.String, DBNull.Value);
            }

            this.db.AddInParameter(com, "IsActive", DbType.Boolean, this.IsActive);

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
            this.CategoryId = Convert.ToInt32(this.db.GetParameterValue(com, "CategoryId"));
        }
        catch(Exception ex)
        {
            //To Do: Handle Exception
            return false;
        }
        return this.CategoryId > 0; //Return whether Id was returned
    }

    #endregion

    #region Update Details For Categories
    private bool Update()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("CategoriesUpdate");
            this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);

            if (!String.IsNullOrEmpty(this.CategoryName))
            {
                this.db.AddInParameter(com, "CategoryName", DbType.String, this.CategoryName);
            }
            else
            {
                this.db.AddInParameter(com, "CategoryName", DbType.String, DBNull.Value);
            }

            this.db.AddInParameter(com, "IsActive", DbType.Boolean, this.IsActive);

            if(this.ModifiedBy > 0)
            {
                this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
            }
            else
            {
                this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, DBNull.Value);
            }

            if(this.ModifiedOn > DateTime.MinValue)
            {
                this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, this.ModifiedOn);
            }
            else
            {
                this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, DBNull.Value);
            }
            this.db.ExecuteNonQuery(com);
        }
        catch(Exception ex)
        {
            //To Do: Handle Exception
            return false;
        }
        return true;
    }
    #endregion

    #region Delete Details of Categories for provided CategoryId
    //Delete Details of Categories for provided CategoryId
    //Return True if operation is successful else False
    public bool Delete()
    {
        try
        {
            DbCommand com = this.db.GetStoredProcCommand("CategoriesDelete");
            this.db.AddInParameter(com, "CategoryId", DbType.Int32, this.CategoryId);
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

    #region Get List of Categories
    //get list of Categories
    //Returns DataSet of result
    public List<Categories> GetList()
    {
        DataSet ds = null;
        List<Categories> categories = new List<Categories>();
        try
        {
            DbCommand com = db.GetStoredProcCommand("CategoriesGetList");
            ds = db.ExecuteDataSet(com);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                categories.Add(new Categories
                {
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    CategoryName = Convert.ToString(row["CategoryName"])
                });
            }
        }
        catch(Exception ex)
        {

        }
        return categories;
    }
    #endregion

    #endregion

    #endregion
}
