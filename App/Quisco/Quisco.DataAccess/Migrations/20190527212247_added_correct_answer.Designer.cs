﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quisco.DataAccess;

namespace Quisco.DataAccess.Migrations
{
    [DbContext(typeof(QuiscoContext))]
    [Migration("20190527212247_added_correct_answer")]
    partial class added_correct_answer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Quisco.Model.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerNumber");

                    b.Property<string>("AnswerText");

                    b.Property<int?>("BelongingQuestionQuestionId");

                    b.Property<int?>("QuestionId");

                    b.HasKey("AnswerId");

                    b.HasIndex("BelongingQuestionQuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Quisco.Model.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BelongingQuizQuizId");

                    b.Property<int?>("CorrectAnswerAnswerId");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("QuestionId");

                    b.HasIndex("BelongingQuizQuizId");

                    b.HasIndex("CorrectAnswerAnswerId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Quisco.Model.Quiz", b =>
                {
                    b.Property<int>("QuizId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("QuizCategory")
                        .HasMaxLength(20);

                    b.Property<string>("QuizName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("QuizId");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("Quisco.Model.Answer", b =>
                {
                    b.HasOne("Quisco.Model.Question", "BelongingQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("BelongingQuestionQuestionId");

                    b.HasOne("Quisco.Model.Question")
                        .WithMany("AnswersCollection")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("Quisco.Model.Question", b =>
                {
                    b.HasOne("Quisco.Model.Quiz", "BelongingQuiz")
                        .WithMany("QuestionsCollection")
                        .HasForeignKey("BelongingQuizQuizId");

                    b.HasOne("Quisco.Model.Answer", "CorrectAnswer")
                        .WithMany()
                        .HasForeignKey("CorrectAnswerAnswerId");
                });
#pragma warning restore 612, 618
        }
    }
}
