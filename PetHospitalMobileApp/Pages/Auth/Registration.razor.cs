using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using PetHospital.Business.Models.Request;

namespace PetHospitalMobileApp.Pages.Auth;
public partial class Registration
{
    public RegistrationRequest RegistrationModel { get; set; } = new RegistrationRequest() { };

    public async Task OnSubmitAsync()
    {
        try
        {
            await UserService.Registration(RegistrationModel);
            NavigationManager.NavigateTo("/login", true);
        }
        catch (Exception ex)
        {
            AddError(ex);
            await Toast.Make("Some error", ToastDuration.Long, 30).Show();
        }
    }
}

