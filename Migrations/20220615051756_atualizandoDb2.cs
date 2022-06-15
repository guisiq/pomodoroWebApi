using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pomodoro.Migrations
{
    public partial class atualizandoDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "intervaloMaximo",
                table: "Tarefas");

            migrationBuilder.AddColumn<long>(
                name: "intervaloMaximoDias",
                table: "Tarefas",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "intervaloMaximoDias",
                table: "Tarefas");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "intervaloMaximo",
                table: "Tarefas",
                type: "time",
                nullable: true);
        }
    }
}
