﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pomodoro.data;

#nullable disable

namespace pomodoro.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MetaUsuario", b =>
                {
                    b.Property<long>("MetasId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuariosUsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("MetasId", "UsuariosUsuarioId");

                    b.HasIndex("UsuariosUsuarioId");

                    b.ToTable("MetaUsuario");
                });

            modelBuilder.Entity("pomodoro.data.Meta", b =>
                {
                    b.Property<long>("MetasId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MetasId"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetasId");

                    b.ToTable("Metas");
                });

            modelBuilder.Entity("pomodoro.data.Pomodoro", b =>
                {
                    b.Property<long>("PomodoroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PomodoroId"), 1L, 1);

                    b.Property<long>("Intervalo")
                        .HasColumnType("bigint");

                    b.Property<long?>("TarefaId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime2");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConcluida")
                        .HasColumnType("bit");

                    b.Property<long>("MetaId")
                        .HasColumnType("bigint");

                    b.HasKey("TarefaId");

                    b.HasIndex("MetaId");

                    b.ToTable("Tarefas");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Tarefa");
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

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("pomodoro.data.Rotina", b =>
                {
                    b.HasBaseType("pomodoro.data.Tarefa");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("intervaloMaximoDias")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Rotina");
                });

            modelBuilder.Entity("pomodoro.data.TarefaRecursiva", b =>
                {
                    b.HasBaseType("pomodoro.data.Tarefa");

                    b.Property<DateTime>("Fim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Inicio")
                        .HasColumnType("datetime2")
                        .HasColumnName("TarefaRecursiva_Inicio");

                    b.HasDiscriminator().HasValue("TarefaRecursiva");
                });

            modelBuilder.Entity("MetaUsuario", b =>
                {
                    b.HasOne("pomodoro.data.Meta", null)
                        .WithMany()
                        .HasForeignKey("MetasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pomodoro.data.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuariosUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("pomodoro.data.Pomodoro", b =>
                {
                    b.HasOne("pomodoro.data.Tarefa", null)
                        .WithMany("Pomodoros")
                        .HasForeignKey("TarefaId");
                });

            modelBuilder.Entity("pomodoro.data.Tarefa", b =>
                {
                    b.HasOne("pomodoro.data.Meta", "Meta")
                        .WithMany("Tarefas")
                        .HasForeignKey("MetaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meta");
                });

            modelBuilder.Entity("pomodoro.data.Meta", b =>
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
