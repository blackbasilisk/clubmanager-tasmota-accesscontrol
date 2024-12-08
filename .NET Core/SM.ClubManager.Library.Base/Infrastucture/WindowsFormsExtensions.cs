using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SM.ClubManager.Library.Base.Infrastructure.WindowsFormsExtensions
{
    public static class Ext
    {
        #region DatagridView Extensions

        public static void MoveUp(this DataGridView dgv)
        {
            //if (dgv.RowCount <= 0)
            //    return;

            //if (dgv.SelectedRows.Count <= 0)
            //    return;

            //var index = dgv.SelectedCells[0].OwningRow.Index;

            //if (index == 0)
            //    return;

            //var rows = dgv.Rows;
            //var prevRow = rows[index - 1];
            //rows.Remove(prevRow);
            //prevRow.Frozen = false;
            //rows.Insert(index, prevRow);
            //dgv.ClearSelection();
            //dgv.Rows[index - 1].Selected = true;            
        }

        public static void MoveDown(this DataGridView dgv)
        {
            if (dgv.RowCount <= 0)
                return;

            if (dgv.SelectedRows.Count <= 0)
                return;

            var rowCount = dgv.Rows.Count;
            var index = dgv.SelectedCells[0].OwningRow.Index;

            if (index == (rowCount - 2)) // include the header row
                return;

            var rows = dgv.Rows;
            var nextRow = rows[index + 1];
            rows.Remove(nextRow);
            nextRow.Frozen = false;
            rows.Insert(index, nextRow);
            dgv.ClearSelection();
            dgv.Rows[index + 1].Selected = true;
        }
        #endregion

        #region Control Extensions
        public static void DoubleBuffering(this Control control, bool enable)
        {
            var method = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(control, new object[] { ControlStyles.OptimizedDoubleBuffer, enable });
        }

        public static void InvokeIfRequired<T>(this T c, Action<T> action) where T : Control
        {
            try
            {
                if (c.InvokeRequired)
                {
                    if(!c.IsDisposed)
                        c.Invoke(new Action(() => action(c)));
                }
                else
                {
                    action(c);
                }
            }
            catch(ObjectDisposedException objEx)
            {
                return;
            }
            catch (ThreadInterruptedException e)
            {
                if (c.InvokeRequired)
                {
                    c.Invoke(new Action(() => action(c)));
                }
                else
                {
                    action(c);
                }
            }                            
        }

        //public static void EnableSingleControlOfType<T>(this Control control, string controlName) where T : Control
        //{
        //    //find and enable the control
        //    foreach (var cntrl in control.Controls.OfType<T>())
        //    {
        //        var c = cntrl as T;
        //        if (c != null && c.Name == controlName)
        //        {
        //            c.InvokeIfRequired(co => { co.Focus(); co.Enabled = true; });
        //        }
        //        else
        //            c.InvokeIfRequired(co => { c.Enabled = false; });
        //    }
        //}

        //public static void Enable(this Control con, bool enable, Dictionary<string, Type> excludeControls = null)
        //{
        //    if (con != null)
        //    {

        //        foreach (Control c in con.Controls)
        //        {
        //            //c.Enable(enable);
        //            //if (excludeControls == null || !excludeControls.Contains(c.Name))
        //            c.Enable(enable, excludeControls);
        //        }

        //        try
        //        {
        //            if (!(con.GetType() == typeof(Form)))
        //            {

        //                bool isFound = false;
        //                //then loop through the exclusion list and check if the current control is in the exlusion list
        //                //if it is, just continue with next control
        //                if (excludeControls != null)
        //                {
        //                    foreach (KeyValuePair<string, Type> kvp in excludeControls)
        //                    {
        //                        if (con.GetType() == kvp.Value)
        //                        {
        //                            Button btnControl = con as Button;
        //                            if (btnControl != null)
        //                            {
        //                                if (btnControl.Name == kvp.Key)
        //                                {
        //                                    isFound = true;
        //                                    break;
        //                                    //((Button)c).Enable(!enable);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                if (!isFound)
        //                    con.Invoke((MethodInvoker)(() => con.Enabled = enable));
        //                else
        //                {
        //                    string breakHere = "Break Here";
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string e = ex.Message;
        //        }
        //    }
        //}

        public static void SetImage(this PictureBox picBox, string filePath, bool isFilePathRelative = false)
        {
            try
            {
                if (picBox == null)
                    return;
                string fullPath = filePath;
                if(isFilePathRelative)
                {
                    fullPath = System.IO.Path.Combine(Environment.CurrentDirectory, filePath.Trim('\\'));
                }
                

                //Check if file exists
                if(!System.IO.File.Exists(filePath))
                {
                    throw new System.IO.FileNotFoundException();
                }

                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    fs.CopyTo(ms);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    picBox.InvokeIfRequired(i => i.Image = Image.FromStream(ms));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

    }
}
