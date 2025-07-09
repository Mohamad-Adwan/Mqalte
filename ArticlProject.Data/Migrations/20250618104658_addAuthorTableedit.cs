using Microsoft.EntityFrameworkCore.Migrations;

namespace ArticlProject.Data.Migrations
{
    public partial class addAuthorTableedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Catrgory",
                table: "AuthorPost");

            migrationBuilder.AddColumn<int>(
                name: "CatrgoryId",
                table: "AuthorPost",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatrgoryId",
                table: "AuthorPost");

            migrationBuilder.AddColumn<int>(
                name: "Catrgory",
                table: "AuthorPost",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
