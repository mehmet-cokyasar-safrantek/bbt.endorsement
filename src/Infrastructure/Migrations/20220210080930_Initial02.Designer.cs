﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220210080930_Initial02")]
    partial class Initial02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Approval", b =>
                {
                    b.Property<string>("ApprovalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApprovalTitle")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<string>("InstanceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApprovalId");

                    b.ToTable("Approvals");
                });

            modelBuilder.Entity("Domain.Entities.Callback", b =>
                {
                    b.Property<string>("CallbackId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstanceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferenceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CallbackId");

                    b.HasIndex("ReferenceId");

                    b.ToTable("Callbacks");
                });

            modelBuilder.Entity("Domain.Entities.Config", b =>
                {
                    b.Property<string>("ApprovalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstanceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxRetryCount")
                        .HasColumnType("int");

                    b.Property<string>("RetryFrequence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeoutMinutes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApprovalId");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("Domain.Entities.Reference", b =>
                {
                    b.Property<string>("ApprovalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstanceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApprovalId");

                    b.ToTable("References");
                });

            modelBuilder.Entity("Domain.Entities.Callback", b =>
                {
                    b.HasOne("Domain.Entities.Reference", "Reference")
                        .WithMany("Callbacks")
                        .HasForeignKey("ReferenceId");

                    b.Navigation("Reference");
                });

            modelBuilder.Entity("Domain.Entities.Config", b =>
                {
                    b.HasOne("Domain.Entities.Approval", "Approval")
                        .WithOne("Config")
                        .HasForeignKey("Domain.Entities.Config", "ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");
                });

            modelBuilder.Entity("Domain.Entities.Reference", b =>
                {
                    b.HasOne("Domain.Entities.Approval", "Approval")
                        .WithOne("Reference")
                        .HasForeignKey("Domain.Entities.Reference", "ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");
                });

            modelBuilder.Entity("Domain.Entities.Approval", b =>
                {
                    b.Navigation("Config");

                    b.Navigation("Reference");
                });

            modelBuilder.Entity("Domain.Entities.Reference", b =>
                {
                    b.Navigation("Callbacks");
                });
#pragma warning restore 612, 618
        }
    }
}