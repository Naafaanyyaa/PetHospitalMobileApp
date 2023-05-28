namespace PetHospitalMobileApp.Infrastructure;

public class ConnectionOptions
{
    public string BaseUrl { get; set; }

    public ConnectionOptions(string baseUrl)
    {
        BaseUrl = baseUrl;
    }
}