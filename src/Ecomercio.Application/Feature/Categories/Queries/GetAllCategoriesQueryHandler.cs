using MediatR;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Application.Common.DTOs.Category;

namespace Ecomercio.Application.Feature.Categories.Queries;

public class GetAllCategoriesQueryHandler(ICategoryRepository repository)
    : IRequestHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryDto>>
{
    public async Task<IReadOnlyList<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
    {
        var categories = await repository.GetAllAsync(ct);
        return categories.Select(c => new CategoryDto(c.Id, c.Name)).ToList();
    }
}
