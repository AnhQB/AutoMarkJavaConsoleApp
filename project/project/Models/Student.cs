using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class Student
    {
        public Student()
        {
            ExamResults = new HashSet<ExamResult>();
        }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }

        public virtual ICollection<ExamResult> ExamResults { get; set; }
    }
}
