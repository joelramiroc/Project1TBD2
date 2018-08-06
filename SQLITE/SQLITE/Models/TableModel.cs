using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLITE.Models
{
    public class TableModel
    {
        public TableModel()
        {
            if (this.Columns == null)
            {
                this.Columns = new List<ColumnModel>();
            }
        }
        public string TableName { get; set; }

        public List<ColumnModel> Columns { get; set; }
    }
}
