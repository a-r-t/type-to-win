using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Text;

namespace type_to_win.Database
{
    /// <summary>
    /// Handles Database Connecting and Reading to Game's Database SQLite file
    /// </summary>
    public class DatabaseHandler
    {
        public SQLiteConnection DbConnection;   // Database connection instance

        /// <summary>
        /// DatabaseHandler Constructor
        /// </summary>
        public DatabaseHandler()
        {
            // Create Database connection string
            string databaseFile = ConfigurationManager.AppSettings["databaseFile"];
            DbConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", databaseFile));
        }

        /// <summary>
        /// Runs a "non query" command against the database (e.g. create, insert)
        /// </summary>
        /// <param name="sql">sql statement to run</param>
        /// <param name="dbParams">sql params</param>
        /// <returns>index of item that was modified</returns>
        public int RunNonQuery(string sql, Dictionary<string, object> dbParams = null)
        {
            // open database connection
            DbConnection.Open();

            // create new sql command
            SQLiteCommand command = new SQLiteCommand(sql, DbConnection);

            // add sql parameters
            if (dbParams != null)
            {
                foreach (KeyValuePair<string, object> pair in dbParams)
                {
                    command.Parameters.AddWithValue(pair.Key, pair.Value);
                }
            }

            // run query
            int result = command.ExecuteNonQuery();

            // close database connection
            DbConnection.Close();
            return result;
        }

        /// <summary>
        /// Runs a query against database (e.g. select)
        /// </summary>
        /// <param name="sql">sql statement to run</param>
        /// <param name="dbParams">sql params</param>
        /// <returns>SQLite Data Reader object containing returned records from query execution</returns>
        public SQLiteDataReader RunQuery(string sql, Dictionary<string, object> dbParams = null)
        {
            // open database connection
            DbConnection.Open();

            // create new sql command
            SQLiteCommand command = new SQLiteCommand(sql, DbConnection);

            // add sql parameters
            if (dbParams != null)
            {
                foreach (KeyValuePair<string, object> pair in dbParams)
                {
                    command.Parameters.AddWithValue(pair.Key, pair.Value);
                }
            }

            // run query
            SQLiteDataReader result = command.ExecuteReader();
            return result;

        }
    }
}
