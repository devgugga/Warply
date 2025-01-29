namespace Warply.Domain.Services.Utils;

public interface ICloudflareClient
{
    Task UploadImage(Stream image, string imageName, string type);
}