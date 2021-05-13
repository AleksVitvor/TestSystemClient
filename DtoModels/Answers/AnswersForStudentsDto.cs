using System.ComponentModel.DataAnnotations;

namespace DtoModels.Answers
{
    public class AnswersForStudentsDto
    {
        [ScaffoldColumn(false)]
        public int AnswerId { get; set; }

        [ScaffoldColumn(false)]
        public int QuestionId { get; set; }

        public string Answer { get; set; }

        public bool IsSelected { get; set; }
    }
}
