using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBookShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddressChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Order",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValueSql: "проспект Людвіга Свободи, 33, Харків, Харківська область, 61000")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Order",
                type: "longtext",
                nullable: false,
                defaultValueSql: "проспект Людвіга Свободи, 33, Харків, Харківська область, 61000",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
