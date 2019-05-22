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
		[NotMapped]
		public int QuestionNumber { get; set; }
		public Quiz BelongingQuiz { get; set; }
		[NotMapped]
		public IList<Answer> Answers { get; set;  } = new List<Answer>();
		[Column("Answers")]
		public ICollection<Answer> AnswersCollection { get; set;  } = new List<Answer>();

		public Question(){}

		public Question(string questionText)
		{
			this.QuestionText = questionText;
		}

		public Question(int questionId, string questionText, ICollection<Answer> answersCollection) 
		{
			this.QuestionId = questionId;
			this.QuestionText = questionText;
			this.AnswersCollection = answersCollection;
		}

		public Question(string questionText, ICollection<Answer> answersCollection)
		{
			this.QuestionText = questionText;
			this.AnswersCollection = answersCollection;
		}

		public override string ToString()
		{
			return this.QuestionText;
		}
	}
}