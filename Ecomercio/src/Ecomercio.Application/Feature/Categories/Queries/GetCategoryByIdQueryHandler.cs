using MediatR;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Exceptions;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Application.Common.DTOs.Category;

namespace Ecomercio.Application.Feature.Categories.Queries;

public class GetCategoryByIdQueryHandler(ICategoryRepository repository)
    : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken ct)
    {
        var category = await repository.GetByIdAsync(request.Id, ct);

        if (category is null)
            throw new NotFoundException(nameof(Category), request.Id);

        return new CategoryDto(category.Id, category.Name);
    }
}
