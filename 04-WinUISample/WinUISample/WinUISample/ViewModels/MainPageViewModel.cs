using System.Collections.Generic;
using System.Threading.Tasks;
using WinUISample.Models;
using WinUISample.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.IO;

namespace WinUISample.ViewModels
{
    public class MainPageViewModel: ObservableObject
    {
        private TsApiService _api;
        public MainPageViewModel()
        {
            _api = new TsApiService();
        }

        private SerieFollowersVM _highlighted;

        public SerieFollowersVM Highlighted
        {
            get { return _highlighted; }
            set { SetProperty(ref _highlighted, value); }
        }

        private List<SerieFollowersVM> _topSeries;

        public List<SerieFollowersVM> TopSeries
        {
            get { return _topSeries; }
            set { SetProperty(ref _topSeries, value); }
        }

        public async Task LoadData()
        {
            Highlighted = null;
            Highlighted = await _api.GetStatsSerieHighlighted();
            if (TopSeries == null)
            {
                TopSeries = await _api.GetStatsTopSeries();
            }
        }

        public async Task SaveData()
        {
            string file = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\file.txt";
            await File.WriteAllTextAsync(file, $"List of TV shows: {Environment.NewLine} {Environment.NewLine}");
            foreach (var item in TopSeries)
            {
                await File.AppendAllTextAsync(file, $"{item.Name} {Environment.NewLine}");

            }
        }

        public void ResetApp()
        {
            RegistryService registryService = new RegistryService();
            registryService.ResetApp();
        }
    }
}

