using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

public class SQLHelper
{

    private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
        foreach (SqlParameter p in commandParameters)
        {
            //check for derived output value with no value assigned
            if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
            {
                p.Value = DBNull.Value;
            }

            command.Parameters.Add(p);
        }
    }

    private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
    {
        if ((commandParameters == null) || (parameterValues == null))
        {
            //do nothing if we get no data
            return;
        }

        // we must have the same number of values as we pave parameters to put them in
        if (commandParameters.Length != parameterValues.Length)
        {
            throw new ArgumentException("Parameter count does not match Parameter Value count.");
        }

        //iterate through the SqlParameters, assigning the values from the corresponding position in the 
        //value array

        for (int i = 0, j = commandParameters.Length; i < j; i++)
        {
            if (commandParameters[i].DbType == DbType.DateTime)
                commandParameters[i].Value = (parameterValues[i].ToString().Trim() == string.Empty ? "1900-01-01 00:00:00.000" : parameterValues[i].ToString());
            else
                commandParameters[i].Value = parameterValues[i];
        }
    }

    private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
    {
        //if the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        //associate the connection with the command
        command.Connection = connection;

        //set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText;

        //if we were provided a transaction, assign it.
        if (transaction != null)
        {
            command.Transaction = transaction;
        }

        //set the command type
        command.CommandType = commandType;

        //attach the command parameters if they are provided
        if (commandParameters != null)
        {
            AttachParameters(command, commandParameters);
        }

        return;
    }

    public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        try
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(cn, commandType, commandText, commandParameters);
            }
        }
        catch (Exception)
        {
            throw new Exception("SQL ERROR");
        }
    }

    public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
    {
        try
        {
            //connectionString = new Util().Decrypt(connectionString);
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            }
        }
        catch (Exception)
        {
            throw new Exception("SQL ERROR");
        }
    }

    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        try
        {
            //create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

            //finally, execute the command.
            int retval = cmd.ExecuteNonQuery();

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();
            return retval;
        }
        catch (Exception ex)
        {
            throw new Exception("SQL ERROR"+ex.ToString());
        
        }
    }

    public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        try
        {
            //connectionString = new Util().Decrypt(connectionString);
            //create & open a SqlConnection, and dispose of it after we are done.
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteDataset(cn, commandType, commandText, commandParameters);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("SQL ERROR "+ex.ToString());
        }
    }

    public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
    {
        try
        {
            //connectionString = new Util().Decrypt(connectionString);
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("SQL ERROR"+ex.ToString());
        }
    }

    public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        try
        {
            //create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

            //create the DataAdapter & DataSet
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds);

            // detach the SqlParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception("SQL ERROR"+ex.Message);

        }
    }

    public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        try
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteScalar(cn, commandType, commandText, commandParameters);
            }
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
    {
        try
        {
            //connectionString = new Util().Decrypt(connectionString);
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                //pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                //assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            }
        }
        catch (Exception ex)
        {
            return "ERROR"+ex.ToString();
        }
    }

    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        try
        {
            //create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

            //execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();
            return retval;

        }
        catch (Exception ex)
        {
            throw new Exception("SQL ERROR"+ex.Message);
        }
    }
}



public sealed class SqlHelperParameterCache
{
    //*********************************************************************
    //
    // Since this class provides only static methods, make the default constructor private to prevent 
    // instances from being created with "new SqlHelperParameterCache()".
    //
    //*********************************************************************

    private SqlHelperParameterCache() { }

    private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

    //*********************************************************************
    //
    // resolve at run time the appropriate set of SqlParameters for a stored procedure
    // 
    // param name="connectionString" a valid connection string for a SqlConnection 
    // param name="spName" the name of the stored procedure 
    // param name="includeReturnValueParameter" whether or not to include their return value parameter 
    //
    //*********************************************************************

    private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
    {
        using (SqlConnection cn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(spName, cn))
        {
            cn.Open();
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommandBuilder.DeriveParameters(cmd);

            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }

            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count]; ;

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            return discoveredParameters;
        }
    }

    private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
    {
        //deep copy of cached SqlParameter array
        SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

        for (int i = 0, j = originalParameters.Length; i < j; i++)
        {
            clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
        }

        return clonedParameters;
    }

    //*********************************************************************
    //
    // add parameter array to the cache
    //
    // param name="connectionString" a valid connection string for a SqlConnection 
    // param name="commandText" the stored procedure name or T-SQL command 
    // param name="commandParameters" an array of SqlParamters to be cached 
    //
    //*********************************************************************

    public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
    {
        string hashKey = connectionString + ":" + commandText;

        paramCache[hashKey] = commandParameters;
    }

    //*********************************************************************
    //
    // Retrieve a parameter array from the cache
    // 
    // param name="connectionString" a valid connection string for a SqlConnection 
    // param name="commandText" the stored procedure name or T-SQL command 
    // returns an array of SqlParamters
    //
    //*********************************************************************

    public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
    {
        string hashKey = connectionString + ":" + commandText;

        SqlParameter[] cachedParameters = (SqlParameter[])paramCache[hashKey];

        if (cachedParameters == null)
        {
            return null;
        }
        else
        {
            return CloneParameters(cachedParameters);
        }
    }

    //*********************************************************************
    //
    // Retrieves the set of SqlParameters appropriate for the stored procedure
    // 
    // This method will query the database for this information, and then store it in a cache for future requests.
    // 
    // param name="connectionString" a valid connection string for a SqlConnection 
    // param name="spName" the name of the stored procedure 
    // returns an array of SqlParameters
    //
    //*********************************************************************

    public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
    {
        return GetSpParameterSet(connectionString, spName, false);
    }

    //*********************************************************************
    //
    // Retrieves the set of SqlParameters appropriate for the stored procedure
    // 
    // This method will query the database for this information, and then store it in a cache for future requests.
    // 
    // param name="connectionString" a valid connection string for a SqlConnection 
    // param name="spName" the name of the stored procedure 
    // param name="includeReturnValueParameter" a bool value indicating whether the return value parameter should be included in the results 
    // returns an array of SqlParameters
    //
    //*********************************************************************

    public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
    {
        string hashKey = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

        SqlParameter[] cachedParameters;

        cachedParameters = (SqlParameter[])paramCache[hashKey];

        if (cachedParameters == null)
        {
            cachedParameters = (SqlParameter[])(paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter));
        }

        return CloneParameters(cachedParameters);
    }
}
