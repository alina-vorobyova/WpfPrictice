using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using MusicSearchWPF.Models;

namespace MusicSearchWPF.Services
{
 
    class MusicApiClient : IMusicApiClient
    {
        private readonly WebClient _webClient;
        private readonly string apiKey = "968824ebe0ad689d37ad2ff01658523a";
        private readonly string url = "http://ws.audioscrobbler.com/2.0/";
        public MusicApiClient()
        {
            _webClient = new WebClient();
        }

        public ArtistSearchApiResponse SearchArtist(string artistName)
        {
            try
            {
                var json = _webClient.DownloadString($"{url}?method=artist.search&artist={artistName}&api_key={apiKey}&format=json");
                var artistsList = JsonSerializer.Deserialize<ArtistSearchApiResponse>(json);
                return artistsList;
            }
            catch (Exception e)
            {
                throw new Exception("Something get wrong! Please, try again!");
            }
        }

        public ArtistGetInfoApiResponse GetArtistInfo(string artistName)
        {
            try
            {
                var json = _webClient.DownloadString($"{url}?method=artist.getinfo&artist={artistName}&api_key={apiKey}&format=json");
                var artist = JsonSerializer.Deserialize<ArtistGetInfoApiResponse>(json);
                return artist;
            }
            catch (Exception e)
            {
                throw new Exception("Something get wrong! Please, try again!");
            }
        }

    }
}
