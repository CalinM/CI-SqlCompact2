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
    }
}
