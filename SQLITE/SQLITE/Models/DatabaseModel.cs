using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLITE.Models
{
    public class DatabaseModel
    {
        public List<TableModel> Tables { get; set; }

        public List<ViewModel> Views { get; set; }

        public List<TriggerModel> Triggers { get; set; }

        public List<CheckModel> Checks { get; set; }

        public string DataBaseName { get; set; }
    }
}
