using PostIT.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PostIT.DAL
{
    public class ArcticleContext : DbContext
    {
        public ArcticleContext() : base("ArcticleContext")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // modelBuilder.Entity<Article>().HasMany(c => c.Tags).WithMany(i => i.Articles).Map(t => t.MapLeftKey("ArticleID").MapRightKey("TagID").ToTable("ArticleTag"));
        }
    }
}