using Microsoft.EntityFrameworkCore;
using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.Data
{
    public class NoteDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NoteDbContext(DbContextOptions<NoteDbContext> contextOptions)
            : base(contextOptions)
        {
            Database.EnsureCreated();   
        }
    }
}
