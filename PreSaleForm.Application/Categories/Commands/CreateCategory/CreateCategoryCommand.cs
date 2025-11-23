using MediatR;

namespace PreSaleForm.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<Guid>;
