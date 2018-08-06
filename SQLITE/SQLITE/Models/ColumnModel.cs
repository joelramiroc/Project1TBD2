using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLITE.Models
{
    public class ColumnModel
    {
        public string ColumnName { get; set; }

        public int? Cid { get; set; }

        public DataType? DataType { get; set; }

        public string DateType { get; set; }

        public bool? IsNull { get; set; }

        public string DefaultValue { get; set; }

        public bool? IsPrimaryKey { get; set; }
    }
}
