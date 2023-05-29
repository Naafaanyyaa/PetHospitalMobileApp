using BlazorInputFile;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PetHospital.Business.Models.Request;
using PetHospitalMobileApp.Services;

namespace PetHospitalMobileApp.Pages.Animal
{
    public partial class AnimalAdd
    {
        [Inject] private AnimalService AnimalService { get; set; }

        private AnimalRequest _animalRequest;
        private Stream _photoStream;
        private string _photoFileName;
        private IFileListEntry _selectedFile;

        protected override void OnInitialized()
        {
            _animalRequest = new AnimalRequest
            {
                UserId = CurrentUser.Id
            };
        }


        private async Task HandleFileInput(InputFileChangeEventArgs e)
        {
            var files = e.GetMultipleFiles();

            if (files != null)
            {
                var file = files[0];
                _photoStream = file.OpenReadStream();
                _photoFileName = file.Name;
            }
        }


        public async Task OnSubmitAsync()
        {
            try
            {
                await AnimalService.AddAnimal(_animalRequest, _photoStream, _photoFileName);
                await Toast.Make("Animal added", ToastDuration.Long, 30).Show();
                NavigationManager.NavigateTo("/animal-list", true);
            }
            catch (Exception ex)
            {
                AddError(ex);
                await Toast.Make("Some error", ToastDuration.Long, 30).Show();
            }
        }

    }
}
