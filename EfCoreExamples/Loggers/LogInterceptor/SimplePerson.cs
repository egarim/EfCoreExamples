using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExamples.Loggers.LogInterceptor
{


    public class SimplePerson
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
