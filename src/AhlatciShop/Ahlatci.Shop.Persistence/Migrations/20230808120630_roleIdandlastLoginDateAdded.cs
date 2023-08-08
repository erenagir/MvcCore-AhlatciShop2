using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ahlatci.Shop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class roleIdandlastLoginDateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginDate",
                table: "ACCOUNTS",
                newName: "LAST_LOGİN_DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LAST_LOGİN_DATE",
                table: "ACCOUNTS",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<int>(
                name: "ROlE_ID",
                table: "ACCOUNTS",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ROlE_ID",
                table: "ACCOUNTS");

            migrationBuilder.RenameColumn(
                name: "LAST_LOGİN_DATE",
                table: "ACCOUNTS",
                newName: "LastLoginDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginDate",
                table: "ACCOUNTS",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 5);
        }
    }
}
