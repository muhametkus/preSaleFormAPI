namespace PreSaleForm.Application.PreSaleForms.Commands.Create;

public class CreatePreSaleFormResponse
{
    public Guid Id { get; set; }
    public string CustomerFullName { get; set; } = default!;
    public string CustomerPhone { get; set; } = default!;
}