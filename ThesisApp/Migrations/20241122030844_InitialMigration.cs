using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PreTheses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PreThesisName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreThesisLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreTheses", x => x.id);
                    table.ForeignKey(
                        name: "FK_PreTheses_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Theses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ThesisName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThesisLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Theses_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MentorPairs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreThesisId = table.Column<int>(type: "int", nullable: false),
                    MentorLecturerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentorPairs", x => x.id);
                    table.ForeignKey(
                        name: "FK_MentorPairs_PreTheses_PreThesisId",
                        column: x => x.PreThesisId,
                        principalTable: "PreTheses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MentorPairs_Users_MentorLecturerId",
                        column: x => x.MentorLecturerId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThesisDefences",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThesisId = table.Column<int>(type: "int", nullable: false),
                    MentorLecturerId = table.Column<int>(type: "int", nullable: false),
                    ExaminerLecturerId = table.Column<int>(type: "int", nullable: false),
                    Schedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThesisDefences", x => x.id);
                    table.ForeignKey(
                        name: "FK_ThesisDefences_Theses_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "Theses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThesisDefences_Users_ExaminerLecturerId",
                        column: x => x.ExaminerLecturerId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThesisDefences_Users_MentorLecturerId",
                        column: x => x.MentorLecturerId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MentoringSessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MentorPairId = table.Column<int>(type: "int", nullable: false),
                    Schedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentoringSessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_MentoringSessions_MentorPairs_MentorPairId",
                        column: x => x.MentorPairId,
                        principalTable: "MentorPairs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MentoringSessions_MentorPairId",
                table: "MentoringSessions",
                column: "MentorPairId");

            migrationBuilder.CreateIndex(
                name: "IX_MentorPairs_MentorLecturerId",
                table: "MentorPairs",
                column: "MentorLecturerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MentorPairs_PreThesisId",
                table: "MentorPairs",
                column: "PreThesisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreTheses_StudentId",
                table: "PreTheses",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theses_StudentId",
                table: "Theses",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThesisDefences_ExaminerLecturerId",
                table: "ThesisDefences",
                column: "ExaminerLecturerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThesisDefences_MentorLecturerId",
                table: "ThesisDefences",
                column: "MentorLecturerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThesisDefences_ThesisId",
                table: "ThesisDefences",
                column: "ThesisId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MentoringSessions");

            migrationBuilder.DropTable(
                name: "ThesisDefences");

            migrationBuilder.DropTable(
                name: "MentorPairs");

            migrationBuilder.DropTable(
                name: "Theses");

            migrationBuilder.DropTable(
                name: "PreTheses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
