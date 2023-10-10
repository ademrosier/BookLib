using BookLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookRepositoryLibTest
{
    [TestClass]
    public class BooksRepositoryDBTest
    {
        private static BooksDbContext? _dbContext;
        private static IBooksRepository _repo;
        private static DbContextOptionsBuilder<BooksDbContext> optionsBuilder;

        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            optionsBuilder = new DbContextOptionsBuilder<BooksDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookLib;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        [TestInitialize]
        public void TestSeup()
        {
            // opret forbindelse til en database, og sørg for at databasen er tom, så der kan tilføjes nye bøger.
            _dbContext = new BooksDbContext(optionsBuilder.Options);
            _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Books");
            _repo = new BooksRepositoryDB(_dbContext);
            _repo.Add(new Book() { Title = "Harry Potter", Price = 199.99 });
            _repo.Add(new Book() { Title = "Lord of the Rings", Price = 169.99 });
            _repo.Add(new Book() { Title = "Jurassic Park", Price = 129.99 });
            _repo.Add(new Book() { Title = "Twilight", Price = 89.99 });
            _repo.Add(new Book() { Title = "The Godfather", Price = 89.99 });
        }

        [TestMethod]
        public void AddBookTest()
        {
            // tilføjer en ny bog med dens nye værdier. 
            // får denne bog en ny id? 
            // matcher bogens title med "Title" variablen?
            // matcher bogens pris med "Price" variablen?
            // jeg skal nu sikre mig, at der er 6 bøger, da jeg lige har tilføjet en ny bog.
            Book snowWhite = _repo.Add(new Book { Title = "Snehvide", Price = 59.99 });

            Assert.IsTrue(snowWhite.Id >= 0);
            Assert.AreEqual("Snehvide", snowWhite.Title);
            Assert.AreEqual(59.99, snowWhite.Price);

            // Hent alle bøger fra repository
            IEnumerable<Book> allBooks = _repo.Get();
            Assert.AreEqual(6, allBooks.Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Book? book = _repo.Update(1, new Book { Title = "Harry Potter and the Deathly Hallows", Price = 299.99 });
            Assert.IsNotNull(book);
            Book? book2 = _repo.GetById(1);
            Assert.AreEqual("Harry Potter and the Deathly Hallows", book.Title);

            Assert.IsNull(_repo.Update(-1, new Book { Title = "Buh", Price = 299.99 }));
            Assert.ThrowsException<ArgumentException>(() => _repo.Update(1, new Book { Title = "", Price = 299.99 }));
        }
    }
}