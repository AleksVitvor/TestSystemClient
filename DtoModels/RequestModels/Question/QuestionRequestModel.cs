namespace DtoModels.RequestModels.Question
{
    public class QuestionRequestModel
    {
        public int TestId { get; set; }

        public int QuestionId { get; set; }

        public string Question { get; set; }

        public string Test { get; set; }
    }
}
