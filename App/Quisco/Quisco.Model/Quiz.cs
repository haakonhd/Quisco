using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

		public ICollection<Question> Questions { get; } = new List<Question>();

		public Quiz(){}

		public Quiz(string quizName)
		{
			QuizName = quizName;
		}

		public override string ToString()
		{
			return this.QuizName;
		}
	}
}
