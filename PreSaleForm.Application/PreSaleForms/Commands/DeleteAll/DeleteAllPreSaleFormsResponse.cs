namespace PreSaleForm.Application.PreSaleForms.Commands.DeleteAll;

public class DeleteAllPreSaleFormsResponse
{
    public bool Success { get; set; }
    public int DeletedCount { get; set; }
    public string Message { get; set; } = default!;
}

