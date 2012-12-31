using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EfCrutches
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class Message
    {
        public int Id { get; set; }
        public bool IsRead { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        [Required]
        public virtual MessageThread Thread { get; set; }
        [Required]
        public virtual Account Owner { get; set; }
        [Required]
        public virtual Account Sender { get; set; }
        [Required]
        public virtual Account Receiver { get; set; }
    }

    public class MessageThread
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Subject { get; set; }

        public virtual ICollection<MessageFolder> Folders { get; set; }
    }

    public class MessageFolder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public Account Owner { get; set; }

        public virtual ICollection<MessageThread> Threads { get; set; }
    }

    public class CrutchContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageThread> Threads { get; set; }
        public DbSet<MessageFolder> Folders { get; set; }

        protected CrutchContext()
        {
        }

        public CrutchContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Message>().HasRequired(f => f.Sender).WithMany();
            modelBuilder.Entity<Message>().HasRequired(f => f.Thread).WithMany();
            modelBuilder.Entity<Message>().HasRequired(f => f.Receiver).WithMany();
            modelBuilder.Entity<Message>().HasRequired(f => f.Owner).WithMany();
            modelBuilder.Entity<MessageFolder>().HasMany(f => f.Threads).WithMany(f=>f.Folders).Map(f => f.ToTable("ThreadsInFolders"));
            modelBuilder.Entity<MessageFolder>().HasRequired(f => f.Owner).WithMany();
            

            base.OnModelCreating(modelBuilder);
        }

        public CrutchContext WithoutChangesDetection()
        {
            Configuration.AutoDetectChangesEnabled = false;
            return this;
        }

        public CrutchContext WithoutValidation()
        {
            Configuration.ValidateOnSaveEnabled = false;
            return this;
        }
    }
}