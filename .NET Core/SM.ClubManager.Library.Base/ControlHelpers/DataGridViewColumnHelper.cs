using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SM.ClubManager.Library.Base.Infrastructure;

namespace SM.ClubManager.Library.Base.ControlHelpers
{
    public class DataGridViewColumnHelper
    {
        public DataGridViewColumn Column { get { return dgColumn; } }

        private DataGridViewColumn dgColumn;

        public DataGridViewColumnHelper(string ColumnName, string HeaderText = "", string DataPropertyName = "", DataGridViewColumnType ColumnType = DataGridViewColumnType.TextBox, DataGridViewAutoSizeColumnMode autoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, bool isVisible = true, bool isReadOnly = false)
        {
            //dgColumn = new DataGridViewColumn();
            switch (ColumnType)
            {
                case DataGridViewColumnType.CheckBox:
                    dgColumn = new DataGridViewCheckBoxColumn();
                    dgColumn.CellTemplate = new DataGridViewCheckBoxCell();
                    break;
                case DataGridViewColumnType.ComboBox:
                    dgColumn = new DataGridViewComboBoxColumn();
                    dgColumn.CellTemplate = new DataGridViewComboBoxCell();
                    break;
                case DataGridViewColumnType.TextBox:
                default:
                    dgColumn = new DataGridViewColumn();
                    dgColumn.CellTemplate = new DataGridViewTextBoxCell();
                    break;
            }
            if (string.IsNullOrEmpty(HeaderText))
            {
                HeaderText = ColumnName;
            }

            if (string.IsNullOrEmpty(DataPropertyName))
            {
                DataPropertyName = ColumnName;
            }
            dgColumn.ReadOnly = isReadOnly;
            dgColumn.Name = ColumnName;
            dgColumn.DataPropertyName = DataPropertyName;
            dgColumn.HeaderText = HeaderText;
            dgColumn.Visible = isVisible;
            dgColumn.AutoSizeMode = autoSizeMode;                        
        }

        public DataGridViewColumnHelper AutoSizeMode(DataGridViewAutoSizeColumnMode autoSizeMode)
        {
            dgColumn.AutoSizeMode = autoSizeMode;
            return this;
        }

        public DataGridViewColumnHelper IsVisible(bool isVisible = true)
        {
            dgColumn.Visible = isVisible;
            return this;
        }   
    }
}
