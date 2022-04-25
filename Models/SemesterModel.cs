using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentHQ.Models
{
    public class SemesterModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Year { get; set; }
        public string Season { get; set; }
        //[Ignore]
        public string Name { get; set; }

        //public SchoolYearModel()
        //{
        //    Year = DateTime.Now.Year.ToString();            
        //}
    }
}
