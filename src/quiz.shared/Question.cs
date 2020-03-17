using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public class Question {

        [Key]
        public Guid QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Solution { get; set; }

        public bool HasEmptyProperties()
        {
            var type = GetType();
            var properties = type.GetProperties();
            var hasEmptyProperty = properties.Select(x => x.GetValue(this))
                                        .Any(y => String.IsNullOrWhiteSpace(y.ToString()));
            return hasEmptyProperty;
        }
    }