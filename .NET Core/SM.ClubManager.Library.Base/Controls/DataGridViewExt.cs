using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SM.ClubManager.Library.Base.Controls
{
    public partial class DataGridViewExt : UserControl
    {
        private System.Windows.Forms.ContextMenuStrip contextGridMenu;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem1;

        private bool _enableCopyPastContextMenu;

        public bool EnableCopyPasteContextMenu
        {
            get
            {
                return _enableCopyPastContextMenu;
            }
            set
            {
                
                _enableCopyPastContextMenu = value;

                if (value == false)
                {
                    DisableContextMenu();
                }
                else
                {
                    InitializeConextMenuStrip();
                }
            }
        }

        private void DisableContextMenu()
        {
          
            if (cutToolStripMenuItem != null)
            {
                cutToolStripMenuItem.Click -= new System.EventHandler(cutToolStripMenuItem_Click);
                cutToolStripMenuItem.Dispose();
                cutToolStripMenuItem = null;
            }
            if (copyToolStripMenuItem != null)
            {
                this.copyToolStripMenuItem.Click -= new System.EventHandler(this.copyToolStripMenuItem_Click);
                copyToolStripMenuItem.Dispose();
                copyToolStripMenuItem = null;
            }
            if (pasteToolStripMenuItem1 != null)
            {
                this.pasteToolStripMenuItem1.Click -= new System.EventHandler(this.pasteToolStripMenuItem1_Click);
                pasteToolStripMenuItem1.Dispose();
                pasteToolStripMenuItem1 = null;
            }

            if (this.contextGridMenu != null)
            {
                this.contextGridMenu.Dispose();
                this.contextGridMenu = null;
            }            
        }

        public DataGridView DataGridView
        {
            get { return this.Grid; } 
        }

        
        #region Private
        private void InitializeConextMenuStrip()
        {
            if (contextGridMenu != null)
            {
                contextGridMenu.Dispose();
                contextGridMenu = null;
            }
            if (cutToolStripMenuItem != null)
            {
                cutToolStripMenuItem.Click -= new System.EventHandler(cutToolStripMenuItem_Click);
                cutToolStripMenuItem.Dispose();
                cutToolStripMenuItem = null;
            }
            if( copyToolStripMenuItem != null)
            {
                this.copyToolStripMenuItem.Click -= new System.EventHandler(this.copyToolStripMenuItem_Click);
                copyToolStripMenuItem.Dispose();
                copyToolStripMenuItem = null;
            }
            if (pasteToolStripMenuItem1 != null)
            {
                this.pasteToolStripMenuItem1.Click -= new System.EventHandler(this.pasteToolStripMenuItem1_Click);
                pasteToolStripMenuItem1.Dispose();
                pasteToolStripMenuItem1 = null;
            }

            if(this.contextGridMenu != null)
            {
                this.contextGridMenu.Dispose();
                this.contextGridMenu = null;
            }

            this.contextGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();

            this.contextGridMenu.SuspendLayout();

            // 
            // contextGridMenu
            // 
            this.contextGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem1});
            this.contextGridMenu.Name = "contextGridMenu";
            this.contextGridMenu.Size = new System.Drawing.Size(103, 70);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.cutToolStripMenuItem.Text = "&Cut";
            cutToolStripMenuItem.Click += new System.EventHandler(cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "C&opy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem1.Text = "&Paste";
            this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.pasteToolStripMenuItem1_Click);

            this.contextGridMenu.ResumeLayout(false);
        }
        #endregion

        public DataGridViewExt()
        {
            InitializeComponent();
            this.Grid.RowHeadersVisible = false;
            this.Grid.DataError += Grid_DataError;
            this.Grid.CellMouseClick += Grid_CellMouseClick;



        }        

        public void DoubleBufferedGrid(bool value)
        {
            Type dgvType = this.DataGridView.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.DataGridView, value, null);
        }

        void Grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

           
        }

        protected void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine("Grid error occured. Details to follow...");
            Console.WriteLine("Column: " + e.ColumnIndex);
            Console.WriteLine("Row: " + e.RowIndex);
            Console.WriteLine("Context: " + e.Context.ToString());
            Console.WriteLine("Exception: " + e.Exception.Message);
        }

        //protected void MoveRow(DataGridViewRow row, RowDirection direction) 
        //{
        //    if (row.Index > 0) 
        //    {
        //        row.Index += (int)direction;
        //    }
        //}

        //public enum RowDirection
        //{
        //    Up = -1,
        //    Down = 1
        //}

        /// <summary>
        /// Selects a single row using the Primary key column (usually the Id column) and the Id value
        /// </summary>
        /// <param name="ColumnName"></param>
        /// <param name="keyValue"></param>
        public void SetSelectedGridRow(long keyValue) 
        {
            for (int i = 0; i < this.Grid.Rows.Count; i++)
			{
			 if ((long)this.Grid.Rows[i].Cells["Id"].Value == keyValue)
                {
                    this.Grid.CurrentCell = this.Grid.Rows[i].Cells[1];
                }
			}
            
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented, sorry");
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented, sorry");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void CopyToClipboard()
        {
            //Copy to clipboard
            DataObject dataObj = Grid.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Grid.SelectedCells.Count > 0)
                Grid.ContextMenuStrip = contextGridMenu;
        }
    }
       
}
