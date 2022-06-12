﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pomodoro.data;

#nullable disable

namespace pomodoro.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20220611004935_adicionadoDataSets")]
    partial class adicionadoDataSets
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("pomodoro.data.Metas", b =>
                {
                    b.Property<long>("MetasId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MetasId"), 1L, 1);

                    b.HasKey("MetasId");

                    b.ToTable("Metas");
                });

            modelBuilder.Entity("pomodoro.data.Pomodoro", b =>
                {
                    b.Property<long>("PomodoroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PomodoroId"), 1L, 1);

                    b.Property<TimeSpan>("IntervaloProgramado")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("IntervaloReal")
                        .HasColumnType("time");

                    b.Property<long?>("TarefaId")
                        .HasColumnType("bigint");

                    b.Property<short>("tipoPomodoro")
                        .HasColumnType("smallint");

                    b.HasKey("PomodoroId");

                    b.HasIndex("TarefaId");

                    b.ToTable("Pomodoros");
                });

            modelBuilder.Entity("pomodoro.data.Tarefa", b =>
                {
                    b.Property<long>("TarefaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TarefaId"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConcluida")
                        .HasColumnType("bit");

                    b.Property<long?>("MetasId")
                        .HasColumnType("bigint");

                    b.HasKey("TarefaId");

                    b.HasIndex("MetasId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("pomodoro.data.Usuario", b =>
                {
                    b.Property<long>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UsuarioId"), 1L, 1);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("pomodoro.data.Pomodoro", b =>
                {
                    b.HasOne("pomodoro.data.Tarefa", null)
                        .WithMany("Pomodoros")
                        .HasForeignKey("TarefaId");
                });

            modelBuilder.Entity("pomodoro.data.Tarefa", b =>
                {
                    b.HasOne("pomodoro.data.Metas", null)
                        .WithMany("Tarefas")
                        .HasForeignKey("MetasId");
                });

            modelBuilder.Entity("pomodoro.data.Metas", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("pomodoro.data.Tarefa", b =>
                {
                    b.Navigation("Pomodoros");
                });
#pragma warning restore 612, 618
        }
    }
}
