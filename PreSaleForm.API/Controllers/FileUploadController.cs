using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreSaleForm.Application.Common.Interfaces;

namespace PreSaleForm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FileUploadController : ControllerBase
{
    private readonly IFileUploadService _fileUploadService;

    public FileUploadController(IFileUploadService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    /// <summary>
    /// Upload an image file
    /// </summary>
    /// <param name="file">Image file to upload (max 5MB, allowed: jpg, jpeg, png, gif, webp)</param>
    /// <param name="cancellationToken"></param>
    /// <returns>URL of the uploaded image</returns>
    [HttpPost("image")]
    [ProducesResponseType(typeof(UploadImageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UploadImageResponse>> UploadImage(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "No file uploaded" });
            }

            using var stream = file.OpenReadStream();
            var imageUrl = await _fileUploadService.UploadImageAsync(stream, file.FileName, cancellationToken);
            return Ok(new UploadImageResponse { ImageUrl = imageUrl });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Delete an uploaded image
    /// </summary>
    /// <param name="imageUrl">URL of the image to delete</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteImage(
        [FromQuery] string imageUrl,
        CancellationToken cancellationToken)
    {
        var result = await _fileUploadService.DeleteImageAsync(imageUrl, cancellationToken);
        
        if (!result)
        {
            return NotFound(new { error = "Image not found or could not be deleted" });
        }

        return NoContent();
    }
}

public class UploadImageResponse
{
    public string ImageUrl { get; set; } = default!;
}
