using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;

namespace HIS.Class
{
    class InitDatatable
    {
        public static void Init(DataTable table)
        {
            if (table.TableName == "TableList")
            {
                table.Columns.Add("TABLE_NAME", typeof(string));
                table.Columns.Add("TABLE_DESC", typeof(string));
                table.Columns.Add("SYSTEM", typeof(string));
                table.Columns.Add("LOGGING_CYCLE", typeof(string));
                table.Columns.Add("SAVING_PERIOD", typeof(string));
                //table.Columns.Add("CREATED_AT", typeof(string));
                //table.Columns.Add("UPDATED_AT", typeof(string));
                table.Columns.Add("MODIFIED", typeof(string));                
            }
            else if(table.TableName == "TrendInfo")
            {
                table.Columns.Add("SEQ", typeof(Int32));
                table.Columns.Add("SYSTEM", typeof(string));
                DataColumn pkDpName = table.Columns.Add("DP_NAME", typeof(string));
                table.Columns.Add("DP_DESC", typeof(string));
                table.Columns.Add("Y_MIN", typeof(string));
                table.Columns.Add("Y_MAX", typeof(string));
                table.Columns.Add("TB_NAME", typeof(string));
                table.Columns.Add("COL_NAME", typeof(string));
                table.Columns.Add("UPDATED", typeof(string));
                table.Columns.Add("MODIFIED", typeof(string));

                table.PrimaryKey = new DataColumn[] { pkDpName };                
            }
            else if(table.TableName == "Master")
            {
                table.Columns.Add("TB_NAME", typeof(string));
                table.Columns.Add("TB_DESC", typeof(string));
                table.Columns.Add("SYSTEM", typeof(string));
                table.Columns.Add("LOGGING", typeof(string));
                table.Columns.Add("SAVING", typeof(string));
                table.Columns.Add("EMPTY", typeof(string));
                table.Columns.Add("MODIFIED", typeof(string));
            }
            else if(table.TableName == "Detail")
            {
                table.Columns.Add("TB_NAME", typeof(string));
                table.Columns.Add("COL_NAME", typeof(string));
                table.Columns.Add("DP_NAME", typeof(string));
                table.Columns.Add("MODIFIED", typeof(string));
            }
            else if(table.TableName == "AlarmHistory")
            {
                table.Columns.Add("SEQ");
                table.Columns.Add("INSERT_TIME");
                table.Columns.Add("PRIO");
                table.Columns.Add("DP");
                table.Columns.Add("ALM_MSG");
                table.Columns.Add("PV");
                table.Columns.Add("SV");
                table.Columns.Add("ACK_TIME");
                table.Columns.Add("ACK");
                table.Columns.Add("RESET_TIME");
                table.Columns.Add("RESET");
                table.Columns.Add("ACK_NAME");
                table.Columns.Add("PANEL");
            }
            else if (table.TableName == "OperationHistory")
            {
                table.Columns.Add("SEQ");
                table.Columns.Add("INSERT_TIME");
                table.Columns.Add("ACTION");
                table.Columns.Add("DP");
                table.Columns.Add("DP_DESC");
                table.Columns.Add("ELE");
                table.Columns.Add("ELE_DESC");
                table.Columns.Add("PRE_VAL");
                table.Columns.Add("CUR_VAL");
                table.Columns.Add("MODULE");
                table.Columns.Add("OBJECT");
                table.Columns.Add("USER_ID");
            }
            else if(table.TableName == "TrendColor")
            {
                table.Columns.Add("COLOR", typeof(Color));
                table.Columns.Add("ISWORK", typeof(string));
            }
            else if(table.TableName == "TrendData")
            {
                var keys = new DataColumn[1];

                table.Columns.Add(" ", typeof(string));
                table.Columns.Add("VISIBLE", typeof(bool));
                table.Columns.Add("SYSTEM", typeof(string));

                DataColumn pk = new DataColumn();
                pk.DataType = Type.GetType("System.String");
                pk.ColumnName = "DP_NAME";
                table.Columns.Add(pk);
                keys[0] = pk;
                table.PrimaryKey = keys;

                table.Columns.Add("DP_DESC", typeof(string));
                table.Columns.Add("DATETIME", typeof(string));
                table.Columns.Add("CUV", typeof(string));
                table.Columns.Add("CURR", typeof(string));
                table.Columns.Add("MIN", typeof(string));
                table.Columns.Add("MAX", typeof(string));                
            }
            else if(table.TableName == "TrendSimpleInfo")
            {
                table.Columns.Add("SYSTEM", typeof(string));
                DataColumn pkDpName = table.Columns.Add("DP_NAME", typeof(string));
                table.Columns.Add("DP_DESC", typeof(string));
                table.Columns.Add("Y_MIN", typeof(string));
                table.Columns.Add("Y_MAX", typeof(string));
                table.Columns.Add("TB_NAME", typeof(string));
                table.Columns.Add("COL_NAME", typeof(string));
            }
            else if(table.TableName == "TrendGroupInfo")
            {
                table.Columns.Add("SEQ", typeof(int));
                table.Columns.Add("GROUP", typeof(string));
                table.Columns.Add("SYSTEM", typeof(string));
                table.Columns.Add("DP_NAME", typeof(string));
                table.Columns.Add("DP_DESC", typeof(string));
                table.Columns.Add("MODIFIED", typeof(string));
            }
            else if(table.TableName == "GroupMaster")
            {
                table.Columns.Add("SEQ", typeof(string));
                table.Columns.Add("PART", typeof(string));
                table.Columns.Add("GROUP", typeof(string));
                table.Columns.Add("DESC", typeof(string));
                table.Columns.Add("MODIFIED", typeof(string));
            }
            else if (table.TableName == "GroupDetail")
            {               
                table.Columns.Add("GROUP_NAME", typeof(string));              
                table.Columns.Add("DP_NAME", typeof(string));
                table.Columns.Add("DP_DESC", typeof(string));
                table.Columns.Add("MIN", typeof(string));
                table.Columns.Add("MAX", typeof(string));                

            }
            else if (table.TableName == "GroupShowDetail")
            {
                table.Columns.Add("SEQ", typeof(string));
                table.Columns.Add("PART", typeof(string));
                table.Columns.Add("GROUP", typeof(string));
                table.Columns.Add("DESC", typeof(string));
                table.Columns.Add("DP_NAME", typeof(string));
                table.Columns.Add("DP_DESC", typeof(string));
                table.Columns.Add("MIN", typeof(string));
                table.Columns.Add("MAX", typeof(string));
                table.Columns.Add("USE_YN", typeof(string));
                table.Columns.Add("MODIFIED", typeof(string));
            }
            else if(table.TableName == "RealTimeValue")
            {
                var keys = new DataColumn[1];
                DataColumn pk = new DataColumn();
                pk.DataType = Type.GetType("System.String");
                pk.ColumnName = "DP_NAME";
                table.Columns.Add(pk);
                keys[0] = pk;
                table.PrimaryKey = keys;

                table.Columns.Add("DATETIME", typeof(string));
                table.Columns.Add("CURR", typeof(float));
            }
            else if (table.TableName == "TrendDpList")
            {
                table.Columns.Add("CHK", typeof(bool));
                table.Columns.Add("SEQ", typeof(int));
                table.Columns.Add("PAGE_NAME", typeof(string));                

                var keys = new DataColumn[1];
                DataColumn pk = new DataColumn();
                pk.DataType = Type.GetType("System.String");
                pk.ColumnName = "DP_NAME";
                table.Columns.Add(pk);
                keys[0] = pk;
                table.PrimaryKey = keys;
            }
            else if (table.TableName == "FirstTrendGroup")
            {
                table.Columns.Add("CHK", typeof(bool));
                table.Columns.Add("SEQ", typeof(int));
                table.Columns.Add("PAGE_NAME", typeof(string));               

                var keys = new DataColumn[1];
                DataColumn pk = new DataColumn();
                pk.DataType = Type.GetType("System.String");
                pk.ColumnName = "DP_NAME";
                table.Columns.Add(pk);
                keys[0] = pk;
                table.PrimaryKey = keys;
            }
            else if (table.TableName == "SecondTrendGroup")
            {
                table.Columns.Add("CHK", typeof(bool));
                table.Columns.Add("SEQ", typeof(int));
                table.Columns.Add("PAGE_NAME", typeof(string));               

                var keys = new DataColumn[1];
                DataColumn pk = new DataColumn();
                pk.DataType = Type.GetType("System.String");
                pk.ColumnName = "DP_NAME";
                table.Columns.Add(pk);
                keys[0] = pk;
                table.PrimaryKey = keys;
            }
            else if (table.TableName == "TrendGroupDpList")
            {
                table.Columns.Add("CHK", typeof(bool));
                table.Columns.Add("SEQ", typeof(int));
                table.Columns.Add("SYSTEM", typeof(string));

                var keys = new DataColumn[1];
                DataColumn pk = new DataColumn();
                pk.DataType = Type.GetType("System.String");
                pk.ColumnName = "DP_NAME";
                table.Columns.Add(pk);
                keys[0] = pk;
                table.PrimaryKey = keys;
                
            }
            else if (table.TableName == "TrendGroupFirst")
            {
                table.Columns.Add("CHK", typeof(bool));
                table.Columns.Add("SEQ", typeof(int));
                table.Columns.Add("SYSTEM", typeof(string));

                var keys = new DataColumn[1];
                DataColumn pk = new DataColumn();
                pk.DataType = Type.GetType("System.String");
                pk.ColumnName = "DP_NAME";
                table.Columns.Add(pk);
                keys[0] = pk;
                table.PrimaryKey = keys;
               
            }
            else if (table.TableName == "TrendGroupSecond")
            {
                table.Columns.Add("CHK", typeof(bool));
                table.Columns.Add("SEQ", typeof(int));
                table.Columns.Add("SYSTEM", typeof(string));

                var keys = new DataColumn[1];
                DataColumn pk = new DataColumn();
                pk.DataType = Type.GetType("System.String");
                pk.ColumnName = "DP_NAME";
                table.Columns.Add(pk);
                keys[0] = pk;
                table.PrimaryKey = keys;                
            }
            else if (table.TableName == "SmsUser")
            {
                table.Columns.Add("CHK", typeof(bool));
                table.Columns.Add("SEQ", typeof(int));
                table.Columns.Add("USER_NAME", typeof(string));
                table.Columns.Add("PHONE_NUMBER", typeof(string));
                table.Columns.Add("WORK_NO", typeof(string));
                table.Columns.Add("RECEIVE_YN", typeof(bool));
            }
            else if (table.TableName == "SMS_HIST")
            {
                table.Columns.Add("SENT_TIME", typeof(string));
                table.Columns.Add("NAME", typeof(string));
                table.Columns.Add("PHONE", typeof(string));
                table.Columns.Add("MESSAGE", typeof(string));
                table.Columns.Add("SUCCESS", typeof(string));
                table.Columns.Add("ALARM_TIME", typeof(string));
               // table.Columns.Add("DSNT", typeof(string));
            }

        }
    }
}
