using System.Collections.Generic;


namespace Zags.Domain.Repositories
{
    public interface IRetriever
    {

        IEnumerable<QueryResult> Search<QueryResult>(string procName, object parameters)
             where QueryResult : class;
    }
}
