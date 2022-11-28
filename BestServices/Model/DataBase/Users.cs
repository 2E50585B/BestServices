using System.Collections.Generic;

namespace BestServices.Model.DataBase
{
    /// <summary>
    /// Представляет собой пользователя из списка пользователей в БД
    /// </summary>
    public partial class Users
    {
        /// <summary>
        /// Создаёт новую коллекцию <see cref="DataBase.SelectedServices"/>, зависимых от <see cref="Users"/>
        /// </summary>
        public Users()
        {
            SelectedServices = new HashSet<SelectedServices>();
        }
    
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Patronymic { get; set; }
        public int? RoleID { get; set; }
    
        public virtual Roles Roles { get; set; }
        public virtual ICollection<SelectedServices> SelectedServices { get; set; }
    }
}