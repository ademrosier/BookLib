namespace BookLib
{
    public class BooksRepository
    {
        private int nextId = 1;
        private readonly List<Book> listOfBooks = new();

        public BooksRepository()
        {
            listOfBooks.Add(new Book() { Id = nextId++, Title = "Harry Potter", Price = 199.99 });
            listOfBooks.Add(new Book() { Id = nextId++, Title = "Lord of the Rings", Price = 169.99 });
            listOfBooks.Add(new Book() { Id = nextId++, Title = "Jurassic Park", Price = 129.99 });
            listOfBooks.Add(new Book() { Id = nextId++, Title = "Twilight", Price = 89.99 });
            listOfBooks.Add(new Book() { Id = nextId++, Title = "The Godfather", Price = 89.99 });
        }

        public IEnumerable<Book> Get(string? title = null, double? price = null, string? orderBy = null)
        {
            //opretter en ny kopi og gemmer den i result
            IEnumerable<Book> result = new List<Book>(listOfBooks);

            //Filtering
            if (title != null)
            {
                result = result.Where(book => book.Title.Contains(title));
            }
            if (price != null)
            {
                result = result.Where(book => book.Price >= price);
            }

            //Ordering
            if (orderBy != null)
            {
                //hvis orderBy er ikke-null, så konverter teksterne til små bogstaver
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    //sorter bøgerne i stigende (A-Z)
                    case "title":
                    case "title_asc":
                        result = result.OrderBy(book => book.Title);
                        break;
                    //sorter bøgerne i faldende (Z-A)
                    case "title_desc":
                        result = result.OrderByDescending(book => book.Title);
                        break;
                    case "price":
                    case "price_asc":
                        result = result.OrderBy(book => book.Price);
                        break;
                    case "price_desc":
                        result = result.OrderByDescending(book => book.Price);
                        break;
                    default:
                        return result;
                }
            }
            return result;
        }

        public Book? GetById(int id)
        {
            return listOfBooks.Find(book => book.Id == id);
        }

        public Book Add(Book book)
        {
            book.ValidateTitleNull();
            book.ValidateTitleShort();
            book.ValidatePrice();

            book.Id = nextId++;
            listOfBooks.Add(book);
            return book;
        }

        public Book? Remove(int id)
        {
            Book? book = GetById(id);
            if (book == null)
            {
                return null;
            }
            listOfBooks.Remove(book);
            return book;
        }

        public Book? Update(int id, Book book)
        {
            book.ValidateTitleNull();
            book.ValidateTitleShort();
            book.ValidatePrice();

            Book? existingBook = GetById(id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = book.Title;
            existingBook.Price = book.Price;
            return existingBook;
        }
    }
}
