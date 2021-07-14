

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestSolition.Entity
{
    [Table("sample01")]
    class sample01
    {
        public String name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? updated_time { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public String body { get; set; }
    }
}
