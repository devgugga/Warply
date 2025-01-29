using Warply.Communication.Request.User;
using Warply.Communication.Response.User;

namespace Warply.Application.UseCases.Update.User;

public interface IUpdateUserUseCase
{
    public Task<ResponseUpdateUserJson> Update(RequestUpdateUserJson request, Guid userId);
    public Task<ResponseUploadUserImageJson> Upload(byte[] imageBytes, string fileName, Guid userId);
}