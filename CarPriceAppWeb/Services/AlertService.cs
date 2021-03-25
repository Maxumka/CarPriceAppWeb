using System;
using System.Timers;

namespace CarPriceAppWeb.Services
{
    public sealed class AlertService : IDisposable
    {
        private Timer _countdown;

        public event Action<string> OnShow;

        public event Action OnHide;

        public void ShowAlert(string message)
        {
            OnShow?.Invoke(message);
            StartCountdown();
        }

        private void HideAlert(object source, ElapsedEventArgs args) => OnHide?.Invoke();

        private void SetCountdown()
        {
            if (_countdown is not null) return;

            _countdown = new(5000);
            _countdown.Elapsed += HideAlert;
            _countdown.AutoReset = false;
        }

        private void StartCountdown()
        {
            SetCountdown();

            if (_countdown.Enabled)
            {
                _countdown.Stop();
                _countdown.Start();
            }
            else
            {
                _countdown.Start();
            }
        }

        public void Dispose()
        {
            _countdown?.Dispose();
        }
    }
}
