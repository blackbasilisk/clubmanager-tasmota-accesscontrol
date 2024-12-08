using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SM.ClubManager.Library.Base.ControlHelpers
{
    public class ListViewColumn
    {
        private string _columHeader;
        public string ColumnHeader
        {
            get { return _columHeader; }            
        }

        private int _width;
        public int Width
        {
            get { return _width; }           
        }

        private HorizontalAlignment _horizontalAlignment;
        public HorizontalAlignment HorizontalAlignment
        {
            get { return _horizontalAlignment; }            
        }
        
        private ListViewColumn(string columnHeader, int width, HorizontalAlignment alignment)
        {
            _columHeader = columnHeader;
            _width = width;
            _horizontalAlignment = alignment;
        }

        public static ListViewColumn CreateColumn(string columnHeader, int width = -2, HorizontalAlignment alignment = HorizontalAlignment.Left)
        {
            return new ListViewColumn(columnHeader, width, alignment);
        }
    }
}
