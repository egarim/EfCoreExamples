using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;

namespace EfCoreExamples.Loggers.LogInterceptor
{
    public class QueryInterceptor : DbCommandInterceptor
    {
        //HACK this will log the select statements
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            Debug.WriteLine(command.CommandText);

            return result;
        }

        //HACK this will log the INSERT,UPDATE,DELETE and DDL statements
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
