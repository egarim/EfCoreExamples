
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

        [Test]//Expected to fail, proxies will add event trigger and interface implementation
        public void TestWithProxies()
        {
            using (var context = new ContextWithImplicitChangeNotifications())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SimplePerson entity = context.CreateProxy<SimplePerson>();

                if (entity is INotifyPropertyChanged npc)
                {
                    Assert.IsTrue(typeof(INotifyPropertyChanged).IsAssignableFrom(npc.GetType()));
                  
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
        [Test]//Expected to fail,no proxies or event trigger will be added
        public void TestWithoutProxies()
        {
            using (var context = new ContextWithImplicitChangeNotifications())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SimplePerson entity = new SimplePerson();

                Assert.IsTrue(typeof(INotifyPropertyChanged).IsAssignableFrom(entity.GetType()));

                entity.Name = "John";
                entity.LastName = "Doe";



                entity.Name = "Peter"; // "Property Name has been changed

                context.SimplePersons.Add(entity);
                context.SaveChanges();
            }
           
        }
    }
}