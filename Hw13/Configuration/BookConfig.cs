using Hw13.Entities;
using Hw13.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Configuration
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NameBook)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Author)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.About)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.PublisherBook)
                   .HasMaxLength(100);

            builder.Property(x => x.YearOfPublication)
                   .IsRequired();

            builder.Property(x => x.Pages)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasDefaultValue(StatusBookEnum.NotBorrowed);

            builder.Property(x => x.BorrowedDate)
               .HasColumnType("datetime")
               .HasDefaultValue(null);

            builder.Property(x => x.EndBorrowedDate)
                   .HasColumnType("datetime")
                   .HasDefaultValue(null);

            builder.Property(x => x.UserId)
                   .HasDefaultValue(null);
        }
    }
}
