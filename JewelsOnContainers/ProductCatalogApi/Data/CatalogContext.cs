using Microsoft.EntityFrameworkCore;
using ProductCatalogApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogApi.Data
{
    public class CatalogContext: DbContext
    {

        // Entity framework pipeline - plug and play
        public CatalogContext(DbContextOptions options)  // Where  (Dependancy injection) Module 10
            : base (options)
        { }


        //Below are What (DI) Module 10
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }

        //how - override only applicable inheritance - can inherit parents behaviour
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity is also the name of the table - model equates to entity also set - USING LABMDA HERE for writing into database
            modelBuilder.Entity<CatalogBrand>(e =>
            {
                e.ToTable("CatalogBrands");
                // old way
                /*e.HasKey(b => b.Id);
                e.Property(b => b.Id).ValueGeneratedOnAdd(); */
                e.Property(b => b.Id).IsRequired().UseHiLo("catalog_brand_hilo");
                e.Property(b => b.Brand).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<CatalogType>(e =>
            {
                e.ToTable("CatalogTypes");
                e.Property(t => t.Id).IsRequired().UseHiLo("catalog_type_hilo");
                e.Property(t => t.Type).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<CatalogItem>(e => 
            {
                e.ToTable("Catalog");
                e.Property(c => c.Id).IsRequired().UseHiLo("catalog_hilo");
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
                e.Property(c => c.Price).IsRequired();
                //each catalog item has one relationship with the catalog type but catalog type has many relation with catalog item
                // if foreign key is not specified here entity framework auto created a hidden column in the table which we do not have access to
                e.HasOne(c => c.CatalogType).WithMany().HasForeignKey(c => c.CatalogTypeId);
                // march 1st module 10 e is the catalog item , c being the entity, virtual navigation property  to the catalog type
                // establishing foreign key here, 2 properties, catalogtypeid key and catalog type is the relation ship
                // relationship is to the table e is the entity catalog item - about the schema 
                e.HasOne(c => c.CatalogBrand).WithMany().HasForeignKey(c => c.CatalogBrandId);
            });
        }
    }
}
