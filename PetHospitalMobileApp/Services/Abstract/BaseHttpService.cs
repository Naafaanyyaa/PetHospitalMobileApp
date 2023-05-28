using PetHospitalMobileApp.Infrastructure;
using RestSharp;

namespace PetHospitalMobileApp.Services.Abstract;

public abstract class BaseHttpService
{
    protected readonly RestClient RestClient;
    protected readonly string ApiUrl;

    protected BaseHttpService(RestClient restClient, ConnectionOptions connectionOptions)
    {
        RestClient = restClient;
        ApiUrl = connectionOptions.BaseUrl;
    }
}