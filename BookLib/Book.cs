namespace BookLib
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"{Id} {Title} {Price}";
        }

        public void ValidateTitleNull() 
        {
            if (Title == null)
            {
                throw new ArgumentNullException(nameof(Title), "The title cannot be null!");
            }
        }

        public void ValidateTitleShort()
        {
            if (Title.Length <= 2)
            {
                throw new ArgumentException("The title must be at least 3 characters!");
            }
        }

        public void ValidatePrice()
        {
            if(Price <0 || Price > 1200)
            {
                throw new ArgumentException("The price must be above 0 and below 1201");
            }
        }

        public virtual void Validate()
        {
            ValidateTitleNull();
            ValidateTitleShort();
            ValidatePrice();
        }
    }
}