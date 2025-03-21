﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AMLDotNetCore.DataBase.Models;

public partial class AppDbContext : DbContext
{
  

    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        string connectionString = "Data Source=.;Initial Catalog=BlogManagementDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
    //        optionsBuilder.UseSqlServer(connectionString);
    //    }
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthot).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
