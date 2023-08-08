
using Microsoft.EntityFrameworkCore;

namespace EfCoreExamples.ValueConverters.ConvertToStorageAndQuery
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            using (var context = new ContextWithConverter())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.SimplePersons.Add(new SimplePerson { Name = "John", LastName = "Doe",Status= Status.Available });
                

                context.SaveChanges();

            

                using (var contextRead = new ContextWithConverter())
                {
                    var availableBooks = contextRead.SimplePersons
                                                .Where(b => b.Status == Status.Available)
                                                .ToList();
                }


            }
            Assert.Pass();
        }
    }
}