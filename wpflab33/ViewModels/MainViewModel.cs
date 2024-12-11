using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace wpflab33.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private static HttpClient _httpClient = new HttpClient();

        private ObservableCollection<ProductReport> _productReports;
        private string? _checkMessage;

        public ObservableCollection<ProductReport> ProductReports
        {
            get => _productReports;
            set
            {
                _productReports = value;
                OnPropertyChanged();
            }
        }

        public string? CheckMessage
        {
            get => _checkMessage;
            set
            {
                _checkMessage = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(string jwtToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
            Initialize();
        }

        private async void Initialize()
        {
            await LoadProductReport();
        }

        public async Task LoadProductReport()
        {
            using var response = await _httpClient.GetAsync("http://localhost:5055/api/entrance");
            if (!response.IsSuccessStatusCode)
            {
                CheckMessage = "Ошибка при загрузке данных";
                return;
            }

            var productReport = await response.Content.ReadFromJsonAsync<List<ProductReport>>();
            ProductReports = new ObservableCollection<ProductReport>(productReport ?? new List<ProductReport>());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProductReport
    {
        public string ProductName { get; set; }
        public int BarchSize { get; set; }
        public decimal BarchPrice { get; set; }
        public decimal TotalCost { get; set; }
    }
}

    


