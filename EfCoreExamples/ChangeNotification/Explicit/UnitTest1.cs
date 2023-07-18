
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace EfCoreExamples.ChangeNotification.Explicit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]//Expected to fail, proxies will not add event trigger
        public void TestWithProxiesAndExplicitImplementationWithoutEventTrigger()
        {
            bool DidTriggerChangeNotification = false;
            using (var context = new ContextWithExplicitChangeNotifications())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SimplePerson entity = context.CreateProxy<SimplePerson>();

                if (entity is INotifyPropertyChanged npc)
                {
                    Assert.IsTrue(typeof(INotifyPropertyChanged).IsAssignableFrom(npc.GetType()));
                   
                    //this will not trigger because the event is not raised by the proxy
                    npc.PropertyChanged += (sender, e) =>
                    {
                        DidTriggerChangeNotification = true;
                        Debug.WriteLine($"Property {e.PropertyName} has been changed");
                    };
                }

                entity.Name = "John";
                entity.LastName = "Doe";
                

               
                entity.Name = "Peter"; // "Property Name has been changed

                context.SimplePersons.Add(entity);
                context.SaveChanges();
            }
            Assert.IsTrue(DidTriggerChangeNotification);
        }
        [Test]//Expected to pass
        public void TestWithExplicitImplementationAndEventTrigger()
        {
            bool DidTriggerChangeNotification = false;
            using (var context = new ContextWithExplicitChangeNotifications())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SimplePersonWithNotificationTrigger entity = new SimplePersonWithNotificationTrigger();

                if (entity is INotifyPropertyChanged npc)
                {
                    Assert.IsTrue(typeof(INotifyPropertyChanged).IsAssignableFrom(npc.GetType()));

                    //this will not trigger because the event is not raised by the proxy
                    npc.PropertyChanged += (sender, e) =>
                    {
                        DidTriggerChangeNotification = true;
                        Debug.WriteLine($"Property {e.PropertyName} has been changed");
                    };
                }

                entity.Name = "John";
                entity.LastName = "Doe";



                entity.Name = "Peter"; // "Property Name has been changed

                context.SimplePersonsWithNotificationTrigger.Add(entity);
                context.SaveChanges();
            }
            Assert.IsTrue(DidTriggerChangeNotification);
        }
    }
}