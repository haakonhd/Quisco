using System;

namespace Quisco.Model
{
	public class Answer
	{
		public int AnswerId { get; set; }
		public string AnswerText { get; set; }
		public int AnswerNumber { get; set; } 
		public Question BelongingQuestion { get; set; }

		public Answer(string answerText, Question belongingQuestion, int answerNumber)
		{
			this.AnswerText = answerText;
			this.BelongingQuestion = belongingQuestion;
			this.AnswerNumber = answerNumber;
		}

		public Answer(int answerId, string answerText, Question belongingQuestion, int answerNumber)
		{
			this.AnswerId = answerId;
			this.AnswerText = answerText;
			this.BelongingQuestion = belongingQuestion;
			this.AnswerNumber = answerNumber;
		}

		public Answer() { }
	}
}