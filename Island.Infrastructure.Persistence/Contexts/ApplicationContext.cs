using Island.Core.Domain.Common;
using Island.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Island.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var currentUserName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = currentUserName ?? "UserRegistration";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = currentUserName;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<ContainerType> ContainerTypes { get; set; }
        public DbSet<ContainerStatus> ContainerStatus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region tables
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Container>().ToTable("Containers");
            modelBuilder.Entity<ContainerType>().ToTable("ContainerTypes");
            modelBuilder.Entity<ContainerStatus>().ToTable("ContainerStatus");
            #endregion

            #region primary keys
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<Container>().HasKey(Container => Container.Id);
            modelBuilder.Entity<ContainerType>().HasKey(ContainerType => ContainerType.Id);
            modelBuilder.Entity<ContainerStatus>().HasKey(ContainerStatus => ContainerStatus.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<ContainerType>()
                .HasMany(container => container.Containers)
                .WithOne(container => container.ContainerType)
                .HasForeignKey(container => container.ContainerTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContainerStatus>()
                .HasMany(container => container.Containers)
                .WithOne(container => container.ContainerStatus)
                .HasForeignKey(container => container.ContainerStatusId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region property configurations

            #region User
            modelBuilder.Entity<User>().Property(p => p.Name)
                .IsRequired().HasMaxLength(60);

            modelBuilder.Entity<User>().Property(p => p.Email)
                .IsRequired().HasMaxLength(100);

            modelBuilder.Entity<User>().Property(p => p.UserName)
                .IsRequired().HasMaxLength(25);

            modelBuilder.Entity<User>().Property(p => p.Password)
                .IsRequired();

            modelBuilder.Entity<User>().Property(p => p.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<User>().Property(p => p.CreatedBy)
                .IsRequired().HasMaxLength(60);

            modelBuilder.Entity<User>().Property(p => p.LastModifiedDate);

            modelBuilder.Entity<User>().Property(p => p.LastModifiedBy)
                .HasMaxLength(60);
            #endregion

            #region Container
            modelBuilder.Entity<Container>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);

            modelBuilder.Entity<Container>().Property(p => p.Location)
                .IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Container>().Property(p => p.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<Container>().Property(p => p.CreatedBy)
                .IsRequired().HasMaxLength(60);

            modelBuilder.Entity<Container>().Property(p => p.LastModifiedDate);

            modelBuilder.Entity<Container>().Property(p => p.LastModifiedBy)
                .HasMaxLength(60);
            #endregion

            #region ContainerType
            modelBuilder.Entity<ContainerType>().Property(p => p.Description)
                .IsRequired().HasMaxLength(20);
            #endregion

            #region ContainerStatus
            modelBuilder.Entity<ContainerStatus>().Property(p => p.Description)
                .IsRequired().HasMaxLength(20);
            #endregion

            #endregion
        }
    }
}
