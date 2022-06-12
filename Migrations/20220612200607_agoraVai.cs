using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pomodoro.Migrations
{
    public partial class agoraVai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "MetaUsuario",
                columns: table => new
                {
                    MetasId = table.Column<long>(type: "bigint", nullable: false),
                    UsuariosUsuarioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaUsuario", x => new { x.MetasId, x.UsuariosUsuarioId });
                    table.ForeignKey(
                        name: "FK_MetaUsuario_Metas_MetasId",
                        column: x => x.MetasId,
                        principalTable: "Metas",
                        principalColumn: "MetasId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetaUsuario_Usuarios_UsuariosUsuarioId",
                        column: x => x.UsuariosUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetaUsuario_UsuariosUsuarioId",
                table: "MetaUsuario",
                column: "UsuariosUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetaUsuario");

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
    }
}
