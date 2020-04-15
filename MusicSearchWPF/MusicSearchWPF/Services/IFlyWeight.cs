using System.Collections.Generic;
using MusicSearchWPF.Models;

namespace MusicSearchWPF.Services
{
    interface IFlyWeight
    {
        void SaveArtistToCash(string artistName, IEnumerable<Artist> apiResponse);
        IEnumerable<Artist> GetArtistSearchFromCash(string artistName);
    }
}