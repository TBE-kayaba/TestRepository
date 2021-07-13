using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TestSolition.Entity
{
    [Table("sample01")]
    class sample01
    {
        public String name { get; set; }
        public DateTime? updated_time { get; set; }
    }
}
