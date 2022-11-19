using System;

namespace BestServices.Model.DataBase
{
    public partial class Roles
    {
        public RoleType Role
        {
            get
            {
                //if (Enum.TryParse(RoleName, out RoleType roleType))
                //{
                //    return roleType;
                //}
                //return (RoleType)Enum.GetValues(typeof(RoleType)).GetValue(ID);
                return (RoleType)ID;
            }
        }

        public enum RoleType
        {
            Менеджер_услуг = 1,
            Гость
        }
    }
}