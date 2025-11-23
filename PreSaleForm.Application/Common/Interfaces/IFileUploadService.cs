namespace PreSaleForm.Application.Common.Interfaces;

public interface IFileUploadService
{
    Task<string> UploadImageAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
    Task<bool> DeleteImageAsync(string imageUrl, CancellationToken cancellationToken = default);
}
