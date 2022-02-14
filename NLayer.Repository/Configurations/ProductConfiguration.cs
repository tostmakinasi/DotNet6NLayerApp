﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(75);
            builder.Property(x => x.Stock).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

            //builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            //builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);
            //builder.Property(x => x.UpdatedDate).HasDefaultValue(DateTime.Now);
        }
    }
}
