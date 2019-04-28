using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quisco.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionText = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuizName = table.Column<string>(maxLength: 100, nullable: false),
                    QuizCategory = table.Column<string>(maxLength: 20, nullable: true),
                    Question1QuestionId = table.Column<int>(nullable: true),
                    Question2QuestionId = table.Column<int>(nullable: true),
                    Question3QuestionId = table.Column<int>(nullable: true),
                    Question4QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                    table.ForeignKey(
                        name: "FK_Quizzes_Question_Question1QuestionId",
                        column: x => x.Question1QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quizzes_Question_Question2QuestionId",
                        column: x => x.Question2QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quizzes_Question_Question3QuestionId",
                        column: x => x.Question3QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quizzes_Question_Question4QuestionId",
                        column: x => x.Question4QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
