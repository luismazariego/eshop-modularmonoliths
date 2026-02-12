using Shared.Contracts.CQRS;

namespace Catalog.Contracts.Products.Features.GetProductById;

public record GetProductByIdRequest(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(ProductDto Product);