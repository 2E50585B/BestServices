using System.Collections.Generic;

namespace BestServices.Model.DataBase
{
    /// <summary>
    /// Представляет собой услугу из списка услуг в БД
    /// </summary>
    public partial class Services
    {
        /// <summary>
        /// Содаёт новую коллекцию <see cref="DataBase.SelectedServices"/>, зависимых от <see cref="Services"/>
        /// </summary>
        public Services()
        {
            SelectedServices = new HashSet<SelectedServices>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    
        public virtual ICollection<SelectedServices> SelectedServices { get; set; }
    }
}