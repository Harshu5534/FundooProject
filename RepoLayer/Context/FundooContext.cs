using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    public class FundooContext : DbContext
    {
        internal IEnumerable<object> UserEntity;

        public FundooContext(DbContextOptions options)
                : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserEntity> UserLogin { get; set; }
    }
}
