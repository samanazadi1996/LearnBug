﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LearnBug.Models.DomainModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LearnBugDBEntities1 : DbContext
    {
        public LearnBugDBEntities1()
            : base("name=LearnBugDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bookmark> Bookmarks { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
