using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetlistMaker.Services.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetlistMaker.Services.Domain.Sqlite.Config
{
    public class BandConfig : BaseEntityConfig<Band>, IEntityConfig
    {
        public override void Configure(EntityTypeBuilder<Band> builder)
        {
            builder.ToTable("Band");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                 .HasMaxLength(90)
                 .IsRequired();
        }
    }
}
