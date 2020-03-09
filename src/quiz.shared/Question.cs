using System;
using System.ComponentModel.DataAnnotations;

public class Question {

        [Key]
        public Guid QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Solution { get; set; }
    }