using System;
using System.Collections.Generic;

namespace projectClient.Models
{
    public partial class TestCase
    {

        public int TestcaseId { get; set; }
        public string? Input { get; set; }
        public string? Output { get; set; }
        public double? Mark { get; set; }
        public int? QuestionId { get; set; }
    }
}
