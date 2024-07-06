using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financial.Infra.Migrations
{
    /// <inheritdoc />
    public partial class updatesubcategorytablewithnameandremoveicon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconName",
                table: "SubCategory");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SubCategory",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SubCategory");

            migrationBuilder.AddColumn<string>(
                name: "IconName",
                table: "SubCategory",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
