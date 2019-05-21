namespace Quisco.Model
{
	public class QuizParams
	{
		public Quiz Quiz { get; set; }
		public int QuestionToHandle { get; set; }

		public QuizParams(Quiz quiz, int questionToHandle)
		{
			this.Quiz = quiz;
			this.QuestionToHandle = questionToHandle;
		}
	}
}