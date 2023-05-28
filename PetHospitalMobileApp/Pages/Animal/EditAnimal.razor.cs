

using ChartJs.Blazor.LineChart;
using ChartJs.Blazor;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.AspNetCore.Components;
using PetHospital.Business.Models.Request;
using PetHospitalMobileApp.Components.Common;
using PetHospitalMobileApp.Services;

namespace PetHospitalMobileApp.Pages.Animal;

public partial class EditAnimal
{

    [Parameter] public string AnimalId { get; set; }
    [Inject] private AnimalService AnimalService { get; set; }
    private AnimalUpdateRequest _animalUpdateRequest = new AnimalUpdateRequest();


    public async Task OnAnimalUpdate()
    {
        try
        {
            await AnimalService.UpdateAnimal(AnimalId, _animalUpdateRequest);
            await Toast.Make("Animal has been edited", ToastDuration.Long, 30).Show();
            NavigationManager.NavigateTo("/animal-list", true);
        }
        catch (Exception ex)
        {
            AddError(ex);
            await Toast.Make("Some error", ToastDuration.Long, 30).Show();

        }
    }
}



