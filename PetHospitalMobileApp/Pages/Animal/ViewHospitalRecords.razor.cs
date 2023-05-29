using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.AspNetCore.Components;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospitalMobileApp.Components.Common;
using PetHospitalMobileApp.Services;

namespace PetHospitalMobileApp.Pages.Animal;
public partial class ViewHospitalRecords : BaseComponent
{
    [Parameter] public string AnimalId { get; set; }
    [Inject] private AnimalService AnimalService { get; set; }
    private AnimalUpdateRequest _animalUpdateRequest = new AnimalUpdateRequest();

    public List<DiseaseResponse> Diseases { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadDiseaseList(AnimalId);
        await base.OnInitializedAsync();
    }

    private async Task LoadDiseaseList(string animalId)
    {
        try
        {
            Diseases = await AnimalService.GetDiseaseList(animalId);
        }
        catch (Exception ex)
        {
            AddError(ex);
            await Toast.Make("Some error", ToastDuration.Long, 30).Show();
        }
    }

}

