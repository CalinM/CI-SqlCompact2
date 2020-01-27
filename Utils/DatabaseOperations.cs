using System;

using Common;
using System.Data.SqlServerCe;

namespace Utils
{
    public static class DatabaseOperations
    {
        public static OperationResult CreateDatabase()
        {
            var result = new OperationResult();

            try
            {
                using (var engine = new SqlCeEngine(Constants.ConnectionString))
                {
                    engine.CreateDatabase();

                    using (var conn = new SqlCeConnection(Constants.ConnectionString))
                    {
                        conn.Open();

                        var cmd = conn.CreateCommand();

                        foreach (var dbDefStep in Constants.DatabaseDef)
                        {
                            cmd.CommandText = dbDefStep;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return result.FailWithMessage(e);
            }

            return result;
        }

        public static OperationResult CreateField(string tableName, string fieldName, string fieldType)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SqlCeConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    SqlCeCommand cmd;

                    var sqlString = @"
                        SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @columnName";

                    cmd = new SqlCeCommand(sqlString, conn);
                    cmd.Parameters.AddWithValue("@tableName", tableName);
                    cmd.Parameters.AddWithValue("@columnName", fieldName);

                    if (cmd.ExecuteScalar() != null)
                        return result.FailWithMessage(string.Format("Field '{0}' already exists in the '{1}' table!", fieldName, tableName));

                    sqlString = string.Format(@"
                        ALTER TABLE {0} ADD COLUMN {1} {2}",
                        tableName,
                        fieldName,
                        fieldType);

                    cmd = new SqlCeCommand(sqlString, conn);
                    cmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                return result.FailWithMessage(e);
            }

            return result;
        }
    }
}
