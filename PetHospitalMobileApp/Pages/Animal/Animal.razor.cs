

using ChartJs.Blazor.LineChart;
using ChartJs.Blazor;
using Microsoft.AspNetCore.Components;
using PetHospital.Business.Models.Response;
using PetHospitalMobileApp.Components.Common;
using PetHospitalMobileApp.Services;

namespace PetHospitalMobileApp.Pages.Animal
{
    public partial class Animal : BaseComponent
    {
        [Inject] protected AnimalService AnimalService { get; set; }

        private LineConfig _lineChartConfig;
        private Chart _lineChart;

        public List<AnimalResponse> Animals { get; set; } = new();

        public bool IsPetDataLoaded { get; private set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadAnimalList();
            await base.OnInitializedAsync();
        }

        private async Task LoadAnimalList()
        {
            Animals = await AnimalService.GetAnimalList();
            IsPetDataLoaded = true;
        }
    }
}
