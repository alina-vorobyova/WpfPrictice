using System;
using System.Collections.Generic;
using System.Text;
using MusicSearchWPF.Models;

namespace MusicSearchWPF.Services
{
    internal interface IMusicApiClient
    {
        ArtistSearchApiResponse SearchArtist(string artist);
        ArtistGetInfoApiResponse GetArtistInfo(string artistName);
    }

}
