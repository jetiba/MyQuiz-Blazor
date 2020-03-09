using System;
using CsvHelper.Configuration;

public sealed class QuestionMap : ClassMap<Question>
{
public QuestionMap()
    {
        Map(m => m.QuestionID).ConvertUsing(row => Guid.NewGuid());
        Map(m => m.QuestionText);
        Map(m => m.Answer1);
        Map(m => m.Answer2);
        Map(m => m.Answer3);
        Map(m => m.Solution);
    }
}