using Microsoft.EntityFrameworkCore.Migrations;

namespace Quisco.DataAccess.Migrations
{
    public partial class UpdatedStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Question_Question1QuestionId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Question_Question2QuestionId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Question_Question3QuestionId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Question_Question4QuestionId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_Question1QuestionId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_Question2QuestionId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_Question3QuestionId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_Question4QuestionId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Question1QuestionId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Question2QuestionId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Question3QuestionId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Question4QuestionId",
                table: "Quizzes");

            migrationBuilder.AddColumn<int>(
                name: "AssociatedQuizQuizId",
                table: "Question",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_AssociatedQuizQuizId",
                table: "Question",
                column: "AssociatedQuizQuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quizzes_AssociatedQuizQuizId",
                table: "Question",
                column: "AssociatedQuizQuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quizzes_AssociatedQuizQuizId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_AssociatedQuizQuizId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "AssociatedQuizQuizId",
                table: "Question");

            migrationBuilder.AddColumn<int>(
                name: "Question1QuestionId",
                table: "Quizzes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Question2QuestionId",
                table: "Quizzes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Question3QuestionId",
                table: "Quizzes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Question4QuestionId",
                table: "Quizzes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_Question1QuestionId",
                table: "Quizzes",
                column: "Question1QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_Question2QuestionId",
                table: "Quizzes",
                column: "Question2QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_Question3QuestionId",
                table: "Quizzes",
                column: "Question3QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_Question4QuestionId",
                table: "Quizzes",
                column: "Question4QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Question_Question1QuestionId",
                table: "Quizzes",
                column: "Question1QuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Question_Question2QuestionId",
                table: "Quizzes",
                column: "Question2QuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Question_Question3QuestionId",
                table: "Quizzes",
                column: "Question3QuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Question_Question4QuestionId",
                table: "Quizzes",
                column: "Question4QuestionId",
                principalTable: "Question",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
