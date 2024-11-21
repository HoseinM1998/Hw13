using Hw13.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw13.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string NameBook { get; set; }
        public string Author { get; set; }
        public string About { get; set; }
        public string PublisherBook { get; set; }
        public int YearOfPublication { get; set; }
        public int Pages { get; set; }
        public StatusBookEnum Status { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public DateTime? EndBorrowedDate { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
