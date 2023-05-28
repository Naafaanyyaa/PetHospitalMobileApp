using Newtonsoft.Json;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospitalMobileApp.Infrastructure;
using PetHospitalMobileApp.Services.Abstract;
using RestSharp;

namespace PetHospitalMobileApp.Services
{
    public class AnimalService : BaseHttpService
    {
        private readonly LocalStorage _localStorage;
        public AnimalService(RestClient restClient, ConnectionOptions connectionOptions, LocalStorage localStorage) : base(restClient, connectionOptions)
        {
            _localStorage = localStorage;
        }

        public async Task<List<AnimalResponse>> GetAnimalList()
        {
            var url = ApiUrl + "/api/Animal/GetAnimal";
            var resultRequest = new RestRequest(new Uri(url), Method.Get)
                .AddHeader("Authorization", "Bearer " + _localStorage[LocalStorageKeys.AuthToken]);

            var response = await RestClient.ExecuteAsync(resultRequest);
            Console.WriteLine(response.Content);

            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
            {
                throw new ApplicationException();
            }

            return JsonConvert.DeserializeObject<List<AnimalResponse>>(response.Content);
        }

        public async Task<AnimalResponse> UpdateAnimal(string animalId, AnimalUpdateRequest animalUpdateRequest)
        {
            var url = ApiUrl + "/api/Animal/UpdateById/" + animalId;
            var resultRequest = new RestRequest(new Uri(url), Method.Put)
                .AddHeader("Authorization", "Bearer " + _localStorage[LocalStorageKeys.AuthToken])
                .AddJsonBody(animalUpdateRequest);
            var response = await RestClient.ExecuteAsync(resultRequest);
            Console.WriteLine(response.Content);

            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
            {
                throw new ApplicationException();
            }

            return JsonConvert.DeserializeObject<AnimalResponse>(response.Content);
        }
    }
}
