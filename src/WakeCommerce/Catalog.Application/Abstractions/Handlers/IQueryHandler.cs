using Catalog.Application.Abstractions.Requests;
using Shared.Defaults.Results;

namespace Catalog.Application.Abstractions.Handlers;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}