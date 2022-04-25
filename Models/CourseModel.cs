using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentHQ.Models
{
    public class CourseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int SemesterID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string? Location { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        //public List<string>? MeetingDays { get; set; }
        public string? Subject { get; set; }
        public string? CourseNumber { get; set; }
        public Uri? MeetingURI { get; set; }
        public string? QuickNoteFile { get; set; }
        public string? MyTasksFile { get; set; }
       
    }
}
