using Common;
using System;
using System.Data.SQLite;

namespace DAL
{
    public static class DatabaseOperations
    {
        public static OperationResult ExecuteSqlString(string sqlString)
        {
            var result = new OperationResult();

            try
            {
                using (var conn = new SQLiteConnection(Constants.ConnectionString))
                {
                    conn.Open();

                    var cmd = new SQLiteCommand(sqlString, conn);
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
