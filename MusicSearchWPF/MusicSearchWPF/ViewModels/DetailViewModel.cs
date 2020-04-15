using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
     class DetailViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IMusicApiClient _musicApiClient;
        
        public DetailViewModel(IMessenger messenger, IMusicApiClient musicApiClient)
        {
            _messenger = messenger;
            _musicApiClient = musicApiClient;
            _messenger.Register<ArtistDetailMessage>(this, message =>
            {
                var artistInfo = _musicApiClient.GetArtistInfo(message.ArtistName);
                if (artistInfo != null)
                {
                    Name = artistInfo.artist.name;
                    Summary = artistInfo.artist.bio.summary;
                    SimilarArtists = artistInfo.artist.similar.artist;
                }
            });
        }

        private string _name;
        private string _summary;
        private List<SimilarArtists> _similarArtists;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Summary
        {
            get => _summary;
            set => Set(ref _summary, value);
        }

        public List<SimilarArtists> SimilarArtists
        {
            get => _similarArtists;
            set => Set(ref _similarArtists, value);
        }

        private SimilarArtists _selectedArtist;
        public SimilarArtists SelectedArtist
        {
            get => _selectedArtist;
            set => Set(ref _selectedArtist, value);
        }

        private RelayCommand _similarArtistDetailsCommand;

        public RelayCommand SimilarArtistDetailsCommand => _similarArtistDetailsCommand ??= new RelayCommand(
            () =>
            {
                if (SelectedArtist != null)
                {
                    var artistInfo = _musicApiClient.GetArtistInfo(SelectedArtist.name);
                    if (artistInfo != null)
                    {
                        Name = artistInfo.artist.name;
                        Summary = artistInfo.artist.bio.summary;
                        SimilarArtists = artistInfo.artist.similar.artist;
                    }
                }
                else
                {
                    MessageBox.Show("Please choose the artist!");
                }
            }
        );
    }
}
