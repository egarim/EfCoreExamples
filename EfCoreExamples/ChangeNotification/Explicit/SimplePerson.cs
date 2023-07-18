using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.ChangeNotification.Explicit
{
    //HACK virtual properties are required for proxies with change notification
    public class SimplePerson: INotifyPropertyChanged
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string LastName { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
