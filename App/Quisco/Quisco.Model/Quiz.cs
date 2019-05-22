using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Quisco.Model
{
	[Table("Quizzes")]
	public class Quiz
	{
		[Key]
		public int QuizId { get; set; }
		[Required]
		[StringLength(100)]
		public string QuizName { get; set; }
		[StringLength(20)]
		public string QuizCategory { get; set; }
		[NotMapped]
		public IList<Question> Questions { get; set; } = new List<Question>();
		[Column("Questions")]
		public ICollection<Question> QuestionsCollection { get; set; } = new List<Question>();

		public Quiz(){}

		public Quiz(string quizName)
		{
			QuizName = quizName;
		}

		public Quiz(int quizId, string quizName, string quizCategory, ICollection<Question> questionsCollection)
		{
			this.QuizId = quizId;
			this.QuizName = quizName;
			this.QuizCategory = quizCategory;
			this.QuestionsCollection = questionsCollection;
		}
		public Quiz(string quizName, string quizCategory, ICollection<Question> questionsCollection)
		{
			this.QuizName = quizName;
			this.QuizCategory = quizCategory;
			this.QuestionsCollection = questionsCollection;
		}
		public override string ToString()
		{
			return this.QuizName;
		}
	}
}
