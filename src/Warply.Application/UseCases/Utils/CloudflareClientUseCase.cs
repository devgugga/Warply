using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Warply.Domain.Services.Utils;
using Warply.Exception.Exceptions;

namespace Warply.Application.UseCases.Utils;

internal class CloudflareClientUseCase(string accountId, string accessKey, string accessSecret) : ICloudflareClient
{
    public async Task UploadImage(Stream image, string imageName, string type)
    {
        var s3Client = new AmazonS3Client(accessKey, accessSecret, new AmazonS3Config
        {
            ServiceURL = $"https://{accountId}.r2.cloudflarestorage.com"
        });

        var request = new PutObjectRequest
        {
            BucketName = "warply",
            Key = imageName,
            InputStream = image,
            ContentType = type,
            DisablePayloadSigning = true
        };

        var response = await s3Client.PutObjectAsync(request);

        if (response.HttpStatusCode != HttpStatusCode.OK && response.HttpStatusCode != HttpStatusCode.Accepted)
            throw new UploadException("Upload to Cloudflare R2 failed");
    }
}