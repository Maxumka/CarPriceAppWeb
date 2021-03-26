using CarPriceAppWeb.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPriceAppWeb.Shared
{
    public class AlertComponent : ComponentBase, IDisposable
    {
        [Inject]
        private AlertService AlertService { get; set; }

        protected string Message { get; set; }
        protected bool IsVisible { get; set; }
        protected string BackgroundCssClass { get; set; }
        protected string IconCssClass { get; set; }

        protected override void OnInitialized()
        {
            AlertService.OnShow += ShowAlert;
            AlertService.OnHide += HideAlert;
        }

        private async void ShowAlert(string message)
        {
            BuildAlert(message);
            IsVisible = true;
            await InvokeAsync(() => StateHasChanged());
        }

        private async void HideAlert()
        {
            IsVisible = false;
            await InvokeAsync(() => StateHasChanged());
        }

        private void BuildAlert(string message)
        {
            BackgroundCssClass = "bg-danger";
            IconCssClass = "exclamation";
            Message = message;
        }

        public void Dispose()
        {
            //GC.SuppressFinalize(this);
            AlertService.OnShow -= ShowAlert;
        }
    }
}
