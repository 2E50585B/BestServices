namespace BestServices.Model.DataBase
{
    /// <summary>
    /// Часть объкта <see cref="Roles"/>, ответственная за выборку <see cref="RoleType"/> по идентификатору должности
    /// </summary>
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