﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Sybon.Archive;
using System;

namespace Sybon.Archive.Migrations
{
    [DbContext(typeof(ArchiveContext))]
    partial class ArchiveContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sybon.Archive.Repositories.CollectionsRepository.Collection", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("Sybon.Archive.Repositories.ProblemsRepository.Problem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CollectionId");

                    b.Property<string>("InternalProblemId")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("InternalProblemId");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("Sybon.Archive.Repositories.ProblemsRepository.Problem", b =>
                {
                    b.HasOne("Sybon.Archive.Repositories.CollectionsRepository.Collection", "Collection")
                        .WithMany("Problems")
                        .HasForeignKey("CollectionId");
                });
#pragma warning restore 612, 618
        }
    }
}
