using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    registrationdate = table.Column<DateTime>(name: "registration_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ordination = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    recipientemail = table.Column<string>(name: "recipient_email", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    recipientnumber = table.Column<string>(name: "recipient_number", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    originemail = table.Column<string>(name: "origin_email", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    clientname = table.Column<string>(name: "client_name", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    username = table.Column<string>(name: "user_name", type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    salary = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    salarysave = table.Column<decimal>(name: "salary_save", type: "decimal(18,4)", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tokenexpirationdate = table.Column<DateTime>(name: "token_expiration_date", type: "datetime2", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phonenumber = table.Column<string>(name: "phone_number", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.id);
                    table.ForeignKey(
                        name: "FK_People_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    monthid = table.Column<Guid>(name: "month_id", type: "uniqueidentifier", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    salary = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.id);
                    table.ForeignKey(
                        name: "FK_Bills_Months_month_id",
                        column: x => x.monthid,
                        principalTable: "Months",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Users_user_id",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpentInMonth",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    monthid = table.Column<Guid>(name: "month_id", type: "uniqueidentifier", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "uniqueidentifier", nullable: false),
                    spent = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    spentpercentageabove = table.Column<decimal>(name: "spent_percentage_above", type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpentInMonth", x => x.id);
                    table.ForeignKey(
                        name: "FK_SpentInMonth_Months_month_id",
                        column: x => x.monthid,
                        principalTable: "Months",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpentInMonth_Users_user_id",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_month_id",
                table: "Bills",
                column: "month_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_user_id",
                table: "Bills",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_People_CompanyId",
                table: "People",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SpentInMonth_month_id",
                table: "SpentInMonth",
                column: "month_id");

            migrationBuilder.CreateIndex(
                name: "IX_SpentInMonth_user_id",
                table: "SpentInMonth",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "SpentInMonth");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
