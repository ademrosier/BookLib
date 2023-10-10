using BookLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookLibTest
{
    [TestClass]
    public class UnitTest1
    {
        Book actor = new Book { Id = 1, Title = "Harry Potter", Price = 199.99 };

        Book title = new Book { Id = 2, Title = "Lord of the Rings", Price = 169.99 };
        Book titleNull = new Book { Id = 2, Title = null, Price = 169.99 };
        Book titleShort = new Book { Id = 2, Title = "LR", Price = 169.99 };

        Book price = new Book { Id = 3, Title = "Twilight", Price = 89.99 };
        Book priceHigh = new Book { Id = 3, Title = "Twilight", Price = 1201 };
        Book priceLow = new Book { Id = 3, Title = "Twilight", Price = -1 };

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("1 Harry Potter 199,99", actor.ToString());
        }

        [TestMethod]
        public void ValidateTitleTest()
        {
            title.ValidateTitleNull();
            title.ValidateTitleShort();
            Assert.ThrowsException<ArgumentNullException>(() => titleNull.ValidateTitleNull());
            Assert.ThrowsException<ArgumentException>(() => titleShort.ValidateTitleShort());
        }

        [TestMethod]
        public void ValidatePriceTest()
        {
            price.ValidatePrice();
            Assert.ThrowsException<ArgumentException>(() => priceHigh.ValidatePrice());
            Assert.ThrowsException<ArgumentException>(() => priceLow.ValidatePrice());
        }
    }
}