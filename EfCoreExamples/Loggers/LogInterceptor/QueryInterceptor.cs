using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;

namespace EfCoreExamples.Loggers.LogInterceptor
{
    public class QueryInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            Debug.WriteLine(command.CommandText);

            return result;
        }
        public override InterceptionResult<int> NonQueryExecuting(
                                                DbCommand command,
                                                CommandEventData eventData,
                                                InterceptionResult<int> result)
        {
            Debug.WriteLine(command.CommandText);

            return result;
        }
    }
}
