using Newtonsoft.Json;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospitalMobileApp.Infrastructure;
using PetHospitalMobileApp.Services.Abstract;
using RestSharp;
using System.Net.Http.Headers;
using System.Text;

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

        public async Task<List<DiseaseResponse>> GetDiseaseList(string animalId)
        {
            var url = ApiUrl + "/api/Disease/GetDiseaseList/" + animalId;
            var resultRequest = new RestRequest(new Uri(url), Method.Get)
                .AddHeader("Authorization", "Bearer " + _localStorage[LocalStorageKeys.AuthToken]);
            var response = await RestClient.ExecuteAsync(resultRequest);
            Console.WriteLine(response.Content);
            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
            {
                throw new ApplicationException();
            }

            return JsonConvert.DeserializeObject<List<DiseaseResponse>>(response.Content);
        }

        public async Task<AnimalResponse> AddAnimal(AnimalRequest animalRequest, Stream photoStream, string photoFileName)
        {
            var url = ApiUrl + "/api/Animal/CreateAnimal";

            using (var httpClientHandler = new HttpClientHandler())
            {
                // Установка опции принятия всех сертификатов сервера
                httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                using (var httpClient = new HttpClient(httpClientHandler))
                {

                    string token = "Bearer " + _localStorage[LocalStorageKeys.AuthToken].ToString();
                    // Установка заголовка авторизации
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _localStorage[LocalStorageKeys.AuthToken].ToString());

                    // Создание объекта MultipartFormDataContent
                    using (var formData = new MultipartFormDataContent())
                    {
                        // Добавление полей AnimalRequest в форму
                        formData.Add(new StringContent(animalRequest.UserId), "UserId");
                        formData.Add(new StringContent(animalRequest.AnimalName), "AnimalName");
                        formData.Add(new StringContent(animalRequest.AnimalDescription), "AnimalDescription");
                        formData.Add(new StringContent(animalRequest.AnimalType.ToString()), "AnimalType");

                        // Добавление файла фотографии в форму
                        var fileContent = new StreamContent(photoStream);
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "file",
                            FileName = photoFileName
                        };
                        formData.Add(fileContent);

                        // Отправка запроса на сервер
                        var response = await httpClient.PostAsync(url, formData);
                        var responseContent = await response.Content.ReadAsStringAsync();

                        if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(responseContent))
                        {
                            throw new ApplicationException();
                        }

                        return JsonConvert.DeserializeObject<AnimalResponse>(responseContent);
                    }
                }
            }
        }


    }
}
