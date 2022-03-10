﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Callback", b =>
                {
                    b.Property<string>("ReferenceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CallbackId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferenceApprovalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReferenceId");

                    b.HasIndex("ReferenceApprovalId");

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

            modelBuilder.Entity("Domain.Entities.Document", b =>
                {
                    b.Property<string>("ApprovalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApprovalOrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ApprovalId");

                    b.HasIndex("ApprovalOrderId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Approver")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Customer")
                        .HasColumnType("bigint");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.Reference", b =>
                {
                    b.Property<string>("ApprovalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
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
                        .HasForeignKey("ReferenceApprovalId");

                    b.Navigation("Reference");
                });

            modelBuilder.Entity("Domain.Entities.Config", b =>
                {
                    b.HasOne("Domain.Entities.Order", "Approval")
                        .WithOne("Config")
                        .HasForeignKey("Domain.Entities.Config", "ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");
                });

            modelBuilder.Entity("Domain.Entities.Document", b =>
                {
                    b.HasOne("Domain.Entities.Order", "Approval")
                        .WithMany("Documents")
                        .HasForeignKey("ApprovalOrderId");

                    b.Navigation("Approval");
                });

            modelBuilder.Entity("Domain.Entities.Reference", b =>
                {
                    b.HasOne("Domain.Entities.Order", "Approval")
                        .WithOne("Reference")
                        .HasForeignKey("Domain.Entities.Reference", "ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("Config");

                    b.Navigation("Documents");

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
