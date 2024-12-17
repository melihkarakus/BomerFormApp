using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BomerFormApp.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_university : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "University",
                table: "Users");
        }
    }
}
