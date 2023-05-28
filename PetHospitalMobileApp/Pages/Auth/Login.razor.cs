using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using PetHospital.Business.Models.Request;

namespace PetHospitalMobileApp.Pages.Auth
{
    public partial class Login
    {
        public AuthenticateRequest LoginModel { get; set; } = new AuthenticateRequest()
        {
            userName = "stickmanuk225",
            password = "55214Artem#"
        };

        public async Task OnSubmitAsync()
        {
            try
            {
                await UserService.LoginAsync(LoginModel);
                NavigationManager.NavigateTo("/profile", true);
            }
            catch (Exception ex)
            {
                AddError(ex);
                await Toast.Make("Some error", ToastDuration.Long, 30).Show();
            }
        }
    }
}
