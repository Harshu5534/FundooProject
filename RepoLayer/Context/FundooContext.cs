using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    public class fundooContext : DbContext
    {
        public fundooContext(DbContextOptions options)
                : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<NoteEntity> NotesTable { get; set; }
        public DbSet<CollabEntity> Collaborator { get; set; }
        public DbSet<LabelEntity> Labels { get; set; }
    }
}
