using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pomodoro.Migrations
{
    public partial class corigindoListas4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MetasId",
                table: "Usuarios",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_MetasId",
                table: "Usuarios",
                column: "MetasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Metas_MetasId",
                table: "Usuarios",
                column: "MetasId",
                principalTable: "Metas",
                principalColumn: "MetasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Metas_MetasId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_MetasId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "MetasId",
                table: "Usuarios");
        }
    }
}
