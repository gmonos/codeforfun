using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Zags.ProductFactory.Database;
using Zags.ProductFactory.Domain;

namespace Zags.ProductFactory.Database.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    partial class ProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Zags.ProductFactory.Domain.Coverage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsMandatory");

                    b.Property<string>("Name");

                    b.Property<int?>("PackId");

                    b.Property<int?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("PackId");

                    b.HasIndex("ProductId");

                    b.ToTable("Coverages");
                });

            modelBuilder.Entity("Zags.ProductFactory.Domain.Pack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Packs");
                });

            modelBuilder.Entity("Zags.ProductFactory.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EffectiveDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Zags.ProductFactory.Domain.Coverage", b =>
                {
                    b.HasOne("Zags.ProductFactory.Domain.Pack")
                        .WithMany("Coverages")
                        .HasForeignKey("PackId");

                    b.HasOne("Zags.ProductFactory.Domain.Product")
                        .WithMany("Coverages")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Zags.ProductFactory.Domain.Pack", b =>
                {
                    b.HasOne("Zags.ProductFactory.Domain.Product", "Product")
                        .WithMany("Packs")
                        .HasForeignKey("ProductId");
                });
        }
    }
}
