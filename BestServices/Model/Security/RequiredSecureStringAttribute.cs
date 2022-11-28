using System;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace BestServices.Model.Security
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true), Obsolete("", false)]
    internal class RequiredSecureStringAttribute : ValidationAttribute
    {
        public RequiredSecureStringAttribute() : base("Field is required") { }

        public override bool IsValid(object value) => (value as SecureString)?.Length > 0;
    }
}