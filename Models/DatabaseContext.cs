using Models.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Models
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DatabaseContext() : base()
        {
        }

        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Follow> Follows { get; set; }


        public virtual int rebuildIndex()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("rebuildIndex");
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new User.Configuration());
            modelBuilder.Configurations.Add(new Content.Configuration());
            modelBuilder.Configurations.Add(new Message.Configuration());
            modelBuilder.Configurations.Add(new Follow.Configuration());
            modelBuilder.Configurations.Add(new Comment.Configuration());
            modelBuilder.Configurations.Add(new Bookmark.Configuration());
            modelBuilder.Configurations.Add(new Factor.Configuration());
            modelBuilder.Configurations.Add(new Group.Configuration());
            modelBuilder.Configurations.Add(new Transaction.Configuration());
            modelBuilder.Configurations.Add(new Setting.Configuration());
        }








    }
}









