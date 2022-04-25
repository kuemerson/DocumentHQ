using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentHQ.Models
{
    public class BookModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int courseId { get; set; }
        [MaxLength(50)]
        public string Label { get; set; }
        [MaxLength(255)]
        public string BookPath { get; set; }

    }
}
