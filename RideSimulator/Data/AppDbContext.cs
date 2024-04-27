﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RideSimulator.Models;

namespace RideSimulator.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<DriverUser> Drivers { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<RiderUser> Riders { get; set; }
    public DbSet<RideRequest>? RideRequests { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(a => a.DriverUser)
            .WithOne(d => d.User)
            .HasForeignKey<DriverUser>(d => d.UserId);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(a => a.RiderUser)
            .WithOne(r => r.User)
            .HasForeignKey<RiderUser>(r => r.UserId);

        modelBuilder.Entity<RideRequest>()
            .HasOne(b  => b.RiderUser)
            .WithMany(a => a.RideRequests).
            HasForeignKey(a => a.RiderId).OnDelete(DeleteBehavior.ClientSetNull); ;

        modelBuilder.Entity<RideRequest>()
           .HasOne(b => b.DriverUser)
           .WithMany(a => a.RideRequests).
           HasForeignKey(a => a.RequestedDriverId).OnDelete(DeleteBehavior.ClientSetNull); ;
    }


}
