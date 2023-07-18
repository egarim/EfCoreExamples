
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace EfCoreExamples.ChangeNotification.Implicit
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
            using (var context = new ContextWithImplicitChangeNotifications())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SimplePerson entity = context.CreateProxy<SimplePerson>();

                if (entity is INotifyPropertyChanged npc)
                {
                    npc.PropertyChanged += (sender, e) =>
                    {
                        Debug.WriteLine($"Property {e.PropertyName} has been changed");
                    };
                }

                entity.Name = "John";
                entity.LastName = "Doe";
                

               
                entity.Name = "Peter"; // "Property Name has been changed

                context.SimplePersons.Add(entity);
                context.SaveChanges();
            }
            Assert.Pass();
        }
    }
}