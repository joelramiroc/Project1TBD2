using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLITE.Models
{
    public class TriggerModel
    {
        public string TriggerName { get; set; }

        public string Sql { get; set; }

        public int Id { get; set; }
    }
}
