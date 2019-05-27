using Microsoft.EntityFrameworkCore.Migrations;

namespace Quisco.DataAccess.Migrations
{
    public partial class added_correct_answer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerAnswerId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnswerNumber",
                table: "Answers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Question_CorrectAnswerAnswerId",
                table: "Question",
                column: "CorrectAnswerAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Answers_CorrectAnswerAnswerId",
                table: "Question",
                column: "CorrectAnswerAnswerId",
                principalTable: "Answers",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Answers_CorrectAnswerAnswerId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_CorrectAnswerAnswerId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerAnswerId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "AnswerNumber",
                table: "Answers");
        }
    }
}
