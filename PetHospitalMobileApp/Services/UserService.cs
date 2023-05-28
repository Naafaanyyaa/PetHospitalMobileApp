using Newtonsoft.Json;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospitalMobileApp.Infrastructure;
using PetHospitalMobileApp.Services.Abstract;
using RestSharp;

namespace PetHospitalMobileApp.Services;

public class UserService : BaseHttpService {
    public UserResponse CurrentUser { get; private set; }
    public bool IsAuthenticated { get; private set; }

    private readonly LocalStorage _localStorage;

    public UserService(RestClient restClient, ConnectionOptions connectionOptions, LocalStorage localStorage) : base(restClient, connectionOptions) {
        _localStorage = localStorage;
    }

    public async Task LoginAsync(AuthenticateRequest requestBody) {
        var url = ApiUrl + "/api/Authentication/Login";
        var request = new RestRequest(new Uri(url), Method.Post);
        var json = JsonConvert.SerializeObject(requestBody);
        request.AddStringBody(json, "application/json");
        var response = await RestClient.ExecuteAsync(request);

        Console.WriteLine(response.Content);

        if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content)) {
            throw new ApplicationException();
        }

        var authResult = JsonConvert.DeserializeObject<AuthorizeResponse>(response.Content);
        _localStorage.AddOrUpdate(LocalStorageKeys.AuthToken, authResult.Token);
        var currentUser = await GetCurrentUserProfileAsync();
        _localStorage.AddOrUpdate(LocalStorageKeys.Profile, currentUser);
        CurrentUser = currentUser;
        IsAuthenticated = true;
    }

    public async Task<UserResponse> GetCurrentUserProfileAsync() {
        var url = ApiUrl + "/api/User/GetUserInfo";
        var request = new RestRequest(new Uri(url)).AddHeader("Authorization", "Bearer " + _localStorage[LocalStorageKeys.AuthToken]); ;
        var response = await RestClient.ExecuteAsync(request);

        Console.WriteLine(response.Content);

        if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content)) {
            throw new ApplicationException();
        }

        return JsonConvert.DeserializeObject<UserResponse>(response.Content);
    }

    public async Task<UserResponse> UpdateProfile(UserRequest request)
    {
        var url = ApiUrl + "/api/User/UpdateUserInfo";
        var resultRequest = new RestRequest(new Uri(url), Method.Put)
            .AddHeader("Authorization", "Bearer " + _localStorage[LocalStorageKeys.AuthToken])
            .AddJsonBody(request);

        var response = await RestClient.ExecuteAsync(resultRequest);
        Console.WriteLine(response.Content);
        
        if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
        {
            throw new ApplicationException();
        }

        var currentUser = await GetCurrentUserProfileAsync();
        _localStorage.AddOrUpdate(LocalStorageKeys.Profile, currentUser);
        CurrentUser = currentUser;

        return JsonConvert.DeserializeObject<UserResponse>(response.Content);
    }

    public async Task ChangePassword(ChangePasswordRequest request)
    {
        var url = ApiUrl + "/api/User/UpdatePassword";
        var resultRequest = new RestRequest(new Uri(url), Method.Patch)
            .AddHeader("Authorization", "Bearer " + _localStorage[LocalStorageKeys.AuthToken])
            .AddJsonBody(request);

        var response = await RestClient.ExecuteAsync(resultRequest);
        Console.WriteLine(response.Content);

        if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
        {
            throw new ApplicationException();
        }
    }

    public void Logout() {
        CurrentUser = null;
        IsAuthenticated = false;
        _localStorage.Remove(LocalStorageKeys.AuthToken);
        _localStorage.Remove(LocalStorageKeys.Profile);
    }
}