using System.Collections.Generic;

namespace BestServices.Model.DataBase
{
    /// <summary>
    /// Представляет собой определённую роль из списка ролей в БД <see cref="BestServicesEntities"/>
    /// </summary>
    public partial class Roles
    {
        /// <summary>
        /// Создаёт новую коллекцию <see cref="DataBase.Users"/>, зависимых от <see cref="Roles"/>
        /// </summary>
        public Roles()
        {
            Users = new HashSet<Users>();
        }
    
        public int ID { get; set; }
        public string RoleName { get; set; }
    
        public virtual ICollection<Users> Users { get; set; }
    }
}