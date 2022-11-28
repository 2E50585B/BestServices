using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BestServices.Model.DataBase
{
    /// <summary>
    /// Представляет собой объект Базы Данных
    /// </summary>
    public partial class BestServicesEntities : DbContext
    {
        public BestServicesEntities() : base("name=BestServicesEntities") { }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SelectedServices> SelectedServices { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}