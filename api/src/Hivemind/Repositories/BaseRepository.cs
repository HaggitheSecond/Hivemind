using System.Threading;
using Dapper;

namespace Hivemind.Repositories
{
    public abstract class BaseRepository
    {
        protected CommandDefinition AsCommand(string sql, CancellationToken token)
        {
            return new CommandDefinition(sql, cancellationToken: token);
        }

        protected CommandDefinition AsCommand(string sql, object param, CancellationToken token)
        {
            return new CommandDefinition(sql, parameters: param, cancellationToken: token);
        }
    }
}