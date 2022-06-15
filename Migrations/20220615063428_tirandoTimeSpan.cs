using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pomodoro.Migrations
{
    public partial class tirandoTimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervaloProgramado",
                table: "Pomodoros");

            migrationBuilder.DropColumn(
                name: "IntervaloReal",
                table: "Pomodoros");

            migrationBuilder.AddColumn<long>(
                name: "Intervalo",
                table: "Pomodoros",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intervalo",
                table: "Pomodoros");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "IntervaloProgramado",
                table: "Pomodoros",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "IntervaloReal",
                table: "Pomodoros",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
