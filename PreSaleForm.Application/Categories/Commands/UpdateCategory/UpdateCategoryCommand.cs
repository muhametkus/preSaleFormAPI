using MediatR;

namespace PreSaleForm.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest;
