using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HIS.Class
{
    class InitDataGridView
    {
        public static void dataGridViewInit(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 25;
            dgv.ReadOnly = true;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;
            dgv.AllowUserToResizeRows = false; //row  높이조절 막기
            dgv.AllowUserToAddRows = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 25;
            dgv.Dock = DockStyle.Fill;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.BackgroundColor = Color.FromArgb(60, 60, 60);
            dgv.ForeColor = Color.White;
            
            
        }


        public static void AutoSettingDatagridView(DataGridView dgv, List<int> fillColumns, List<int> hideColumns)
        {
            InitDataGridView.dataGridViewInit(dgv);

            for (int i = 1; i < dgv.ColumnCount; i++)
            {
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            if (fillColumns.Count > 0)
            {
                foreach (int item in fillColumns)
                {
                    dgv.Columns[item].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            if (hideColumns.Count > 0)
            {
                foreach (int item in hideColumns)
                {
                    dgv.Columns[item].Visible = false;
                }
            }
        }
    }
}
