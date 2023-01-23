using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace HIS.Class
{
    class TableManager
    {
        static public bool CreateTrendTable(string tableName) //기본테이블 생성(ex HMI_TREND_0001_1SEC, HMI_TREND_0002_3SEC...
        {
            bool result = false;
            string query = string.Empty;           

            query += "CREATE TABLE " + tableName + "( ";
            query += "SYSTEM VARCHAR2(30), INSERT_TIME TIMESTAMP(3) PRIMARY KEY, VAL_0001 NUMERIC(20, 6), ";
            query += "VAL_0002 NUMERIC(20, 6), VAL_0003 NUMERIC(20, 6), VAL_0004 NUMERIC(20, 6), VAL_0005 NUMERIC(20, 6), ";
            query += "VAL_0006 NUMERIC(20, 6), VAL_0007 NUMERIC(20, 6), VAL_0008 NUMERIC(20, 6), VAL_0009 NUMERIC(20, 6), ";
            query += "VAL_0010 NUMERIC(20, 6), VAL_0011 NUMERIC(20, 6), VAL_0012 NUMERIC(20, 6), VAL_0013 NUMERIC(20, 6), ";
            query += "VAL_0014 NUMERIC(20, 6), VAL_0015 NUMERIC(20, 6), VAL_0016 NUMERIC(20, 6), VAL_0017 NUMERIC(20, 6), ";
            query += "VAL_0018 NUMERIC(20, 6), VAL_0019 NUMERIC(20, 6), VAL_0020 NUMERIC(20, 6), VAL_0021 NUMERIC(20, 6), ";
            query += "VAL_0022 NUMERIC(20, 6), VAL_0023 NUMERIC(20, 6), VAL_0024 NUMERIC(20, 6), VAL_0025 NUMERIC(20, 6), ";
            query += "VAL_0026 NUMERIC(20, 6), VAL_0027 NUMERIC(20, 6), VAL_0028 NUMERIC(20, 6), VAL_0029 NUMERIC(20, 6), ";
            query += "VAL_0030 NUMERIC(20, 6), VAL_0031 NUMERIC(20, 6), VAL_0032 NUMERIC(20, 6), VAL_0033 NUMERIC(20, 6), ";
            query += "VAL_0034 NUMERIC(20, 6), VAL_0035 NUMERIC(20, 6), VAL_0036 NUMERIC(20, 6), VAL_0037 NUMERIC(20, 6), ";
            query += "VAL_0038 NUMERIC(20, 6), VAL_0039 NUMERIC(20, 6), VAL_0040 NUMERIC(20, 6), VAL_0041 NUMERIC(20, 6), ";
            query += "VAL_0042 NUMERIC(20, 6), VAL_0043 NUMERIC(20, 6), VAL_0044 NUMERIC(20, 6), VAL_0045 NUMERIC(20, 6), ";
            query += "VAL_0046 NUMERIC(20, 6), VAL_0047 NUMERIC(20, 6), VAL_0048 NUMERIC(20, 6), VAL_0049 NUMERIC(20, 6), ";
            query += "VAL_0050 NUMERIC(20, 6), VAL_0051 NUMERIC(20, 6), VAL_0052 NUMERIC(20, 6), VAL_0053 NUMERIC(20, 6), ";
            query += "VAL_0054 NUMERIC(20, 6), VAL_0055 NUMERIC(20, 6), VAL_0056 NUMERIC(20, 6), VAL_0057 NUMERIC(20, 6), ";
            query += "VAL_0058 NUMERIC(20, 6), VAL_0059 NUMERIC(20, 6), VAL_0060 NUMERIC(20, 6), VAL_0061 NUMERIC(20, 6), ";
            query += "VAL_0062 NUMERIC(20, 6), VAL_0063 NUMERIC(20, 6), VAL_0064 NUMERIC(20, 6), VAL_0065 NUMERIC(20, 6), ";
            query += "VAL_0066 NUMERIC(20, 6), VAL_0067 NUMERIC(20, 6), VAL_0068 NUMERIC(20, 6), VAL_0069 NUMERIC(20, 6), ";
            query += "VAL_0070 NUMERIC(20, 6), VAL_0071 NUMERIC(20, 6), VAL_0072 NUMERIC(20, 6), VAL_0073 NUMERIC(20, 6), ";
            query += "VAL_0074 NUMERIC(20, 6), VAL_0075 NUMERIC(20, 6), VAL_0076 NUMERIC(20, 6), VAL_0077 NUMERIC(20, 6), ";
            query += "VAL_0078 NUMERIC(20, 6), VAL_0079 NUMERIC(20, 6), VAL_0080 NUMERIC(20, 6), VAL_0081 NUMERIC(20, 6), ";
            query += "VAL_0082 NUMERIC(20, 6), VAL_0083 NUMERIC(20, 6), VAL_0084 NUMERIC(20, 6), VAL_0085 NUMERIC(20, 6), ";
            query += "VAL_0086 NUMERIC(20, 6), VAL_0087 NUMERIC(20, 6), VAL_0088 NUMERIC(20, 6), VAL_0089 NUMERIC(20, 6), ";
            query += "VAL_0090 NUMERIC(20, 6), VAL_0091 NUMERIC(20, 6), VAL_0092 NUMERIC(20, 6), VAL_0093 NUMERIC(20, 6), ";
            query += "VAL_0094 NUMERIC(20, 6), VAL_0095 NUMERIC(20, 6), VAL_0096 NUMERIC(20, 6), VAL_0097 NUMERIC(20, 6), ";
            query += "VAL_0098 NUMERIC(20, 6), VAL_0099 NUMERIC(20, 6), VAL_0100 NUMERIC(20, 6) )";

            try
            {
                if (Database.OracleConn.State == System.Data.ConnectionState.Closed)
                    Database.OracleConn.Open();

                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                Database.OracleConn.Close();
            }

            return result;
        }

    }
}
