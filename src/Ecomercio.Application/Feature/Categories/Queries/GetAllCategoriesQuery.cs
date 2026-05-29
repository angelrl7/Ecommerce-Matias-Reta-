using MediatR;
using Ecomercio.Application.Common.DTOs.Category;

namespace Ecomercio.Application.Feature.Categories.Queries;

public record GetAllCategoriesQuery() : IRequest<IReadOnlyList<CategoryDto>>;
