
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace EfCoreExamples.ChangeNotification.Attach
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
            int PersonId;
            using (var context = new ContextAttachEntities())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                //HACK proxy documentation https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#change-tracking-proxies
                SimplePerson entity = context.CreateProxy<SimplePerson>();
                entity.Name = "John";
                entity.LastName = "Doe";
                

               
                entity.Name = "Peter"; // "Property Name has been changed

                context.SimplePersons.Add(entity);
                context.SaveChanges();

                PersonId = entity.Id;

                
            }
            using (var context = new ContextAttachEntities())
            {
              
                SimplePerson simplePersonAttached = context.CreateProxy<SimplePerson>(
                p =>
                {
                    p.Id = PersonId;
                });
                //the object will not be reloaded from the database, you can see there is no query in the console
                context.Attach(simplePersonAttached);
                context.Remove(simplePersonAttached);
                context.SaveChanges();
                
                Assert.AreEqual(context.SimplePersons.Count(),0);
            }
          
        }
        [Test]//Expected to fail,no proxies or event trigger will be added
        public void TestWithoutProxies()
        {
            using (var context = new ContextAttachEntities())
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