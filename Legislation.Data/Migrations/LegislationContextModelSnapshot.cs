﻿// <auto-generated />
using System;
using Legislation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Legislation.Data.Migrations
{
    [DbContext(typeof(LegislationContext))]
    partial class LegislationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Legislation.Data.LawEntity.Law", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Laws");
                });

            modelBuilder.Entity("Legislation.Data.ReferendumEntity.Referendum", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

                    b.Property<int>("LawID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Open")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.HasKey("ID");

                    b.HasIndex("LawID")
                        .HasDatabaseName("IX_ReferendumEntity_LawID");

                    b.ToTable("Referendums");
                });

            modelBuilder.Entity("Legislation.Data.ReferendumEntity.Referendum", b =>
                {
                    b.HasOne("Legislation.Data.LawEntity.Law", "Law")
                        .WithMany("Referendums")
                        .HasForeignKey("LawID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Law");
                });

            modelBuilder.Entity("Legislation.Data.LawEntity.Law", b =>
                {
                    b.Navigation("Referendums");
                });
#pragma warning restore 612, 618
        }
    }
}
