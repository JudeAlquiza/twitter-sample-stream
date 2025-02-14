﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Data.Configurations
{
    public partial class TweetConfiguration : IEntityTypeConfiguration<Tweet>
    {
        public void Configure(EntityTypeBuilder<Tweet> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Tweets__3214EC0798234ABF");

            entity.HasIndex(e => e.TweetId, "UC_Tweets").IsUnique();

            entity.Property(e => e.Text)
            .IsRequired()
            .HasMaxLength(350);
            entity.Property(e => e.TweetId)
            .IsRequired()
            .HasMaxLength(50);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Tweet> entity);
    }
}
