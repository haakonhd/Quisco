using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Quisco.Model;

namespace Quisco.DataAccess
{
	public class QuestionDataAccess
	{
		public static void AddQuestionToDb(Question question)
		{
			using (var db = new QuiscoContext())
			{
				db.Questions.Add(question);
				var count = db.SaveChanges();
				//TODO: better error handling
				Console.WriteLine(count == 1
					? "Question saved to database"
					: "An error occured. Question was not saved to database");

			}
		}

		public static List<Question> GetQuestionsFromDbWithoutBelongingQuiz()
		{
			List<Question> result = new List<Question>();

			SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
			connStringBuilder = QuiscoContext.getConnStringBuilder();
			SqlConnection conn = new SqlConnection(connStringBuilder.ToString());
			SqlCommand getQuestionsCommand = new SqlCommand("SELECT * FROM dbo.Question", conn);
			conn.Open();

			var reader = getQuestionsCommand.ExecuteReader();
			while (reader.Read())
			{
				var id = reader.GetInt32(0);
				var text = reader.GetString(1);
				result.Add(new Question(id, text));
			}

			return result;
		}

		public QuestionDataAccess()
		{
			
		}

	}
}