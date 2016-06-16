using LMS.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LMSContext : DbContext
    {
        #region Constructor

        public LMSContext(DbContextOptions<LMSContext> options) : base (options)
        {

        }

        #endregion Constructor

        #region DBSets

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        #endregion DBSets

        #region ModelBuilder

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<PostTag>()
                .HasKey(t => new { t.PostId, t.TagId});

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<PostCategory>()
                .HasKey(t => new { t.PostId, t.CategoryId});

            modelBuilder.Entity<PostCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.PostCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<PostComment>()
                .HasKey(c => new { c.PostId, c.CommentId});

            modelBuilder.Entity<PostComment>()
                .HasOne(pc => pc.Comment)
                .WithMany(c => c.PostComments)
                .HasForeignKey(pc => pc.CommentId);
        }

        #endregion ModelBuilder
    }
}