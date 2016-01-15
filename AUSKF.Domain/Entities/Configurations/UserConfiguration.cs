namespace AUSKF.Domain.Entities.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;
    using Identity;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasMany(r => r.Roles);
            this.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            this.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);

            this.Property(u => u.UserName).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index",
               new IndexAnnotation(new IndexAttribute("UserNameIndex")
               {
                   IsUnique = true
               }));
            this.Property(u => u.Email).HasMaxLength(256);

            //this.Property(a => a.AuskfIdNumber)
            //    .IsRequired()
            //    .HasColumnAnnotation(
            //    IndexAnnotation.AnnotationName,
            //    new IndexAnnotation(new IndexAttribute("IX_FirstNameLastName", 1) { IsUnique = true }));
            //this.ToTable("Users");

        }
    }
}