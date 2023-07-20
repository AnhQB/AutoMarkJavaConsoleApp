using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class ExamResult
    {
        public ExamResult()
        {
            GradeDetails = new HashSet<GradeDetail>();
        }

        public int ExamresultId { get; set; }
        public double? Mark { get; set; }
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }
        public int? PaperNo { get; set; }

        public virtual Exam? Exam { get; set; }
        public virtual Student? Student { get; set; }
        public virtual ICollection<GradeDetail> GradeDetails { get; set; }
    }
}
