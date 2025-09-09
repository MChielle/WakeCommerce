using Catalog.Application.Abstractions.Handlers;
using Catalog.Application.Products.Create;
using Catalog.Application.Products.DeleteById;
using Catalog.Application.Products.GetByFilter;
using Catalog.Application.Products.GetByFilterOrdered;
using Catalog.Application.Products.GetById;
using Catalog.Web.Api.Extensions;
using Catalog.Web.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Shared.Defaults.Results;
using System.Net;

namespace Catalog.Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IQueryHandler<GetByIdProductQuery, GetByIdProductResponse> _getByIdQueryHandler;
        private readonly IQueryHandler<GetByFilterProductQuery, GetByFilterProductResponse> _getByFilterProductHandler;
        private readonly ICommandHandler<CreateProductCommand, Guid> _createCommandHandler;
        private readonly ICommandHandler<UpdateByIdProductCommand> _updateCommandHandler;
        private readonly ICommandHandler<DeleteByIdProductCommand> _deleteCommandHandler;

        public ProductsController(
            IQueryHandler<GetByIdProductQuery, GetByIdProductResponse> getByIdQueryHandler,
            ICommandHandler<CreateProductCommand, Guid> createCommandHandler,
            ICommandHandler<UpdateByIdProductCommand> updateCommandHandler,
            ICommandHandler<DeleteByIdProductCommand> deleteCommandHandler,
            IQueryHandler<GetByFilterProductQuery, GetByFilterProductResponse> getByFilterProductHandler)
        {
            _getByIdQueryHandler = getByIdQueryHandler;
            _createCommandHandler = createCommandHandler;
            _updateCommandHandler = updateCommandHandler;
            _deleteCommandHandler = deleteCommandHandler;
            _getByFilterProductHandler = getByFilterProductHandler;
        }

        [HttpGet("{id:guid}/get-by-id")]
        [ProducesResponseType(typeof(Result<GetByIdProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Results), (int)HttpStatusCode.NotFound)]
        public async Task<IResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetByIdProductQuery(id);
            var response = await _getByIdQueryHandler.Handle(request, cancellationToken);
            return response.Match(Results.Ok, CustomResults.Problem);
        }

        [HttpGet("get-by-filter")]
        [ProducesResponseType(typeof(Result<GetByFilterProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Results), (int)HttpStatusCode.NotFound)]
        public async Task<IResult> GetByfilter([FromQuery] GetByFilterProductQuery request, CancellationToken cancellationToken)
        {
            var response = await _getByFilterProductHandler.Handle(request, cancellationToken);
            return response.Match(Results.Ok, CustomResults.Problem);
        }

        [HttpPost("create-new-product")]
        [ProducesResponseType(typeof(Result<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IResult> Create([FromBody] CreateProductCommand createProduct, CancellationToken cancellationToken)
        {
            var response = await _createCommandHandler.Handle(createProduct, cancellationToken);
            return response.Match(Results.Ok, CustomResults.Problem);
        }

        [HttpPut("{id:guid}/update-by-id")]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NotFound)]
        public async Task<IResult> Update([FromRoute] Guid id, [FromBody] UpdateByIdProductCommand updateProduct, CancellationToken cancellationToken)
        {
            updateProduct.Id = id;
            var response = await _updateCommandHandler.Handle(updateProduct, cancellationToken);
            return response.Match(Results.NoContent, CustomResults.Problem);
        }

        [HttpDelete("{id:guid}/delete-by-id")]
        [ProducesResponseType(typeof(IResult), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(Results), (int)HttpStatusCode.NotFound)]
        public async Task<IResult> DeleteById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteByIdProductCommand(id);
            var response = await _deleteCommandHandler.Handle(request, cancellationToken);
            return response.Match(Results.NoContent, CustomResults.Problem);
        }
    }
}