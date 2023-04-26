using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JR_API.Models;

public partial class JrDbContext : DbContext
{
    public JrDbContext()
    {
    }

    public JrDbContext(DbContextOptions<JrDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Calificacion> Calificacions { get; set; }

    public virtual DbSet<CategorieBook> CategorieBooks { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Reseña> Reseñas { get; set; }

    public virtual DbSet<Reto> Retos { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)

        {

            IConfigurationRoot configuration = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

                        .AddJsonFile("appsettings.json")

                        .Build();

            var connectionString = configuration.GetConnectionString("JRDB");

            optionsBuilder.UseMySQL(connectionString);

        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBook).HasName("PRIMARY");

            entity.ToTable("books");

            entity.HasIndex(e => e.IdCategorie, "Books_fk0");

            entity.HasIndex(e => e.IdUser, "Books_fk1");

            entity.Property(e => e.IdBook).HasColumnName("id_book");
            entity.Property(e => e.AuthorBook)
                .HasMaxLength(100)
                .HasColumnName("author_book");
            entity.Property(e => e.DateBook)
                .HasColumnType("date")
                .HasColumnName("date_book");
            entity.Property(e => e.IdCategorie).HasColumnName("id_categorie");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.NameBook)
                .HasMaxLength(100)
                .HasColumnName("name_book");
            entity.Property(e => e.BookPublish)
                .HasColumnType("date")
                .HasColumnName("book_publish");

            entity.HasOne(d => d.IdCategorieNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdCategorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Books_fk0");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Books_fk1");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.IdCalificacion).HasName("PRIMARY");

            entity.ToTable("calificacion");

            entity.HasIndex(e => e.IdUser, "Calificacion_fk0");

            entity.Property(e => e.IdCalificacion).HasColumnName("id_calificacion");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.LimiteInferior).HasColumnName("limite_inferior");
            entity.Property(e => e.LimiteSuperior).HasColumnName("limite_superior");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Calificacions)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Calificacion_fk0");
        });

        modelBuilder.Entity<CategorieBook>(entity =>
        {
            entity.HasKey(e => e.IdCategorie).HasName("PRIMARY");

            entity.ToTable("categorie_book");

            entity.Property(e => e.IdCategorie).HasColumnName("id_categorie");
            entity.Property(e => e.NameCategorie)
                .HasMaxLength(50)
                .HasColumnName("name_categorie");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.IdGoal).HasName("PRIMARY");

            entity.ToTable("goal");

            entity.Property(e => e.IdGoal).HasColumnName("id_goal");
            entity.Property(e => e.GoalBook).HasColumnName("goal_book");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Progress).HasColumnName("progress");
        });

        modelBuilder.Entity<Reseña>(entity =>
        {
            entity.HasKey(e => e.IdReseña).HasName("PRIMARY");

            entity.ToTable("reseñas");

            entity.HasIndex(e => e.IdUser, "Reseñas_fk0");

            entity.Property(e => e.IdReseña).HasColumnName("id_reseña");
            entity.Property(e => e.AuthorBook)
                .HasMaxLength(100)
                .HasColumnName("author_book");
            entity.Property(e => e.DescriptionReseña)
                .HasMaxLength(500)
                .HasColumnName("Description_reseña");
            entity.Property(e => e.FavoritePhrase)
                .HasMaxLength(200)
                .HasColumnName("Favorite_Phrase");
            entity.Property(e => e.GeneroBook)
                .HasMaxLength(100)
                .HasColumnName("genero_book");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.NameBook)
                .HasMaxLength(100)
                .HasColumnName("name_book");
            entity.Property(e => e.PagesBook).HasColumnName("pages_book");
            entity.Property(e => e.PuntuacionBook).HasColumnName("puntuacion_book");
            entity.Property(e => e.PuntuacionPersonajes).HasColumnName("puntuacion_personajes");
            entity.Property(e => e.PuntuacionTrama).HasColumnName("puntuacion_trama");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Reseñas)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reseñas_fk0");
        });

        modelBuilder.Entity<Reto>(entity =>
        {
            entity.HasKey(e => e.IdReto).HasName("PRIMARY");

            entity.ToTable("retos");

            entity.HasIndex(e => e.IdUser, "id_user");

            entity.Property(e => e.IdReto).HasColumnName("id_reto");
            entity.Property(e => e.DateEnd)
                .HasColumnType("date")
                .HasColumnName("date_end");
            entity.Property(e => e.DateStart)
                .HasColumnType("date")
                .HasColumnName("date_start");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.NombreReto)
                .HasMaxLength(50)
                .HasColumnName("nombre_reto");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PRIMARY");

            entity.ToTable("status");

            entity.Property(e => e.IdStatus).HasColumnName("id_status");
            entity.Property(e => e.NameStatus)
                .HasMaxLength(50)
                .HasColumnName("name_status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("Full_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_name");
            entity.Property(e => e.NumberPhone).HasColumnName("Number_phone");
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
