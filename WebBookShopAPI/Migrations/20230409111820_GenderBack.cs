using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBookShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class GenderBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenderCode",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserGenderCode",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    GenderCode = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.GenderCode);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserGenderCode",
                table: "AspNetUsers",
                column: "UserGenderCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Gender_UserGenderCode",
                table: "AspNetUsers",
                column: "UserGenderCode",
                principalTable: "Gender",
                principalColumn: "GenderCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Gender_UserGenderCode",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserGenderCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserGenderCode",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "GenderCode",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
