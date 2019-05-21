using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

		public IList<string> Answers { get; set;  } = new List<string>();

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