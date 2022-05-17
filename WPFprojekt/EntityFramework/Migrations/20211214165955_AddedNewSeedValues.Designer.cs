﻿// <auto-generated />
using System;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFramework.Migrations
{
    [DbContext(typeof(EFDbContext))]
    [Migration("20211214165955_AddedNewSeedValues")]
    partial class AddedNewSeedValues
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityFramework.Models.Kartoteka", b =>
                {
                    b.Property<int>("KartotekaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Imie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Wiek")
                        .HasColumnType("int");

                    b.Property<byte[]>("Zdjecie")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("KartotekaId");

                    b.ToTable("Kartotekas");

                    b.HasData(
                        new
                        {
                            KartotekaId = 1,
                            Imie = "Zuzanna",
                            Nazwisko = "Bagińska",
                            Wiek = 21
                        },
                        new
                        {
                            KartotekaId = 2,
                            Imie = "Lewis",
                            Nazwisko = "Hamilto",
                            Wiek = 37
                        },
                        new
                        {
                            KartotekaId = 3,
                            Imie = "Bruno",
                            Nazwisko = "Czech",
                            Wiek = 20
                        },
                        new
                        {
                            KartotekaId = 4,
                            Imie = "Stachu",
                            Nazwisko = "Jones",
                            Wiek = 74
                        },
                        new
                        {
                            KartotekaId = 5,
                            Imie = "Stanisław",
                            Nazwisko = "Wokulski",
                            Wiek = 30
                        },
                        new
                        {
                            KartotekaId = 6,
                            Imie = "Witold",
                            Nazwisko = "Gombrowicz",
                            Wiek = 43
                        },
                        new
                        {
                            KartotekaId = 7,
                            Imie = "Piotrek",
                            Nazwisko = "Parker",
                            Wiek = 30
                        },
                        new
                        {
                            KartotekaId = 8,
                            Imie = "Sara",
                            Nazwisko = "Sudoł",
                            Wiek = 23
                        });
                });

            modelBuilder.Entity("EntityFramework.Models.Komenda", b =>
                {
                    b.Property<int>("KomendaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Region_MiastaId")
                        .HasColumnType("int");

                    b.HasKey("KomendaId");

                    b.HasIndex("Region_MiastaId");

                    b.ToTable("Komendas");

                    b.HasData(
                        new
                        {
                            KomendaId = 1,
                            Adres = "Muchomorska 9",
                            Region_MiastaId = 1
                        },
                        new
                        {
                            KomendaId = 2,
                            Adres = "Zawadiaków 14",
                            Region_MiastaId = 2
                        },
                        new
                        {
                            KomendaId = 3,
                            Adres = "Mirosławska 15",
                            Region_MiastaId = 4
                        },
                        new
                        {
                            KomendaId = 4,
                            Adres = "Piątków 21/7",
                            Region_MiastaId = 5
                        },
                        new
                        {
                            KomendaId = 5,
                            Adres = "Miłosierdzia Pańskiego 2137",
                            Region_MiastaId = 6
                        },
                        new
                        {
                            KomendaId = 6,
                            Adres = "Obi-Wana Kenobiego 3",
                            Region_MiastaId = 7
                        },
                        new
                        {
                            KomendaId = 7,
                            Adres = "Plackowa 98",
                            Region_MiastaId = 8
                        },
                        new
                        {
                            KomendaId = 8,
                            Adres = "Iglasta 41",
                            Region_MiastaId = 3
                        },
                        new
                        {
                            KomendaId = 9,
                            Adres = "Chrobrego 21",
                            Region_MiastaId = 9
                        });
                });

            modelBuilder.Entity("EntityFramework.Models.Miasto", b =>
                {
                    b.Property<int>("MiastoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MiastoId");

                    b.ToTable("Miastos");

                    b.HasData(
                        new
                        {
                            MiastoId = 1,
                            Nazwa = "Białystok"
                        },
                        new
                        {
                            MiastoId = 2,
                            Nazwa = "Kraków"
                        },
                        new
                        {
                            MiastoId = 3,
                            Nazwa = "Warszawa"
                        },
                        new
                        {
                            MiastoId = 4,
                            Nazwa = "Rzeszów"
                        },
                        new
                        {
                            MiastoId = 5,
                            Nazwa = "Łódź"
                        },
                        new
                        {
                            MiastoId = 6,
                            Nazwa = "Gdańsk"
                        },
                        new
                        {
                            MiastoId = 7,
                            Nazwa = "Katowice"
                        },
                        new
                        {
                            MiastoId = 8,
                            Nazwa = "Wrocław"
                        },
                        new
                        {
                            MiastoId = 9,
                            Nazwa = "Poznań"
                        });
                });

            modelBuilder.Entity("EntityFramework.Models.Patrol", b =>
                {
                    b.Property<int>("PatrolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data_rozpoczecia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Data_zakonczenia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Godzina_rozpoczecia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Godzina_zakonczenia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PolicjantId")
                        .HasColumnType("int");

                    b.Property<int>("RadiowozId")
                        .HasColumnType("int");

                    b.HasKey("PatrolId");

                    b.HasIndex("PolicjantId");

                    b.HasIndex("RadiowozId");

                    b.ToTable("Patrols");
                });

            modelBuilder.Entity("EntityFramework.Models.Policjant", b =>
                {
                    b.Property<int>("PolicjantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Imie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KomendaId")
                        .HasColumnType("int");

                    b.Property<string>("Nazwisko")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RangaId")
                        .HasColumnType("int");

                    b.HasKey("PolicjantId");

                    b.HasIndex("KomendaId");

                    b.HasIndex("RangaId");

                    b.ToTable("Policjants");

                    b.HasData(
                        new
                        {
                            PolicjantId = 1,
                            Imie = "Adam",
                            KomendaId = 1,
                            Nazwisko = "Pogorzelski",
                            RangaId = 1
                        },
                        new
                        {
                            PolicjantId = 2,
                            Imie = "Krzysztof",
                            KomendaId = 1,
                            Nazwisko = "Gonciarz",
                            RangaId = 2
                        },
                        new
                        {
                            PolicjantId = 3,
                            Imie = "Tomasz",
                            KomendaId = 1,
                            Nazwisko = "Działowy",
                            RangaId = 3
                        },
                        new
                        {
                            PolicjantId = 4,
                            Imie = "Antoni",
                            KomendaId = 1,
                            Nazwisko = "Macierewicz",
                            RangaId = 4
                        },
                        new
                        {
                            PolicjantId = 5,
                            Imie = "Darth",
                            KomendaId = 1,
                            Nazwisko = "Vader",
                            RangaId = 5
                        });
                });

            modelBuilder.Entity("EntityFramework.Models.Przestepstwo", b =>
                {
                    b.Property<int>("PrzestepstwoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Godzina")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrzestepstwoId");

                    b.ToTable("Przestepstwos");
                });

            modelBuilder.Entity("EntityFramework.Models.Radiowoz", b =>
                {
                    b.Property<int>("RadiowozId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Marka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rok_produkcji")
                        .HasColumnType("int");

                    b.HasKey("RadiowozId");

                    b.ToTable("Radiowozs");

                    b.HasData(
                        new
                        {
                            RadiowozId = 1,
                            Marka = "BMW",
                            Model = "M3",
                            Rok_produkcji = 2016
                        },
                        new
                        {
                            RadiowozId = 2,
                            Marka = "BMW",
                            Model = "M3",
                            Rok_produkcji = 2016
                        },
                        new
                        {
                            RadiowozId = 3,
                            Marka = "BMW",
                            Model = "M3",
                            Rok_produkcji = 2016
                        },
                        new
                        {
                            RadiowozId = 4,
                            Marka = "BMW",
                            Model = "M3",
                            Rok_produkcji = 2016
                        },
                        new
                        {
                            RadiowozId = 5,
                            Marka = "BMW",
                            Model = "M3",
                            Rok_produkcji = 2016
                        },
                        new
                        {
                            RadiowozId = 6,
                            Marka = "BMW",
                            Model = "M3",
                            Rok_produkcji = 2016
                        });
                });

            modelBuilder.Entity("EntityFramework.Models.Ranga", b =>
                {
                    b.Property<int>("RangaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Pensja")
                        .HasColumnType("float");

                    b.HasKey("RangaId");

                    b.ToTable("Rangas");

                    b.HasData(
                        new
                        {
                            RangaId = 1,
                            Nazwa = "Posterunkowy",
                            Pensja = 2800.0
                        },
                        new
                        {
                            RangaId = 2,
                            Nazwa = "Starszy Posterunkowy",
                            Pensja = 2900.0
                        },
                        new
                        {
                            RangaId = 3,
                            Nazwa = "Sierżant",
                            Pensja = 3000.0
                        },
                        new
                        {
                            RangaId = 4,
                            Nazwa = "Starszy Sierżant",
                            Pensja = 3100.0
                        },
                        new
                        {
                            RangaId = 5,
                            Nazwa = "Sierżant Sztabowy",
                            Pensja = 3200.0
                        },
                        new
                        {
                            RangaId = 6,
                            Nazwa = "Młodszy Aspirant",
                            Pensja = 3300.0
                        },
                        new
                        {
                            RangaId = 7,
                            Nazwa = "Aspirant",
                            Pensja = 3400.0
                        },
                        new
                        {
                            RangaId = 8,
                            Nazwa = "Starszy Aspirant",
                            Pensja = 3500.0
                        },
                        new
                        {
                            RangaId = 9,
                            Nazwa = "Aspirant Sztabowy",
                            Pensja = 3600.0
                        },
                        new
                        {
                            RangaId = 10,
                            Nazwa = "Podkomisarz",
                            Pensja = 3700.0
                        },
                        new
                        {
                            RangaId = 11,
                            Nazwa = "Komisarz",
                            Pensja = 3800.0
                        },
                        new
                        {
                            RangaId = 12,
                            Nazwa = "Nadkomisarz",
                            Pensja = 3900.0
                        },
                        new
                        {
                            RangaId = 13,
                            Nazwa = "Podinspektor",
                            Pensja = 4000.0
                        },
                        new
                        {
                            RangaId = 14,
                            Nazwa = "Młodszy Inspektor",
                            Pensja = 4100.0
                        },
                        new
                        {
                            RangaId = 15,
                            Nazwa = "Inspektor",
                            Pensja = 4200.0
                        },
                        new
                        {
                            RangaId = 16,
                            Nazwa = "Nadinspektor",
                            Pensja = 4300.0
                        },
                        new
                        {
                            RangaId = 17,
                            Nazwa = "Generalny Inspektor",
                            Pensja = 4400.0
                        });
                });

            modelBuilder.Entity("EntityFramework.Models.Region_Miasta", b =>
                {
                    b.Property<int>("Region_MiastaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MiastoId")
                        .HasColumnType("int");

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stopien_zagrozenia")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Region_MiastaId");

                    b.HasIndex("MiastoId");

                    b.ToTable("Region_Miastas");

                    b.HasData(
                        new
                        {
                            Region_MiastaId = 1,
                            MiastoId = 1,
                            Nazwa = "Piasta",
                            Stopien_zagrozenia = "Niski"
                        },
                        new
                        {
                            Region_MiastaId = 2,
                            MiastoId = 1,
                            Nazwa = "Skorupy",
                            Stopien_zagrozenia = "Wysoki"
                        },
                        new
                        {
                            Region_MiastaId = 3,
                            MiastoId = 5,
                            Nazwa = "Bałuty",
                            Stopien_zagrozenia = "Śmiertelny"
                        },
                        new
                        {
                            Region_MiastaId = 4,
                            MiastoId = 9,
                            Nazwa = "Paciorków",
                            Stopien_zagrozenia = "Średni"
                        },
                        new
                        {
                            Region_MiastaId = 5,
                            MiastoId = 4,
                            Nazwa = "Puchatkowo",
                            Stopien_zagrozenia = "Niski"
                        },
                        new
                        {
                            Region_MiastaId = 6,
                            MiastoId = 3,
                            Nazwa = "Niski Stok",
                            Stopien_zagrozenia = "Wysoki"
                        },
                        new
                        {
                            Region_MiastaId = 7,
                            MiastoId = 3,
                            Nazwa = "Średnia Górka",
                            Stopien_zagrozenia = "Niski"
                        },
                        new
                        {
                            Region_MiastaId = 8,
                            MiastoId = 8,
                            Nazwa = "Swoja",
                            Stopien_zagrozenia = "Średni"
                        },
                        new
                        {
                            Region_MiastaId = 9,
                            MiastoId = 7,
                            Nazwa = "Chmurzyńska Wieś",
                            Stopien_zagrozenia = "Śmiertelny"
                        });
                });

            modelBuilder.Entity("EntityFramework.Models.Uzytkownik", b =>
                {
                    b.Property<int>("UzytkownikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PolicjantId")
                        .HasColumnType("int");

                    b.Property<string>("Rola")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UzytkownikId");

                    b.HasIndex("PolicjantId");

                    b.ToTable("Uzytkowniks");

                    b.HasData(
                        new
                        {
                            UzytkownikId = 1,
                            Login = "Admin",
                            Password = "Admin",
                            Rola = "admin"
                        });
                });

            modelBuilder.Entity("EntityFramework.Models.Wykroczenia", b =>
                {
                    b.Property<int>("WykroczenieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Godzina")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WykroczenieId");

                    b.ToTable("Wykroczenias");
                });

            modelBuilder.Entity("KartotekaPrzestepstwo", b =>
                {
                    b.Property<int>("KartotekasKartotekaId")
                        .HasColumnType("int");

                    b.Property<int>("PrzestepstwosPrzestepstwoId")
                        .HasColumnType("int");

                    b.HasKey("KartotekasKartotekaId", "PrzestepstwosPrzestepstwoId");

                    b.HasIndex("PrzestepstwosPrzestepstwoId");

                    b.ToTable("KartotekaPrzestepstwo");
                });

            modelBuilder.Entity("KartotekaWykroczenia", b =>
                {
                    b.Property<int>("KartotekasKartotekaId")
                        .HasColumnType("int");

                    b.Property<int>("WykroczeniasWykroczenieId")
                        .HasColumnType("int");

                    b.HasKey("KartotekasKartotekaId", "WykroczeniasWykroczenieId");

                    b.HasIndex("WykroczeniasWykroczenieId");

                    b.ToTable("KartotekaWykroczenia");
                });

            modelBuilder.Entity("PolicjantPrzestepstwo", b =>
                {
                    b.Property<int>("PolicjantsPolicjantId")
                        .HasColumnType("int");

                    b.Property<int>("PrzestepstwosPrzestepstwoId")
                        .HasColumnType("int");

                    b.HasKey("PolicjantsPolicjantId", "PrzestepstwosPrzestepstwoId");

                    b.HasIndex("PrzestepstwosPrzestepstwoId");

                    b.ToTable("PolicjantPrzestepstwo");
                });

            modelBuilder.Entity("PolicjantWykroczenia", b =>
                {
                    b.Property<int>("PolicjantsPolicjantId")
                        .HasColumnType("int");

                    b.Property<int>("WykroczeniasWykroczenieId")
                        .HasColumnType("int");

                    b.HasKey("PolicjantsPolicjantId", "WykroczeniasWykroczenieId");

                    b.HasIndex("WykroczeniasWykroczenieId");

                    b.ToTable("PolicjantWykroczenia");
                });

            modelBuilder.Entity("EntityFramework.Models.Komenda", b =>
                {
                    b.HasOne("EntityFramework.Models.Region_Miasta", "Region_Miasta")
                        .WithMany()
                        .HasForeignKey("Region_MiastaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region_Miasta");
                });

            modelBuilder.Entity("EntityFramework.Models.Patrol", b =>
                {
                    b.HasOne("EntityFramework.Models.Policjant", "Policjant")
                        .WithMany("Patrols")
                        .HasForeignKey("PolicjantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Models.Radiowoz", "Radiowoz")
                        .WithMany()
                        .HasForeignKey("RadiowozId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Policjant");

                    b.Navigation("Radiowoz");
                });

            modelBuilder.Entity("EntityFramework.Models.Policjant", b =>
                {
                    b.HasOne("EntityFramework.Models.Komenda", "Komenda")
                        .WithMany()
                        .HasForeignKey("KomendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Models.Ranga", "Ranga")
                        .WithMany()
                        .HasForeignKey("RangaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Komenda");

                    b.Navigation("Ranga");
                });

            modelBuilder.Entity("EntityFramework.Models.Region_Miasta", b =>
                {
                    b.HasOne("EntityFramework.Models.Miasto", "Miasto")
                        .WithMany()
                        .HasForeignKey("MiastoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Miasto");
                });

            modelBuilder.Entity("EntityFramework.Models.Uzytkownik", b =>
                {
                    b.HasOne("EntityFramework.Models.Policjant", "Policjant")
                        .WithMany()
                        .HasForeignKey("PolicjantId");

                    b.Navigation("Policjant");
                });

            modelBuilder.Entity("KartotekaPrzestepstwo", b =>
                {
                    b.HasOne("EntityFramework.Models.Kartoteka", null)
                        .WithMany()
                        .HasForeignKey("KartotekasKartotekaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Models.Przestepstwo", null)
                        .WithMany()
                        .HasForeignKey("PrzestepstwosPrzestepstwoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KartotekaWykroczenia", b =>
                {
                    b.HasOne("EntityFramework.Models.Kartoteka", null)
                        .WithMany()
                        .HasForeignKey("KartotekasKartotekaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Models.Wykroczenia", null)
                        .WithMany()
                        .HasForeignKey("WykroczeniasWykroczenieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PolicjantPrzestepstwo", b =>
                {
                    b.HasOne("EntityFramework.Models.Policjant", null)
                        .WithMany()
                        .HasForeignKey("PolicjantsPolicjantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Models.Przestepstwo", null)
                        .WithMany()
                        .HasForeignKey("PrzestepstwosPrzestepstwoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PolicjantWykroczenia", b =>
                {
                    b.HasOne("EntityFramework.Models.Policjant", null)
                        .WithMany()
                        .HasForeignKey("PolicjantsPolicjantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Models.Wykroczenia", null)
                        .WithMany()
                        .HasForeignKey("WykroczeniasWykroczenieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityFramework.Models.Policjant", b =>
                {
                    b.Navigation("Patrols");
                });
#pragma warning restore 612, 618
        }
    }
}
