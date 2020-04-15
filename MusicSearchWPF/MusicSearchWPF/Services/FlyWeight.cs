using System;
using System.Collections.Generic;
using System.Text;
using MusicSearchWPF.Models;

namespace MusicSearchWPF.Services
{
    public class FlyWeight : IFlyWeight
    {
        Dictionary<string, IEnumerable<Artist>> artistSearch = new Dictionary<string, IEnumerable<Artist>>();

        public void SaveArtistToCash(string artistName, IEnumerable<Artist> apiResponse)
        {
            if (apiResponse != null)
              artistSearch.Add(artistName, apiResponse);
        }

        public IEnumerable<Artist> GetArtistSearchFromCash(string artistName)
        {
            if (artistSearch.ContainsKey(artistName))
                return artistSearch[artistName];
            return null;
        }
    }
}
