// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.7
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace Infraestructure.Context
{
    using Domain.EntityModel;
    using Infraestructure.EntityConfigurations;

    public partial class CustomDbContextFactory : System.Data.Entity.Infrastructure.IDbContextFactory<CustomDbContext>
    {
        public CustomDbContext Create()
        {
            return new CustomDbContext();
        }
    }
}
// </auto-generated>
