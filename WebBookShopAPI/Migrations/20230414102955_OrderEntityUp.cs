using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBookShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class OrderEntityUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Order",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Order");
        }
    }
}
