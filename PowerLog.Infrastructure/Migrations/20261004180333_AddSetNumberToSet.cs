using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerLog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSetNumberToSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RPE",
                table: "Sets",
                newName: "Rpe");

            migrationBuilder.RenameColumn(
                name: "RPE",
                table: "PersonalRecords",
                newName: "Rpe");

            migrationBuilder.AddColumn<int>(
                name: "SetNumber",
                table: "Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetNumber",
                table: "Sets");

            migrationBuilder.RenameColumn(
                name: "Rpe",
                table: "Sets",
                newName: "RPE");

            migrationBuilder.RenameColumn(
                name: "Rpe",
                table: "PersonalRecords",
                newName: "RPE");
        }
    }
}
