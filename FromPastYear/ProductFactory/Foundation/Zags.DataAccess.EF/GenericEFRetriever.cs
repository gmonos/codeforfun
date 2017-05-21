using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Zags.Domain.Repositories;

namespace Zags.DataAccess.EF
{

    public class GenericEFRetriever : IRetriever
    {
        protected DbContext Context { get; set; }


        public GenericEFRetriever(DbContext context)
        {
            Context = context;
        }

        public IEnumerable<QueryResult> Search<QueryResult>(string procName, object parameters) where QueryResult : class
        {
            throw new NotImplementedException();
        }
    }


}
