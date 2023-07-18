using System;
using System.ComponentModel;
using System.Linq;

namespace EfCoreExamples.ChangeNotification.Explicit
{
    //HACK virtual properties are required for proxies with change notification
    public class SimplePersonWithNotificationTrigger : INotifyPropertyChanged, INotifyPropertyChanging
    {

        
        public virtual int Id
        {
            get => id;
            set
            {
                if (id == value)
                    return;
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        string lastName;
        string name;
        int id;

        public virtual string Name
        {
            get => name;
            set
            {
                if (name == value)
                    return;
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        
        public virtual string LastName
        {
            get => lastName;
            set
            {
                if (lastName == value)
                    return;
                lastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastName)));
            }
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
    }
}
