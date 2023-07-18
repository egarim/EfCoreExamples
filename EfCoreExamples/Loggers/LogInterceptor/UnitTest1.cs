
namespace EfCoreExamples.Loggers.LogInterceptor
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
            using (var context = new ContextWithInterceptors())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.SimplePersons.Add(new SimplePerson { Name = "John", LastName = "Doe" });
                context.SaveChanges();
            }
            Assert.Pass();
        }
    }
}