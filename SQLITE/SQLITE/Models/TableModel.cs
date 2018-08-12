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
            if (this.Indexs == null)
            {
                this.Indexs = new List<IndexModel>();
            }
            if (this.Fireigns== null)
            {
                this.Fireigns = new List<ForeignKeyModel>();
            }
        }
        public string TableName { get; set; }

        public List<ColumnModel> Columns { get; set; }

        public List<ForeignKeyModel> Fireigns { get; set; }

        public List<IndexModel> Indexs { get; set; }
    }
}
