using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MusicSearchWPF.Messages;
using MusicSearchWPF.Models;
using MusicSearchWPF.Services;

namespace MusicSearchWPF.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        private readonly IMusicApiClient _musicApiClient;
        private readonly IMessenger _messenger;
        private readonly IFlyWeight _flyWeight;
        private ObservableCollection<Artist> artistInfoApiResponses;

        private Artist _selectedArtist;
        public Artist SelectedArtist
        {
            get => _selectedArtist;
            set => Set(ref _selectedArtist, value);
        }

        private string _artist;
        public string Artist
        {
            get => _artist;
            set
            {
                Set(ref _artist, value);
                SearchArtistCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Artist> ArtistInfoApiResponses
        {
            get => artistInfoApiResponses;
            set => Set(ref artistInfoApiResponses, value);
        }

        public HomeViewModel(IMusicApiClient _musicApiClient, IMessenger messenger, IFlyWeight flyWeight)
        {
            this._musicApiClient = _musicApiClient;
            _messenger = messenger;
            _flyWeight = flyWeight;
        }

        private RelayCommand _searchArtistCommand;

        public RelayCommand SearchArtistCommand => _searchArtistCommand ??= new RelayCommand(
            () =>
            {
                if (Artist != null)
                {
                    try
                    {
                        var artistSearchInfoInCash = _flyWeight.GetArtistSearchFromCash(Artist);
                        if (artistSearchInfoInCash != null)
                            ArtistInfoApiResponses = new ObservableCollection<Artist>(artistSearchInfoInCash);
                        else
                        {
                            var apiResponse = _musicApiClient.SearchArtist(Artist);
                            if (apiResponse != null)
                            {
                                ArtistInfoApiResponses = new ObservableCollection<Artist>(apiResponse.results.artistmatches.artist);
                                _flyWeight.SaveArtistToCash(Artist, apiResponse.results.artistmatches.artist);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                else
                    MessageBox.Show("Please enter an artist name");
            },
            () => !string.IsNullOrWhiteSpace(Artist)
        );

        private RelayCommand _detailsCommand;

        public RelayCommand DetailsCommand => _detailsCommand ??= new RelayCommand(
            () =>
            {
                if (SelectedArtist != null)
                {
                    _messenger.Send(new NavigationMessage { ViewModelType = typeof(DetailViewModel) });
                    _messenger.Send(new ArtistDetailMessage() { ArtistName = SelectedArtist.name });
                }
                else
                {
                    MessageBox.Show("Please choose the artist!");
                }
            }
        );
    }
}
