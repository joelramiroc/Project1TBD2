using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITE.Views
{
    public partial class RegisterOfTableView : Form
    {
        public string TableName { get; set; }

        public RegisterOfTableView(string tableName)
        {
            InitializeComponent();
        }
    }
}
