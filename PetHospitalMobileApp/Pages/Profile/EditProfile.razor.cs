using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using PetHospital.Business.Models.Request;
using Sentry;

namespace PetHospitalMobileApp.Pages.Profile
{
    public partial class EditProfile
    {
        private UserRequest _profileModel = new UserRequest();
        
        public async Task UpdateProfile()
        {
            try
            {
                await UserService.UpdateProfile(_profileModel);
                await Toast.Make("Profile has been edited", ToastDuration.Long, 30).Show();
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

