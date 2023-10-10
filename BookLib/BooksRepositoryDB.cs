namespace BookLib
{
    public class BooksRepositoryDB : IBooksRepository
    {
        private readonly BooksDbContext _context;

        public BooksRepositoryDB(BooksDbContext dbContext)
        {
            _context = dbContext;
        }

        public Book Add(Book book)
        {
            book.Validate();
            book.Id = 0;
            _context.Books.Add(book);
            _context.SaveChanges(); 
            return book;
        }

        public IEnumerable<Book> Get(string? title = null, double? price = null, string? orderBy = null)
        {
            //List<Book> result = _context.Books.ToList();
            IQueryable<Book> query = _context.Books.AsQueryable();
            if (price != null)
            {
                query = query.Where(b => b.Price > price);
            }
            if (title != null)
            {
                query = query.Where(b => b.Title.Contains(title));
            }
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "title":
                    case "title_asc":
                        query = query.OrderBy(b => b.Title);
                        break;
                    case "title_desc":
                        query = query.OrderByDescending(b => b.Title);
                        break;
                    case "price":
                        query = query.OrderBy(b => b.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(b => b.Price);
                        break;
                    default:
                        break; 
                        // do nothing
                        //throw new ArgumentException("Unknown sort order: " + orderBy);
                }
            }
            return query;
        }

        public Book? GetById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public Book? Remove(int id)
        {
            Book? book = GetById(id);
            if (book is null)
            {
                return null;
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return book;
        }

        public Book? Update(int id, Book book)
        {
            book.Validate();
            Book? bookToUpdate = GetById(id);
            if (bookToUpdate == null) return null;
            bookToUpdate.Title = book.Title;
            bookToUpdate.Price = book.Price;
            _context.SaveChanges();
            return bookToUpdate;
        }
    }
}