using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quisco.DataAccess.Migrations
{
    public partial class structurechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quizzes_AssociatedQuizQuizId",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "AssociatedQuizQuizId",
                table: "Question",
                newName: "BelongingQuizQuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_AssociatedQuizQuizId",
                table: "Question",
                newName: "IX_Question_BelongingQuizQuizId");

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerText = table.Column<string>(nullable: true),
                    BelongingQuestionQuestionId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Question_BelongingQuestionQuestionId",
                        column: x => x.BelongingQuestionQuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_BelongingQuestionQuestionId",
                table: "Answers",
                column: "BelongingQuestionQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quizzes_BelongingQuizQuizId",
                table: "Question",
                column: "BelongingQuizQuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quizzes_BelongingQuizQuizId",
                table: "Question");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.RenameColumn(
                name: "BelongingQuizQuizId",
                table: "Question",
                newName: "AssociatedQuizQuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_BelongingQuizQuizId",
                table: "Question",
                newName: "IX_Question_AssociatedQuizQuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quizzes_AssociatedQuizQuizId",
                table: "Question",
                column: "AssociatedQuizQuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
