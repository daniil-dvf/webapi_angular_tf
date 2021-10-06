using Microsoft.EntityFrameworkCore;
using PetShop.DAL.EF.Entities;
using PetShop.DAL.EF.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.DAL.EF.Context
{
    public partial class PetShopContext : DbContext
    {
        public PetShopContext()
        {
        }

        public PetShopContext(DbContextOptions<PetShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Breed> Breeds { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<PetStatus> PetStatuses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VUser> VUsers { get; set; }
        public virtual DbSet<VPet> VPets { get; set; }
        public virtual DbSet<VBreed> VBreeds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("Server=FORMA811\\TFTIC;database=Animals;Integrated Security=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VUser>().ToView("VUser");
            modelBuilder.Entity<VPet>().ToView("VPet");
            modelBuilder.Entity<VBreed>().ToView("VBreed");

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            modelBuilder.Entity<Breed>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.Name, e.AnimalId })
                    .IsUnique();

                entity.Property(e => e.AnimalId)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Breeds)
                    .HasForeignKey(d => d.AnimalId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Reference)
                    .IsUnique();

                entity.Property(e => e.ImageMimeType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Breed)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.BreedId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.PetStatus)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.PetStatusId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<PetStatus>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.Salt)
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
