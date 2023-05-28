using Microsoft.AspNetCore.Components;
using PetHospital.Business.Models.Response;
using PetHospitalMobileApp.Infrastructure;
using PetHospitalMobileApp.Services;

namespace PetHospitalMobileApp.Components.Common
{
    public class BaseComponent : ComponentBase, IDisposable, IAsyncDisposable
    {
        private readonly List<string> _errors = new List<string>();
        private readonly List<string> _messages = new List<string>();

        //[Inject] protected AnimalService AnimalService { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected UserService UserService { get; set; }
        [Inject] protected ConnectionOptions ConnectionOptions { get; set; }
        [Inject] protected LocalStorage LocalStorage { get; set; }
        [Inject] public TranslateService Translate { get; set; }


        public UserResponse CurrentUser => UserService?.CurrentUser ?? null;

        public List<string> Errors => _errors;
        public List<string> Messages => _messages;

        protected override void OnInitialized()
        {
            Translate.LanguageChanged += LanguageChanged;
        }

        public void AddError(Exception ex)
        {
            _messages.Clear();
            _errors.Clear();
            _errors.Add(ex.Message);
            StateHasChanged();
        }

        public void ClearError()
        {
            _errors.Clear();
            StateHasChanged();
        }

        public void AddMessage(string message)
        {
            _errors.Clear();
            _messages.Clear();
            _messages.Add(message);
            StateHasChanged();
        }

        public void ClearMessage()
        {
            _messages.Clear();
            StateHasChanged();
        }

        public virtual void Dispose()
        {
            Translate.LanguageChanged -= LanguageChanged;
            ClearError();
            ClearMessage();
        }

        public virtual ValueTask DisposeAsync() {
            Translate.LanguageChanged -= LanguageChanged;
            ClearError();
            ClearMessage();
            return ValueTask.CompletedTask;
        }

        private void LanguageChanged(object sender, EventArgs e)
        {
            StateHasChanged();
        }
    }
}
