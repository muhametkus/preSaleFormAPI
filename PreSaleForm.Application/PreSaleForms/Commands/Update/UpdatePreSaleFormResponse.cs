namespace PreSaleForm.Application.PreSaleForms.Commands.Update;

public class UpdatePreSaleFormResponse
{
    public Guid Id { get; set; }
    public string CustomerFullName { get; set; } = default!;
    public string CustomerPhone { get; set; } = default!;
}

