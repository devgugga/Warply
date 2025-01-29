using SixLabors.ImageSharp;
using Warply.Communication.Request.User;
using Warply.Communication.Response.User;
using Warply.Domain;
using Warply.Domain.Repositories.Users;
using Warply.Domain.Services.Security;
using Warply.Domain.Services.Utils;
using Warply.Exception.Exceptions;

namespace Warply.Application.UseCases.Update.User;

internal class UpdateUserUseCase(
    IUsersRepository repository,
    IUnityOfWork unityOfWork,
    IPasswordHasher passwordHasher,
    ICloudflareClient cloudflareClient) : IUpdateUserUseCase
{
    public async Task<ResponseUpdateUserJson> Update(RequestUpdateUserJson request, Guid userId)
    {
        ValidateRequest(request);

        var user = await repository.GetByIdAsync(userId);

        if (user is null) throw new InvalidException("User does not exist.");

        if (user.Name != request.Name)
            user.Name = request.Name;
        else if (user.NickName != request.NickName)
            user.NickName = request.NickName;
        else if (user.Email != request.Email)
            user.Email = request.Email;

        if (!string.IsNullOrWhiteSpace(request.Password))
            if (!passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password))
                user.PasswordHash = passwordHasher.HashPassword(request.Password);

        await unityOfWork.Commit();

        var response = new ResponseUpdateUserJson
        {
            Name = user.Name,
            NickName = user.NickName,
            Email = user.Email
        };

        return response;
    }

    public async Task<ResponseUploadUserImageJson> Upload(byte[] imageBytes, string fileName, Guid userId)
    {
        var user = await repository.GetByIdAsync(userId);

        if (user is null) throw new InvalidException("User does not exist.");

        await using var webpStream = await ChangeImageFormat(imageBytes);

        var imageName = GenerateImageName(fileName);

        await cloudflareClient.UploadImage(webpStream, imageName, "image/webp");

        var imageUrl = $"https://gustavgomes.com.br/{imageName}";

        user.Avatar = imageUrl;

        await unityOfWork.Commit();

        var response = new ResponseUploadUserImageJson
        {
            ImageUrl = imageUrl
        };

        return response;
    }

    private static void ValidateRequest(RequestUpdateUserJson request)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        if (result.IsValid) return;

        var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }

    private static async Task<Stream> ChangeImageFormat(byte[] imageBytes)
    {
        using var inStream = new MemoryStream(imageBytes);
        using var image = await Image.LoadAsync(inStream);
        var outStream = new MemoryStream();

        await image.SaveAsWebpAsync(outStream);

        outStream.Position = 0;

        return outStream;
    }

    private static string GenerateImageName(string fileName)
    {
        var guid = Guid.NewGuid().ToString("N")[..16];

        var noSpaceFileName = fileName.Replace(" ", "-");

        var imageName = $"{noSpaceFileName}_{guid}";

        return imageName;
    }
}