﻿using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
    }
}