using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using PetHospital.Business.Models.Request;
using Microsoft.Maui.Controls;
using Sentry;


namespace PetHospitalMobileApp.Pages.Profile
{
    public partial class ChangePassword
    {
        private ChangePasswordRequest _changePasswordModel = new ChangePasswordRequest();

        public async Task ChangePasswordAsync()
        {
            try
            {
                SentrySdk.CaptureMessage("Hello Sentry");
                await UserService.ChangePassword(_changePasswordModel);
                await Toast.Make("Password has been changed", ToastDuration.Long, 30).Show();
                
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
