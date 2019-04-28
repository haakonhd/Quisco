using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quisco.Model
{
	[Table("Question")]
	public class Question
	{
		[Key]
		public int QuestionId { get; set; }
		[Required]
		[StringLength(500)]
		public string QuestionText { get; set; }
		[Required]
		public string Answer1 { get; set; }
		[Required]
		public string Answer2 { get; set; }
		public string Answer3 { get; set; }
		public string Answer4 { get; set; }
		public Quiz AssociatedQuiz { get; set; }

		public Question(){}

		public Question(string questionText)
		{
			this.QuestionText = questionText;
		}

		public Question(int questionId, string questionText)
		{
			this.QuestionId = questionId;
			this.QuestionText = questionText;
		}

		public override string ToString()
		{
			return this.QuestionText;
		}
	}
}