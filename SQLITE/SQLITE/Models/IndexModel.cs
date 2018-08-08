using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLITE.Models
{
    public class IndexModel
    {
        public int Id { get; set; }

        public string IndexNamenName { get; set; }

        public string Sql { get; set; }
    }
}
