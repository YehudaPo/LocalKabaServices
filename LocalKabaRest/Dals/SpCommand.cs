using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalKabaRest.Dals
{
    public class SpCommand
    {
        private DbConnection con;
        private DbCommand cmd;

        public SpCommand(DbConnection _con, String SpName)
        {
            cmd = new SqlCommand(SpName);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = _con;
            con = _con;
        }

        public SpCommand(DbConnection _con, DbTransaction _trans, String SpName)
        {
            cmd = new SqlCommand(SpName);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = _con;
            cmd.Transaction = _trans;
            con = _con;
        }

        public void AddParameter(DbType dbType, String paramName, object value)
        {
            DbParameter parameter = cmd.CreateParameter();
            parameter.DbType = dbType;
            parameter.ParameterName = paramName;
            parameter.Value = value;
            cmd.Parameters.Add(parameter);
        }

        public DbDataReader Execute()
        {
            try
            {
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                General.TraceUtility tu = new General.TraceUtility("TraceLog");
                tu.TraceError(null, ex.ToString());
            }
            return null;
        }
    }
}