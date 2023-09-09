using LibraryCore.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.DataAccessLayer.Concrete
{
    public class Context :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;initial catalog=LibraryCore; Integrated Security=true;");

        }


        //Entityler Ve Veritabanı Tablo İslemleri//
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<EntityLayer.Concrete.Type> Types { get; set; }
        public DbSet<Position> Positions { get; set; }



        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<CustomerAccountProcess>()
        //        .HasOne(x => x.SenderCustomer)
        //        .WithMany(y => y.CustomerSender)
        //        .HasForeignKey(z => z.SenderID)
        //        .OnDelete(DeleteBehavior.ClientSetNull);

        //    builder.Entity<CustomerAccountProcess>()
        //        .HasOne(x => x.ReceiverCustomer)
        //        .WithMany(y => y.CustomerReceiver)
        //        .HasForeignKey(z => z.ReceiverID)
        //        .OnDelete(DeleteBehavior.ClientSetNull);

        //    base.OnModelCreating(builder);
        //}



    }
}
