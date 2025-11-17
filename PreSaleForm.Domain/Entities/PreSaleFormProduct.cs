namespace PreSaleForm.Domain.Entities;

public class PreSaleFormProduct
{
    public Guid Id { get; set; }
    public Guid PreSaleFormId { get; set; }
    public PreSaleFormEntity PreSaleForm { get; set; } = default!;

    public string DoorModel { get; set; } = default!;
    public string DoorSurfaceType { get; set; } = default!;
    public decimal DoorLeafWidth { get; set; }
    public decimal DoorLeafHeight { get; set; }
    public decimal DoorFrameWidth { get; set; }
    public int DoorQuantity { get; set; }
    public bool IsWithGlass { get; set; }
    public string Color { get; set; } = default!;
    public decimal Amount { get; set; }
    public string? Note { get; set; }
}