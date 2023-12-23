using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Threading;
using BlossomiShymae.SkipNSlash.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace BlossomiShymae.SkipNSlash.Desktop.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        public List<string> _gameVersions = [];

        [ObservableProperty]
        private string _selectedVersion = string.Empty;

        [ObservableProperty]
        private string _search = string.Empty;

        [ObservableProperty]
        private List<string> _results = [];

        [ObservableProperty]
        private string _selectedResult = string.Empty;

        [ObservableProperty]
        private string _status = string.Empty;

        public MainWindowViewModel(IServiceProvider serviceProvider, HttpClient httpClient)
        {
            _serviceProvider = serviceProvider;
            _httpClient = httpClient;

            Dispatcher.UIThread.Post(async () =>
            {
                var versions = await Versions.GetAllAsync(httpClient);
                GameVersions = versions.ToList();
                SelectedVersion = "latest";
            });
        }

        [RelayCommand]
        private void SearchFiles()
        {
            if (string.IsNullOrWhiteSpace(SelectedVersion) || string.IsNullOrWhiteSpace(Search))
            {
                Results = [];
                Status = string.Empty;
                return;
            }
            
            Dispatcher.UIThread.Post(async () => 
            {
                var text = await FilesExported.GetAsync(SelectedVersion, _httpClient);
                Results = text.Search(Search);
                Status = $"{Results.Count} results";
            });
        }

        partial void OnSelectedResultChanged(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(SelectedVersion))
                return;

            var url = $"https://raw.communitydragon.org/{SelectedVersion}/{value}";
            // Open asset in default web browser
            Process.Start(new ProcessStartInfo(url)
            {
                UseShellExecute = true
            });
        }
    }
}