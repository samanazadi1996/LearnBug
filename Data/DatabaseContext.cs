using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DatabaseContext>());
        }
        public DbSet<User> users { get; set; }
        public DbSet<Content> contents { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Factor> factors { get; set; }
        public DbSet<Setting> settings { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Bookmark> bookmarks { get; set; }
        public DbSet<Follow> follows { get; set; }
    }
}
