﻿// <auto-generated />
using System;
using Api.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Persistencia.Migraciones
{
    [DbContext(typeof(GestionTareasDbContext))]
    partial class GestionTareasDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ProyectoUsuario", b =>
                {
                    b.Property<Guid>("ProyectoAsignadosId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UsuariosId")
                        .HasColumnType("char(36)");

                    b.HasKey("ProyectoAsignadosId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("ProyectoUsuario");
                });

            modelBuilder.Entity("biblioteca.Dominio.Comentario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("CreacionUsuario")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Fecha")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Ticket")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("TicketId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Usuario")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comentario");
                });

            modelBuilder.Entity("biblioteca.Dominio.Proyecto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreacionUsuario")
                        .HasColumnType("char(36)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Proyecto");
                });

            modelBuilder.Entity("biblioteca.Dominio.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime?>("FechaFin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Proyecto")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ProyectoId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Usuario")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProyectoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("biblioteca.Dominio.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreacionUsuario")
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("varchar(24)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("ProyectoUsuario", b =>
                {
                    b.HasOne("biblioteca.Dominio.Proyecto", null)
                        .WithMany()
                        .HasForeignKey("ProyectoAsignadosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("biblioteca.Dominio.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("biblioteca.Dominio.Comentario", b =>
                {
                    b.HasOne("biblioteca.Dominio.Ticket", null)
                        .WithMany("Actividad")
                        .HasForeignKey("TicketId");

                    b.HasOne("biblioteca.Dominio.Usuario", null)
                        .WithMany("ComentariosUsuario")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("biblioteca.Dominio.Ticket", b =>
                {
                    b.HasOne("biblioteca.Dominio.Proyecto", null)
                        .WithMany("Tickets")
                        .HasForeignKey("ProyectoId");

                    b.HasOne("biblioteca.Dominio.Usuario", null)
                        .WithMany("TicketsAsignados")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("biblioteca.Dominio.Proyecto", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("biblioteca.Dominio.Ticket", b =>
                {
                    b.Navigation("Actividad");
                });

            modelBuilder.Entity("biblioteca.Dominio.Usuario", b =>
                {
                    b.Navigation("ComentariosUsuario");

                    b.Navigation("TicketsAsignados");
                });
#pragma warning restore 612, 618
        }
    }
}
