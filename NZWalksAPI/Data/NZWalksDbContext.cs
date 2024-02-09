using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    
    
        public class NZWalksDbContext : DbContext
        {
            public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
            {

            }

            public DbSet<Difficulty> Difficulties { get; set; }

            public DbSet<Region> Regions { get; set; }

            public DbSet<Walk> Walks { get; set; }

             public DbSet<Image> Images { get; set; }


        //Data Seeding

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed the data for difficulties
            //Easy. Medium, Hard

            var difficulties = new List<Difficulty>()
            {
               new Difficulty
               {
                   Id = Guid.Parse("ac485118-365c-47ab-b689-108dc1a892f5"),
                   Name = "Easy"
               },
               new Difficulty
               {
                   Id = Guid.Parse("745aa1b9-d722-477f-8ff0-45ebe92d1a2c"),
                   Name = "Medium"
               },
               new Difficulty
               {
                   Id = Guid.Parse("70db6a7a-8c48-4480-aa79-87dfa933d8e9"),
                   Name = "Hard"
               }


            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            //Seed Data for Region

            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse ("21dc44fc-281a-4f62-a305-84c81abae3d9"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = null

                },
                new Region
                {
                    Id = Guid.Parse ("119992a6-5d88-412d-9be2-6dc87a288cd9"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = null

                },
                new Region
                {
                    Id = Guid.Parse ("a2a81496-0898-4877-9cfd-15aaef5f3d90"),
                    Name = "SouthLand",
                    Code = "STL",
                    RegionImageUrl = null

                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
    
}


