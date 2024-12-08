using SM.ClubManager.Library.Base.BaseClasses;
using SM.ClubManager.Library.Base.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SM.ClubManager.Library.Base.Controls
{
    public partial class ComboBoxExt : ComboBox
    {
        public List<ComboBoxItem> DataSource { get; set; }
        
        public ComboBoxExt()
        {
            InitializeComponent();
        }

        public void SetDataSource(List<ComboBoxItem> comboBoxItems) 
        {
            //clear the datasource of the underlying combobox
            base.DataSource = null;

            if (comboBoxItems == null)
            {              
                comboBoxItems = new List<ComboBoxItem>();
            }
               
            this.DropDownStyle = ComboBoxStyle.DropDownList;

            this.DisplayMember = "Value";
            this.ValueMember = "Key";
            comboBoxItems.Insert(0, new ComboBoxItem() { Key = "0", Value = Constants.DropDownNullValueDisplay });
            base.DataSource = comboBoxItems;
        }

        /// <summary>
        /// Selects the item based on the key of the item
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="selectedKey">Value of the selected key i.e. the Id of the object</param>
        public void SetSelectedItemByKey(object selectedKey) 
        {
            string keyValue = selectedKey.ToString();
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (((ComboBoxItem)this.Items[i]).Key == keyValue)
                    this.SelectedIndex = i;
            }
        }

        public ComboBoxExt(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            
        }                     
    }
}
