﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using AppStore.Domain.Users;

namespace AppStore.Infra.Data.Persistence.EF.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            base.HasKey(u => u.UserId);

            base.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_Users_Email", 1) { IsUnique = true }));

            base.HasMany(p => p.CreditCards)
                .WithRequired(i => i.User)
                .Map(m => m.MapKey("UserId"));
        }
    }
}