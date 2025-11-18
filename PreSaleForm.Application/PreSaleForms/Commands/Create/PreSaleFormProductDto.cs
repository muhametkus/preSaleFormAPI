namespace PreSaleForm.Application.PreSaleForms.Commands.Create;

public class PreSaleFormProductDto
{
    public string DoorModel { get; set; }
    public string DoorSurfaceType { get; set; }
    public decimal DoorLeafWidth { get; set; }
    public decimal DoorLeafHeight { get; set; }
    public decimal DoorFrameWidth { get; set; }

    public int DoorQuantity { get; set; }
    public bool IsWithGlass { get; set; }
    public string Color { get; set; }
    public decimal Amount { get; set; }
}