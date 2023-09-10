using LibraryCore.EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.EntityLayer.Concrete
{
    public class Book : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
    }
}
