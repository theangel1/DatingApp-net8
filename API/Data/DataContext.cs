using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

//painful to do this, but neccesary because we are using ints as id keys
public class DataContext(DbContextOptions options) : IdentityDbContext<
AppUser,
AppRole, int,
IdentityUserClaim<int>,
AppUserRole,
IdentityUserLogin<int>,
IdentityRoleClaim<int>
, IdentityUserToken<int>>(options)
{
    public DbSet<UserLike> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    public DbSet<Group> Groups{ get; set; }
    public DbSet<Connection> Connections { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<AppUser>()
    .HasMany(u => u.UserRoles)
    .WithOne(u => u.User)
    .HasForeignKey(u => u.UserId)
    .IsRequired();

    builder.Entity<AppRole>()
  .HasMany(u => u.UserRoles)
  .WithOne(u => u.Role)
  .HasForeignKey(u => u.RoleId)
  .IsRequired();


    builder.Entity<UserLike>()
    .HasKey(k => new { k.SourceUserId, k.TargetUserId });

    //ojo que en sql server no le gusta mucho cascade, por lo que habria que usar NoAction
    builder.Entity<UserLike>()
    .HasOne(s => s.SourceUser)
    .WithMany(l => l.LikedUsers)
    .HasForeignKey(s => s.SourceUserId)
    .OnDelete(DeleteBehavior.Cascade);

    builder.Entity<UserLike>()
   .HasOne(s => s.TargetUser)
   .WithMany(l => l.LikedByUsers)
   .HasForeignKey(s => s.TargetUserId)
   .OnDelete(DeleteBehavior.Cascade);

    builder.Entity<Message>()
   .HasOne(x => x.Recipient)
   .WithMany(x => x.MessagesReceived)
   .OnDelete(DeleteBehavior.Restrict);


    builder.Entity<Message>()
      .HasOne(x => x.Sender)
      .WithMany(x => x.MessagesSent)
      .OnDelete(DeleteBehavior.Restrict);

  }
}
