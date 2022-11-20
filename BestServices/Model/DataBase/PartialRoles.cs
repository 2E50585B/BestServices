namespace BestServices.Model.DataBase
{
    public partial class Roles
    {
        public RoleType Role => (RoleType)ID;

        public enum RoleType
        {
            Менеджер_услуг = 1,
            Гость
        }
    }
}