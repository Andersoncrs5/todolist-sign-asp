using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListApi.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ulong>(
                name: "FkUserId",
                table: "tasks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ulong>(
                name: "FkUserId",
                table: "tasks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "bigint");
        }
    }
}
