﻿// <auto-generated />
using System;
using GlobalMentalityAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GlobalMentalityAPI.Migrations
{
    [DbContext(typeof(BusinessContext))]
    partial class BusinessContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GlobalMentalityAPI.Models.Business", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("ID");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("GlobalMentalityAPI.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusinessID");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleInitial")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("PhoneNum");

                    b.Property<string>("Title");

                    b.Property<int>("UserID");

                    b.HasKey("DoctorID");

                    b.HasIndex("BusinessID");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("GlobalMentalityAPI.Models.OfficeAdmin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusinessID");

                    b.Property<string>("Description");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNum");

                    b.Property<int>("UserID");

                    b.HasKey("AdminID");

                    b.HasIndex("BusinessID");

                    b.ToTable("OfficeAdmins");
                });

            modelBuilder.Entity("GlobalMentalityAPI.Models.Patient", b =>
                {
                    b.Property<int>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("BusinessID");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int>("DoctorID");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<int>("GroupInsuranceNumber");

                    b.Property<string>("InsuranceProvider");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<string>("PhoneNum");

                    b.Property<int>("UserID");

                    b.HasKey("PatientID");

                    b.HasIndex("BusinessID");

                    b.HasIndex("DoctorID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("GlobalMentalityAPI.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GlobalMentalityAPI.Models.Doctor", b =>
                {
                    b.HasOne("GlobalMentalityAPI.Models.Business")
                        .WithMany("Doctors")
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GlobalMentalityAPI.Models.OfficeAdmin", b =>
                {
                    b.HasOne("GlobalMentalityAPI.Models.Business")
                        .WithMany("OfficeAdmins")
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GlobalMentalityAPI.Models.Patient", b =>
                {
                    b.HasOne("GlobalMentalityAPI.Models.Business")
                        .WithMany("Patients")
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GlobalMentalityAPI.Models.Doctor")
                        .WithMany("Patients")
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
