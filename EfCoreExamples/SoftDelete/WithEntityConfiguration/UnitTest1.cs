namespace EfCoreExamples.SoftDelete.WithEntityConfiguration
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
            using (var context = new ContextSoftDeleteWithEntityConfiguration())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SimplePerson entity = new SimplePerson { Name = "John", LastName = "Doe" };
                context.SimplePersons.Add(entity);
                context.SaveChanges();
                context.Remove(entity);
                context.SaveChanges();

                Assert.AreEqual(context.SimplePersons.Count(), 0);
            }

        }
    }
}