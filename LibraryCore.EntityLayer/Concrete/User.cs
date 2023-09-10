using LibraryCore.EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.EntityLayer.Concrete
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public int PositionId { get; set; }
        public Position ?Position { get; set; }
        public List<BorrowedBook> ?BorrowedBooks { get; set; }
        
    }
}
