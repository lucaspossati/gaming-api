using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Companies_CompanyId",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "People",
                newName: "company_id");

            migrationBuilder.RenameIndex(
                name: "IX_People_CompanyId",
                table: "People",
                newName: "IX_People_company_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "company_id",
                table: "People",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Companies_company_id",
                table: "People",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Companies_company_id",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "People",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_People_company_id",
                table: "People",
                newName: "IX_People_CompanyId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "People",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Companies_CompanyId",
                table: "People",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "id");
        }
    }
}
