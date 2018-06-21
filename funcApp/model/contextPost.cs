namespace funcApp.model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class contextPost : DbContext
    {

        public contextPost(string connectionString)
            : base(connectionString)
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<PostData> PostData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PostDataMap());
        }
    }

    
    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}