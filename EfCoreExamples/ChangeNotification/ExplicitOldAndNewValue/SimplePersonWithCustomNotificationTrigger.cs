using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace EfCoreExamples.ChangeNotification.ExplicitOldAndNewValue
{
    //HACK virtual properties are required for proxies with change notification
    public class SimplePersonWithCustomNotificationTrigger : INotifyPropertyChanged, INotifyPropertyChanging
    {

        
        public  int Id
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

        public string Name
        {
            get => name;
            set => SetPropertyValue<string>(nameof(Name), ref name, value);
        }
        void OnChanged(string PropertyName, object OldValue, object NewValue)
        {
            
        }
        void SetPropertyValue<T>(string PropertyName,ref T OldValue,T NewValue)
        {
            if (EqualityComparer<T>.Default.Equals(OldValue, NewValue))
                return;
            OnChanged(PropertyName, OldValue, NewValue);
            OldValue = NewValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public string LastName
        {
            get => lastName;
            set => SetPropertyValue<string>(nameof(LastName), ref lastName, value);
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
    }
}
