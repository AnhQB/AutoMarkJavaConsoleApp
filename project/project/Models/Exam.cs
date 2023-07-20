using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class Exam
    {
        public Exam()
        {
            ExamResults = new HashSet<ExamResult>();
            Questions = new HashSet<Question>();
        }

        public int ExamId { get; set; }
        public int PaperNo { get; set; }
        public string? ExamName { get; set; }

        public virtual ICollection<ExamResult> ExamResults { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
