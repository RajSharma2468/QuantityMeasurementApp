using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Exceptions;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Repositories
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private string _connectionString;
        private ILogger<QuantityMeasurementDatabaseRepository> _logger;
        private List<SqlConnection> _connectionPool;
        private int _activeConnections;
        
        public QuantityMeasurementDatabaseRepository(IConfiguration configuration, ILogger<QuantityMeasurementDatabaseRepository> logger)
        {
            this._logger = logger;
            this._connectionPool = new List<SqlConnection>();
            this._activeConnections = 0;
            
            IConfigurationSection dbSection = configuration.GetSection("Database");
            if (dbSection != null)
            {
                this._connectionString = dbSection["ConnectionString"];
            }
            
            if (string.IsNullOrEmpty(this._connectionString))
            {
                this._connectionString = "Server=(localdb)\\mssqllocaldb;Database=QuantityMeasurementDB;Trusted_Connection=True;";
            }
            
            this.CreateDatabase();
            this.CreateTables();
            
            this._logger.LogInformation("DatabaseRepository started");
        }
        
        private SqlConnection GetNewConnection()
        {
            SqlConnection conn = new SqlConnection(this._connectionString);
            conn.Open();
            return conn;
        }
        
        private void CreateDatabase()
        {
            SqlConnection masterConn = null;
            
            try
            {
                string masterConnString = this._connectionString.Replace("QuantityMeasurementDB", "master");
                masterConn = new SqlConnection(masterConnString);
                masterConn.Open();
                
                string checkDbSql = "SELECT COUNT(*) FROM sys.databases WHERE name = 'QuantityMeasurementDB'";
                SqlCommand checkCmd = new SqlCommand(checkDbSql, masterConn);
                int dbExists = (int)checkCmd.ExecuteScalar();
                
                if (dbExists == 0)
                {
                    SqlCommand createCmd = new SqlCommand("CREATE DATABASE QuantityMeasurementDB", masterConn);
                    createCmd.ExecuteNonQuery();
                    this._logger.LogInformation("Database created");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Database creation error");
            }
            finally
            {
                if (masterConn != null)
                {
                    masterConn.Close();
                    masterConn.Dispose();
                }
            }
        }
        
        private void CreateTables()
        {
            SqlConnection conn = null;
            
            try
            {
                conn = this.GetNewConnection();
                
                string createSql = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='QuantityMeasurements')
                    CREATE TABLE QuantityMeasurements (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Value FLOAT NOT NULL,
                        UnitType NVARCHAR(50) NOT NULL,
                        UnitName NVARCHAR(50) NOT NULL,
                        Operation NVARCHAR(50) NOT NULL,
                        Result FLOAT NULL,
                        OperationDate DATETIME NOT NULL
                    )";
                
                SqlCommand createCmd = new SqlCommand(createSql, conn);
                createCmd.ExecuteNonQuery();
                
                this._logger.LogInformation("Tables created");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Table creation error");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        
        public void Save(QuantityMeasurementEntity entity)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            
            try
            {
                conn = this.GetNewConnection();
                trans = conn.BeginTransaction();
                
                string insertSql = @"
                    INSERT INTO QuantityMeasurements 
                    (Value, UnitType, UnitName, Operation, Result, OperationDate)
                    VALUES 
                    (@Value, @UnitType, @UnitName, @Operation, @Result, @OperationDate);
                    
                    SELECT SCOPE_IDENTITY()";
                
                SqlCommand cmd = new SqlCommand(insertSql, conn, trans);
                
                cmd.Parameters.AddWithValue("@Value", entity.Value);
                cmd.Parameters.AddWithValue("@UnitType", entity.UnitType);
                cmd.Parameters.AddWithValue("@UnitName", entity.UnitName);
                cmd.Parameters.AddWithValue("@Operation", entity.Operation);
                
                if (entity.Result.HasValue)
                {
                    cmd.Parameters.AddWithValue("@Result", entity.Result.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Result", DBNull.Value);
                }
                
                cmd.Parameters.AddWithValue("@OperationDate", entity.OperationDate);
                
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    entity.Id = Convert.ToInt32(result);
                }
                
                trans.Commit();
                this._logger.LogInformation("Saved measurement " + entity.Id);
            }
            catch (Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                this._logger.LogError(ex, "Save error");
                throw new DatabaseException("Failed to save: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        
        public List<QuantityMeasurementEntity> GetAll()
        {
            List<QuantityMeasurementEntity> list = new List<QuantityMeasurementEntity>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            
            try
            {
                conn = this.GetNewConnection();
                
                string sql = "SELECT Id, Value, UnitType, UnitName, Operation, Result, OperationDate FROM QuantityMeasurements";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    QuantityMeasurementEntity e = new QuantityMeasurementEntity();
                    e.Id = reader.GetInt32(0);
                    e.Value = reader.GetDouble(1);
                    e.UnitType = reader.GetString(2);
                    e.UnitName = reader.GetString(3);
                    e.Operation = reader.GetString(4);
                    
                    if (!reader.IsDBNull(5))
                    {
                        e.Result = reader.GetDouble(5);
                    }
                    
                    e.OperationDate = reader.GetDateTime(6);
                    
                    list.Add(e);
                }
                
                reader.Close();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "GetAll error");
                throw new DatabaseException("Failed to get data: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            
            return list;
        }
        
        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            List<QuantityMeasurementEntity> list = new List<QuantityMeasurementEntity>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            
            try
            {
                conn = this.GetNewConnection();
                
                string sql = "SELECT Id, Value, UnitType, UnitName, Operation, Result, OperationDate FROM QuantityMeasurements WHERE Operation = @Operation";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Operation", operation);
                
                reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    QuantityMeasurementEntity e = new QuantityMeasurementEntity();
                    e.Id = reader.GetInt32(0);
                    e.Value = reader.GetDouble(1);
                    e.UnitType = reader.GetString(2);
                    e.UnitName = reader.GetString(3);
                    e.Operation = reader.GetString(4);
                    
                    if (!reader.IsDBNull(5))
                    {
                        e.Result = reader.GetDouble(5);
                    }
                    
                    e.OperationDate = reader.GetDateTime(6);
                    
                    list.Add(e);
                }
                
                reader.Close();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "GetByOperation error");
                throw new DatabaseException("Failed to get data: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            
            return list;
        }
        
        public List<QuantityMeasurementEntity> GetByUnitType(string unitType)
        {
            List<QuantityMeasurementEntity> list = new List<QuantityMeasurementEntity>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            
            try
            {
                conn = this.GetNewConnection();
                
                string sql = "SELECT Id, Value, UnitType, UnitName, Operation, Result, OperationDate FROM QuantityMeasurements WHERE UnitType = @UnitType";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UnitType", unitType);
                
                reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    QuantityMeasurementEntity e = new QuantityMeasurementEntity();
                    e.Id = reader.GetInt32(0);
                    e.Value = reader.GetDouble(1);
                    e.UnitType = reader.GetString(2);
                    e.UnitName = reader.GetString(3);
                    e.Operation = reader.GetString(4);
                    
                    if (!reader.IsDBNull(5))
                    {
                        e.Result = reader.GetDouble(5);
                    }
                    
                    e.OperationDate = reader.GetDateTime(6);
                    
                    list.Add(e);
                }
                
                reader.Close();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "GetByUnitType error");
                throw new DatabaseException("Failed to get data: " + ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            
            return list;
        }
        
        public int GetTotalCount()
        {
            int count = 0;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            
            try
            {
                conn = this.GetNewConnection();
                
                string sql = "SELECT COUNT(*) FROM QuantityMeasurements";
                cmd = new SqlCommand(sql, conn);
                
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    count = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "GetTotalCount error");
                throw new DatabaseException("Failed to get count: " + ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            
            return count;
        }
        
        public void DeleteAll()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            
            try
            {
                conn = this.GetNewConnection();
                
                string sql = "DELETE FROM QuantityMeasurements";
                cmd = new SqlCommand(sql, conn);
                
                int rows = cmd.ExecuteNonQuery();
                this._logger.LogInformation("Deleted " + rows + " measurements");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "DeleteAll error");
                throw new DatabaseException("Failed to delete: " + ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        
        public Dictionary<string, object> GetPoolStatistics()
        {
            Dictionary<string, object> stats = new Dictionary<string, object>();
            stats.Add("Repository Type", "Database");
            stats.Add("Total Measurements", this.GetTotalCount());
            return stats;
        }
        
        public void ReleaseResources()
        {
            foreach (SqlConnection conn in this._connectionPool)
            {
                try
                {
                    conn.Close();
                    conn.Dispose();
                }
                catch (Exception) { }
            }
            this._connectionPool.Clear();
            this._logger.LogInformation("Released database resources");
        }
    }
}