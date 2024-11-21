using Hw13.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Configuration
{
    public class UserBookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(b => b.User)  
                   .WithMany(u => u.Books)  
                   .HasForeignKey(b => b.UserId)  
                   .OnDelete(DeleteBehavior.SetNull); 
        }
    }
}
