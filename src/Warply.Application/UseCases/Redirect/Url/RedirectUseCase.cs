using System.Net.Http.Json;
using Warply.Communication.Response.Url;
using Warply.Domain;
using Warply.Domain.Repositories.Url;
using Warply.Exception.Exceptions;

namespace Warply.Application.UseCases.Redirect.Url;

internal class RedirectUseCase(IUrlRepository repository, IUnityOfWork unityOfWork, HttpClient httpClient)
    : IRedirectUseCase
{
    public async Task<ResponseRedirectUrlJson> Redirect(string shortCode, string ip)
    {
        var url = await VerifyShortCode(shortCode);

        if (url.UrlStatus is not 0)
            throw new InvalidException("This shorted url is no longer avaliable.");

        var originalUrl = url.OriginalUrl;

        var country = await GetCountryFromIp(ip);

        if (country != null) url.UsersLocations.Add(country);

        url.LinkAccess += 1;

        await unityOfWork.Commit();

        var response = new ResponseRedirectUrlJson
        {
            OriginalUrl = originalUrl
        };

        return response;
    }


    private async Task<Domain.Entities.Url> VerifyShortCode(string shortCode)
    {
        var url = await repository.GetUrlByShortCode(shortCode);
        if (url is null)
            throw new InvalidException("Url doesn't exist.");

        return url;
    }

    private async Task<string?> GetCountryFromIp(string ip)
    {
        try
        {
            var response =
                await httpClient.GetFromJsonAsync<UrlIpApiResponse>(
                    $"http://ip-api.com/json/{ip}?fields=country,countryCode");
            Console.WriteLine(response);
            return response?.country;
        }
        catch (System.Exception ex)
        {
            throw new System.Exception(ex.Message);
        }
    }
}