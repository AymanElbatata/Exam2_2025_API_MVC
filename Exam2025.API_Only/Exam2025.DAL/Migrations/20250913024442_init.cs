using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam2025.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BDataSchema");

            migrationBuilder.EnsureSchema(
                name: "ASecurity");

            migrationBuilder.CreateTable(
                name: "AppErrorTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppErrorTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateOfJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "ASecurity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberofQuestions = table.Column<int>(type: "int", nullable: false),
                    SuccessRate = table.Column<int>(type: "int", nullable: true),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExamTBLs_Users_CreatedUserID",
                        column: x => x.CreatedUserID,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "ASecurity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "ASecurity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "ASecurity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "ASecurity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamTBLId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionTBLs_ExamTBLs_ExamTBLId",
                        column: x => x.ExamTBLId,
                        principalSchema: "BDataSchema",
                        principalTable: "ExamTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserExamTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamTBLId = table.Column<int>(type: "int", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    UserRate = table.Column<int>(type: "int", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExamTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserExamTBLs_ExamTBLs_ExamTBLId",
                        column: x => x.ExamTBLId,
                        principalSchema: "BDataSchema",
                        principalTable: "ExamTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserExamTBLs_Users_CreatedUserID",
                        column: x => x.CreatedUserID,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnswerTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTBLId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRight = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AnswerTBLs_QuestionTBLs_QuestionTBLId",
                        column: x => x.QuestionTBLId,
                        principalSchema: "BDataSchema",
                        principalTable: "QuestionTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserExamDetailTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserExamTBLId = table.Column<int>(type: "int", nullable: true),
                    QuestionTBLId = table.Column<int>(type: "int", nullable: true),
                    AnswerTBLId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExamDetailTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserExamDetailTBLs_AnswerTBLs_AnswerTBLId",
                        column: x => x.AnswerTBLId,
                        principalSchema: "BDataSchema",
                        principalTable: "AnswerTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserExamDetailTBLs_QuestionTBLs_QuestionTBLId",
                        column: x => x.QuestionTBLId,
                        principalSchema: "BDataSchema",
                        principalTable: "QuestionTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserExamDetailTBLs_UserExamTBLs_UserExamTBLId",
                        column: x => x.UserExamTBLId,
                        principalSchema: "BDataSchema",
                        principalTable: "UserExamTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerTBLs_QuestionTBLId",
                schema: "BDataSchema",
                table: "AnswerTBLs",
                column: "QuestionTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTBLs_CreatedUserID",
                schema: "BDataSchema",
                table: "ExamTBLs",
                column: "CreatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTBLs_ExamTBLId",
                schema: "BDataSchema",
                table: "QuestionTBLs",
                column: "ExamTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "ASecurity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "ASecurity",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "ASecurity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExamDetailTBLs_AnswerTBLId",
                schema: "BDataSchema",
                table: "UserExamDetailTBLs",
                column: "AnswerTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExamDetailTBLs_QuestionTBLId",
                schema: "BDataSchema",
                table: "UserExamDetailTBLs",
                column: "QuestionTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExamDetailTBLs_UserExamTBLId",
                schema: "BDataSchema",
                table: "UserExamDetailTBLs",
                column: "UserExamTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExamTBLs_CreatedUserID",
                schema: "BDataSchema",
                table: "UserExamTBLs",
                column: "CreatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserExamTBLs_ExamTBLId",
                schema: "BDataSchema",
                table: "UserExamTBLs",
                column: "ExamTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "ASecurity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "ASecurity",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "ASecurity",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "ASecurity",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppErrorTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserExamDetailTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "AnswerTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserExamTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "QuestionTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "ExamTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "ASecurity");
        }
    }
}
