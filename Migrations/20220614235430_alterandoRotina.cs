using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pomodoro.Migrations
{
    public partial class alterandoRotina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "intervaloMaximo",
                table: "Tarefas");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "intervaloMaximoDias",
                table: "Tarefas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data",
                table: "Pomodoros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "intervaloMaximoDias",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "data",
                table: "Pomodoros");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "intervaloMaximo",
                table: "Tarefas",
                type: "time",
                nullable: true);
        }
    }
}
