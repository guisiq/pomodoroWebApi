using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pomodoro.Migrations
{
    public partial class configurandoLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Metas_MetasId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Tarefas_RotinaTarefaId",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_RotinaTarefaId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "RotinaTarefaId",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "MetasId",
                table: "Tarefas",
                newName: "MetaId");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_MetasId",
                table: "Tarefas",
                newName: "IX_Tarefas_MetaId");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Metas_MetaId",
                table: "Tarefas",
                column: "MetaId",
                principalTable: "Metas",
                principalColumn: "MetasId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Metas_MetaId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "MetaId",
                table: "Tarefas",
                newName: "MetasId");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_MetaId",
                table: "Tarefas",
                newName: "IX_Tarefas_MetasId");

            migrationBuilder.AddColumn<long>(
                name: "RotinaTarefaId",
                table: "Tarefas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_RotinaTarefaId",
                table: "Tarefas",
                column: "RotinaTarefaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Metas_MetasId",
                table: "Tarefas",
                column: "MetasId",
                principalTable: "Metas",
                principalColumn: "MetasId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Tarefas_RotinaTarefaId",
                table: "Tarefas",
                column: "RotinaTarefaId",
                principalTable: "Tarefas",
                principalColumn: "TarefaId");
        }
    }
}
