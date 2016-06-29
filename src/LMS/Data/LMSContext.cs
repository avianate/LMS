using LMS.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LMSContext : IdentityDbContext<User>
    {
        #region Constructor

        public LMSContext(DbContextOptions<LMSContext> options) : base(options)
        {

        }

        #endregion Constructor

        #region DBSets

        /// <summary>
        ///     Posts DbSet
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        ///     Comments DbSet
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        ///     Categories DbSet
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        ///     Tags DbSet
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        ///     PostTags DbSet
        /// </summary>
        public DbSet<PostTag> PostTags { get; set; }

        /// <summary>
        ///     PostCategories DbSet
        /// </summary>
        public DbSet<PostCategory> PostCategories { get; set; }

        /// <summary>
        ///     PostComments DbSet
        /// </summary>
        public DbSet<PostComment> PostComments { get; set; }

        /// <summary>
        ///     Coures DbSet
        /// </summary>
        public DbSet<Course> Courses { get; set; }

        #endregion DBSets

        #region ModelBuilder

        /// <summary>
        ///     Sets the navigation property keys
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTag>()
                .HasKey(t => new { t.PostId, t.TagId });

            // modelBuilder.Entity<PostTag>()
            //     .HasOne(pt => pt.Tag)
            //     .WithMany(t => t.PostTags)
            //     .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<PostCategory>()
                .HasKey(t => new { t.PostId, t.CategoryId });

            // modelBuilder.Entity<PostCategory>()
            //     .HasOne(pc => pc.Category)
            //     .WithMany(c => c.PostCategories)
            //     .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<PostComment>()
                .HasKey(c => new { c.PostId, c.CommentId });

            // modelBuilder.Entity<PostComment>()
            //     .HasOne(pc => pc.Comment)
            //     .WithMany(c => c.PostComments)
            //     .HasForeignKey(pc => pc.CommentId);
        }

        #endregion ModelBuilder
    }
}