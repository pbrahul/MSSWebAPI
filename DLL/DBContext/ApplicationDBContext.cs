using DLL.Model;
using DLL.Model.Interfaces;
using DLL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DLL.DBContext
{
    public class ApplicationDBContext : IdentityDbContext <AppUser, AppRole,int,IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int> >
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDBContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private static readonly MethodInfo _propertyMethod = typeof(EF)
            .GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public)?.MakeGenericMethod(typeof(bool));

        private const  string IsDeletedProperty = "IsDeleted";
       public DbSet<CustomerBalance> CustomerBalances { get; set; }

        public DbSet<Order> Orders { get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entity.ClrType))
                {
                    entity.AddProperty(IsDeletedProperty, typeof(bool)); // deletedAt
                    modelBuilder.Entity(entity.ClrType).HasQueryFilter(GetIsDeletedRestriction(entity.ClrType));
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            BeforeSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void BeforeSaveChanges()
        {
            var entries = ChangeTracker.Entries();
            var userName = GetCurrentUser();

            foreach (var entity in entries)
            {
                var nowTime = DateTime.Now;
                if (entity.Entity is ITrackable trackable)
                {
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            trackable.CreatedAt = nowTime;
                            trackable.UpdatedAt = nowTime;
                            trackable.CreatedBy = userName;
                            trackable.UpdatedBy = userName;
                            break;
                        case EntityState.Modified:
                            trackable.UpdatedAt = nowTime;
                            trackable.UpdatedBy = userName;
                            break;
                        case EntityState.Deleted:
                            entity.Property(IsDeletedProperty).CurrentValue = true;
                            entity.State = EntityState.Modified;
                            trackable.UpdatedAt = nowTime;
                            break;
                    }
                }
            }
        }

        private string GetCurrentUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                var userName = httpContext.User.FindFirst(x => x.Type == CustomJwtClaimsName.UserName)?.Value;
                return userName;
            }

            return null;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var parm = Expression.Parameter(type, "it");
            var prop = Expression.Call(_propertyMethod, parm, Expression.Constant(IsDeletedProperty));
            var condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parm);
            return lambda;
        }
    }

}
