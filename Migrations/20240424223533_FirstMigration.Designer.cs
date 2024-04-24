﻿// <auto-generated />
using System;
using ArtDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArtDealer.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240424223533_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ArtDealer.Models.LikesAndLikers", b =>
                {
                    b.Property<int>("LikesAndLikersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LikesAndLikersId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("LikesAndLikerss");
                });

            modelBuilder.Entity("ArtDealer.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("ImageForSale")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ImageMedium")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ArtDealer.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ArtDealer.Models.LikesAndLikers", b =>
                {
                    b.HasOne("ArtDealer.Models.Post", "Wedding")
                        .WithMany("Likers")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtDealer.Models.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Wedding");
                });

            modelBuilder.Entity("ArtDealer.Models.Post", b =>
                {
                    b.HasOne("ArtDealer.Models.User", "Creator")
                        .WithMany("AllPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("ArtDealer.Models.Post", b =>
                {
                    b.Navigation("Likers");
                });

            modelBuilder.Entity("ArtDealer.Models.User", b =>
                {
                    b.Navigation("AllPosts");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}