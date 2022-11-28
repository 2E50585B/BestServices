namespace BestServices.Model.DataBase
{
    /// <summary>
    /// Представляет собой услугу <see cref="DataBase.Services"/>,
    /// которую выбрал пользователь <see cref="DataBase.Users"/>
    /// </summary>
    public partial class SelectedServices
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ServiceID { get; set; }
    
        public virtual Services Services { get; set; }
        public virtual Users Users { get; set; }
    }
}