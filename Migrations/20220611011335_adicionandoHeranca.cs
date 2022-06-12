using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pomodoro.Migrations
{
    public partial class adicionandoHeranca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Metas_MetasId",
                table: "Tarefas");

            migrationBuilder.AlterColumn<long>(
                name: "MetasId",
                table: "Tarefas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fim",
                table: "Tarefas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inicio",
                table: "Tarefas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RotinaTarefaId",
                table: "Tarefas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TarefaRecursiva_Inicio",
                table: "Tarefas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "intervaloMaximo",
                table: "Tarefas",
                type: "time",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Discriminator",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Fim",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Inicio",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "RotinaTarefaId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "TarefaRecursiva_Inicio",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "intervaloMaximo",
                table: "Tarefas");

            migrationBuilder.AlterColumn<long>(
                name: "MetasId",
                table: "Tarefas",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Metas_MetasId",
                table: "Tarefas",
                column: "MetasId",
                principalTable: "Metas",
                principalColumn: "MetasId");
        }
    }
}
