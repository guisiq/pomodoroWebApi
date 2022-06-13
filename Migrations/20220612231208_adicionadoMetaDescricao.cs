using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pomodoro.Migrations
{
    public partial class adicionadoMetaDescricao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descricao",
                table: "Metas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descricao",
                table: "Metas");
        }
    }
}
