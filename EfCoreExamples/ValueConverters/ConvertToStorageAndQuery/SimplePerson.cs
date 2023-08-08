using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.ValueConverters.ConvertToStorageAndQuery
{


    public class SimplePerson
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Status Status { get; set; }
    }
    public enum Status
    {
        Available,
        Married,
        Divorced
    }

}
