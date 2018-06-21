using System.Data.Entity.ModelConfiguration;

namespace funcApp.model
{
    internal class PostDataMap : EntityTypeConfiguration<PostData>
    {
        public PostDataMap()
        {
            ToTable("Posts");
            HasKey(t => t.Id);
            Property(t => t.name);
            
        }
    }
}