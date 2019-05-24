using System;

namespace Quisco.Model
{
	public class Answer
	{
		public int AnswerId { get; set; }
		public string AnswerText { get; set; }
		public Boolean IsCorrect { get; set; }
		public Question BelongingQuestion { get; set; }

		public Answer(string answerText, Question belongingQuestion)
		{
			this.AnswerText = answerText;
			this.BelongingQuestion = belongingQuestion;
		}

		public Answer(int answerId, string answerText, Question belongingQuestion)
		{
			this.AnswerId = answerId;
			this.AnswerText = answerText;
			this.BelongingQuestion = belongingQuestion;
		}

		public Answer() { }
	}
}