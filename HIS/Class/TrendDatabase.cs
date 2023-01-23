using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Class
{
    class TrendDatabase
    {
        public static OracleConnection OracleConn = null;

        public static OracleConnection CreateDatabase()
        {
            Util util = new Util(@"C:\system_db\config\info.ini");
            string sHost = util.GetIni("ORACLE", "host");
            string sUser = util.GetIni("ORACLE", "user");
            string sPwd = util.GetIni("ORACLE", "pwd");
            string sService = util.GetIni("ORACLE", "service");
            string oradb = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sHost})(PORT=1521)))" +
                $"(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={sService})));User ID={sUser};Password={sPwd};Connection Timeout=30;";

            try
            {
                OracleConn = new OracleConnection(oradb);
                OracleConn.Open();
            }
            catch
            {
                OracleConn = null;
            }

            return OracleConn;

        }

        public static bool Open()
        {
            if (TrendDatabase.OracleConn.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    TrendDatabase.OracleConn.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public static void Close()
        {
            if (TrendDatabase.OracleConn.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    TrendDatabase.OracleConn.Close();
                }
                catch
                {

                }
            }
        }
    }
}
