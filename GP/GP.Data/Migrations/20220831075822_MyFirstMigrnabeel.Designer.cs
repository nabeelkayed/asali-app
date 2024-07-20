﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealWord.Data;

namespace GP.Data.Migrations
{
    [DbContext(typeof(GPDbContext))]
    [Migration("20220831075822_MyFirstMigrnabeel")]
    partial class MyFirstMigrnabeel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GP.Data.Entities.BusinessOwner", b =>
                {
                    b.Property<Guid>("BusinessOwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Setup")
                        .HasColumnType("bit");

                    b.HasKey("BusinessOwnerId");

                    b.HasIndex("BusinessId")
                        .IsUnique();

                    b.ToTable("BusinessOwners");
                });

            modelBuilder.Entity("RealWord.Data.Entities.Business", b =>
                {
                    b.Property<Guid>("BusinessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MenuWebsite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BusinessId");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("RealWord.Data.Entities.BusinessFollowers", b =>
                {
                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BusinessId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("BusinessFollowers");
                });

            modelBuilder.Entity("RealWord.Data.Entities.OpenDay", b =>
                {
                    b.Property<Guid>("OpenDayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("OpenDayId");

                    b.HasIndex("BusinessId");

                    b.ToTable("OpenDays");
                });

            modelBuilder.Entity("RealWord.Data.Entities.Photo", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhotoName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PhotoId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("ReviewId");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("RealWord.Data.Entities.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sentement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReviewId");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("RealWord.Data.Entities.ReviewCool", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReviewId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ReviewCool");
                });

            modelBuilder.Entity("RealWord.Data.Entities.ReviewFunny", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReviewId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ReviewFunny");
                });

            modelBuilder.Entity("RealWord.Data.Entities.ReviewUseful", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReviewId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ReviewUseful");
                });

            modelBuilder.Entity("RealWord.Data.Entities.Service", b =>
                {
                    b.Property<Guid>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceId");

                    b.HasIndex("BusinessId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("RealWord.Data.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadLine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GP.Data.Entities.BusinessOwner", b =>
                {
                    b.HasOne("RealWord.Data.Entities.Business", "Business")
                        .WithOne("BusinessOwner")
                        .HasForeignKey("GP.Data.Entities.BusinessOwner", "BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("RealWord.Data.Entities.BusinessFollowers", b =>
                {
                    b.HasOne("RealWord.Data.Entities.Business", "Business")
                        .WithMany("Followers")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWord.Data.Entities.User", "User")
                        .WithMany("Followings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealWord.Data.Entities.OpenDay", b =>
                {
                    b.HasOne("RealWord.Data.Entities.Business", "Business")
                        .WithMany("OpenDays")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("RealWord.Data.Entities.Photo", b =>
                {
                    b.HasOne("RealWord.Data.Entities.Business", "Business")
                        .WithMany("Photos")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWord.Data.Entities.Review", "Review")
                        .WithMany("Photos")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RealWord.Data.Entities.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Review");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealWord.Data.Entities.Review", b =>
                {
                    b.HasOne("RealWord.Data.Entities.Business", "Business")
                        .WithMany("Reviews")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWord.Data.Entities.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealWord.Data.Entities.ReviewCool", b =>
                {
                    b.HasOne("RealWord.Data.Entities.Review", "Review")
                        .WithMany("Cool")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWord.Data.Entities.User", "User")
                        .WithMany("Cool")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealWord.Data.Entities.ReviewFunny", b =>
                {
                    b.HasOne("RealWord.Data.Entities.Review", "Review")
                        .WithMany("Funny")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWord.Data.Entities.User", "User")
                        .WithMany("Funny")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealWord.Data.Entities.ReviewUseful", b =>
                {
                    b.HasOne("RealWord.Data.Entities.Review", "Review")
                        .WithMany("Useful")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealWord.Data.Entities.User", "User")
                        .WithMany("Useful")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Review");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealWord.Data.Entities.Service", b =>
                {
                    b.HasOne("RealWord.Data.Entities.Business", "Business")
                        .WithMany("Services")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("RealWord.Data.Entities.Business", b =>
                {
                    b.Navigation("BusinessOwner");

                    b.Navigation("Followers");

                    b.Navigation("OpenDays");

                    b.Navigation("Photos");

                    b.Navigation("Reviews");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("RealWord.Data.Entities.Review", b =>
                {
                    b.Navigation("Cool");

                    b.Navigation("Funny");

                    b.Navigation("Photos");

                    b.Navigation("Useful");
                });

            modelBuilder.Entity("RealWord.Data.Entities.User", b =>
                {
                    b.Navigation("Cool");

                    b.Navigation("Followings");

                    b.Navigation("Funny");

                    b.Navigation("Photos");

                    b.Navigation("Reviews");

                    b.Navigation("Useful");
                });
#pragma warning restore 612, 618
        }
    }
}
