using MediatR;
using Ecomercio.Domain.Entities;
using Ecomercio.Domain.Interfaces;
using Ecomercio.Application.Common.DTOs.Category;

namespace Ecomercio.Application.Feature.Categories.Commands;

public class CreateCategoryCommandHandler(ICategoryRepository repository)
    : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken ct)
    {
        var category = Category.Create(request.Name);
        await repository.AddAsync(category, ct);
        return new CategoryDto(category.Id, category.Name);
    }
}
