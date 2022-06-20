using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Register> Registers { get; set; }
        public DbSet<PoliceStation> PoliceStations { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<Patrol> Patrols { get; set; }
        public DbSet<Policeman> Policemans { get; set; }
        public DbSet<Crime> Crimes { get; set; }
        public DbSet<PoliceCar> PoliceCars { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Region_City> Region_Citys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Felony> Felonys { get; set; }

        public class EFDbContextFactory : IDesignTimeDbContextFactory<EFDbContext>
        {
            public EFDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<EFDbContext>();
                optionsBuilder.UseSqlServer(
                    "Server = (localdb)\\mssqllocaldb; Database = police database; Trusted_Connection = True; MultipleActiveResultSets = true");
                // optionsBuilder.UseSqlServer(
                //     "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\STUDIA\\Semestr 6\\Programowanie aplikacji w WPF\\Projekt\\WPF - projekt\\WPFprojekt\\EntityFramework\\DB\\Database1.mdf\";Integrated Security=True");
                return new EFDbContext(optionsBuilder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Uzytkownicy
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Login = "admin", Password = "admin", Role = "admin", PolicemanId = 1 },
                new User { UserId = 2, Login = "bruh", Password = "bruh", Role = "", PolicemanId=5 },
                new User { UserId = 3, Login = "xxx", Password = "xxx", Role = "", PolicemanId = 3 }

                );

            //Miasta
            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, Name = "Białystok"},
                new City { CityId = 2, Name = "Kraków"},
                new City { CityId = 3, Name = "Warszawa"},
                new City { CityId = 4, Name = "Rzeszów"},
                new City { CityId = 5, Name = "Łódź" },
                new City { CityId = 6, Name = "Gdańsk" },
                new City { CityId = 7, Name = "Katowice" },
                new City { CityId = 8, Name = "Wrocław" },
                new City { CityId = 9, Name = "Poznań" }
                );

            //Komendy
            /*
                public int PoliceStationId { get; set; }
                public string Address { get; set; }
                public int RegionId { get; set; }
                public Region_City Region { get; set; }
             */
            modelBuilder.Entity<PoliceStation>().HasData(
               new PoliceStation { PoliceStationId = 1, Address = "Panel Admina", Region_CityId = 1, IsActive=false },
               new PoliceStation { PoliceStationId = 2, Address = "Zawadiaków 14", Region_CityId = 2 },
               new PoliceStation { PoliceStationId = 3, Address = "Mirosławska 15", Region_CityId = 4 },
               new PoliceStation { PoliceStationId = 4, Address = "Piątków 21/7", Region_CityId = 5 },
               new PoliceStation { PoliceStationId = 5, Address = "Miłosierdzia Pańskiego 2137", Region_CityId = 6 },
               new PoliceStation { PoliceStationId = 6, Address = "Obi-Wana Kenobiego 3", Region_CityId = 7 },
               new PoliceStation { PoliceStationId = 7, Address = "Plackowa 98", Region_CityId = 8 },
               new PoliceStation { PoliceStationId = 8, Address = "Iglasta 41", Region_CityId = 3 },
               new PoliceStation { PoliceStationId = 9, Address = "Chrobrego 21", Region_CityId = 9 },
               new PoliceStation { PoliceStationId = 10, Address = "Muchomorska 9", Region_CityId = 1 }

               );
            //Region-Miata
            /*
                public int RegionId { get; set; }
                public int ID_miasta { get; set; }
                public City City { get; set; }
                public string Name { get; set; }
                public string DangerStage { get; set; }
            */
            modelBuilder.Entity<Region_City>().HasData(
              new Region_City { Region_CityId = 1, CityId = 1, DangerStage="Niski", Name="Piasta" },
              new Region_City { Region_CityId = 2, CityId = 1, DangerStage="Wysoki", Name="Skorupy" },
              new Region_City { Region_CityId = 3, CityId = 5, DangerStage="Śmiertelny", Name="Bałuty" },
              new Region_City { Region_CityId = 4, CityId = 9, DangerStage="Średni", Name="Paciorków" },
              new Region_City { Region_CityId = 5, CityId = 4, DangerStage="Niski", Name="Puchatkowo" },
              new Region_City { Region_CityId = 6, CityId = 3, DangerStage="Wysoki", Name="Niski Stok" },
              new Region_City { Region_CityId = 7, CityId = 3, DangerStage="Niski", Name="Średnia Górka" },
              new Region_City { Region_CityId = 8, CityId = 8, DangerStage="Średni", Name="Swoja" },
              new Region_City { Region_CityId = 9, CityId = 7, DangerStage="Śmiertelny", Name="Chmurzyńska Wieś" }
              );
            modelBuilder.Entity<Register>().HasData(
                new Register {RegisterId=1,FirstName="Zuzanna", Surname="Bagińska",Age=21, Picture= File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images\\stock1.jpg")) },
                new Register { RegisterId = 2, FirstName = "Lewis", Surname = "Hamilto", Age = 37, Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images\\stock2.jpg")) },
                new Register { RegisterId = 3, FirstName = "Bruno", Surname = "Czech", Age = 20, Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images\\stock3.jpg")) },
                new Register { RegisterId = 4, FirstName = "Stachu", Surname = "Jones", Age = 74, Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images\\stock4.jpg")) },
                new Register { RegisterId = 5, FirstName = "Stanisław", Surname = "Wokulski", Age = 30, Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images\\stock5.jpg")) },
                new Register { RegisterId = 6, FirstName = "Witold", Surname = "Gombrowicz", Age = 43, Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images\\stock6.jpg")) },
                new Register { RegisterId = 7, FirstName = "Piotrek", Surname = "Parker", Age = 30, Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images\\stock7.jpg")) },
                new Register { RegisterId = 8, FirstName = "Sara", Surname = "Sudoł", Age = 23, Picture = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images\\stock8.jpg")) }
                );
            modelBuilder.Entity<Policeman>().HasData(
                new Policeman { PolicemanId = 1, FirstName = "Admin", Surname = "Admin", RangaId = 18, PoliceStationId = 1, IsActive = false },
                new Policeman { PolicemanId = 2, FirstName = "Krzysztof", Surname = "Gonciarz", RangaId = 2, PoliceStationId = 2 },
                new Policeman { PolicemanId = 3, FirstName = "Tomasz", Surname = "Działowy", RangaId = 3, PoliceStationId = 3 },
                new Policeman { PolicemanId = 4, FirstName = "Antoni", Surname = "Macierewicz", RangaId = 4, PoliceStationId = 4 },
                new Policeman { PolicemanId = 5, FirstName = "Darth", Surname = "Vader", RangaId = 5, PoliceStationId = 2 },
                new Policeman { PolicemanId = 6, FirstName = "Adam", Surname="Pogorzelski", RangaId=1, PoliceStationId=2 }
            );
            modelBuilder.Entity<Rank>().HasData(
                new Rank { RangaId = 1, Name = "Posterunkowy", Salary = 2800 },
                new Rank { RangaId = 2, Name = "Starszy Posterunkowy", Salary = 2900 },
                new Rank { RangaId = 3, Name = "Sierżant", Salary = 3000 },
                new Rank { RangaId = 4, Name = "Starszy Sierżant", Salary = 3100 },
                new Rank { RangaId = 5, Name = "Sierżant Sztabowy", Salary = 3200 },
                new Rank { RangaId = 6, Name = "Młodszy Aspirant", Salary = 3300 },
                new Rank { RangaId = 7, Name = "Aspirant", Salary = 3400 },
                new Rank { RangaId = 8, Name = "Starszy Aspirant", Salary = 3500 },
                new Rank { RangaId = 9, Name = "Aspirant Sztabowy", Salary = 3600 },
                new Rank { RangaId = 10, Name = "Podkomisarz", Salary = 3700 },
                new Rank { RangaId = 11, Name = "Komisarz", Salary = 3800 },
                new Rank { RangaId = 12, Name = "Nadkomisarz", Salary = 3900 },
                new Rank { RangaId = 13, Name = "Podinspektor", Salary = 4000 },
                new Rank { RangaId = 14, Name = "Młodszy Inspektor", Salary = 4100 },
                new Rank { RangaId = 15, Name = "Inspektor", Salary = 4200 },
                new Rank { RangaId = 16, Name = "Nadinspektor", Salary = 4300 },
                new Rank { RangaId = 17, Name = "Generalny Inspektor", Salary = 4400 },
                new Rank { RangaId = 18, Name = "Admin", Salary = 0, IsActive=false }

                );
            modelBuilder.Entity<PoliceCar>().HasData(
                new PoliceCar { PoliceCarId = 1, Model = "M3", Brand = "BMW", ProductionYear = 2016 },
                new PoliceCar { PoliceCarId = 2, Model = "M3", Brand = "BMW", ProductionYear = 2016 },
                new PoliceCar { PoliceCarId = 3, Model = "M3", Brand = "BMW", ProductionYear = 2016 },
                new PoliceCar { PoliceCarId = 4, Model = "M3", Brand = "BMW", ProductionYear = 2016 },
                new PoliceCar { PoliceCarId = 5, Model = "M3", Brand = "BMW", ProductionYear = 2016 },
                new PoliceCar { PoliceCarId = 6, Model = "M3", Brand = "BMW", ProductionYear = 2016 }
                );
        }
    }
}
