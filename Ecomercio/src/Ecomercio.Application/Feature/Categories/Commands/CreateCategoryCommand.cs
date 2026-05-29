using MediatR;
using Ecomercio.Application.Common.DTOs.Category;

namespace Ecomercio.Application.Feature.Categories.Commands;

public record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;
