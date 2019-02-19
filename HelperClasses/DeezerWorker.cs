using DeezerClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HelperClasses
{
    public class DeezerWorker
    {

        public static Albums GetArtistAlbums(string artist_id, int limit=0)
        {
            string url = Urls.GetArtistsAlbumsUrl(artist_id,limit);
            var albums = new Albums();
            albums.data = new List<Album>();
            string content = "";
            bool end = false;


            var errors = 0;
            while (!end)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = client.GetAsync(url).Result;
                        content = response.Content.ReadAsStringAsync().Result;
                        var l = JsonConvert.DeserializeObject<Albums>(content);
                        albums.data.AddRange(l.data);
                        end = ((limit==0) && (albums.data.Count ==  l.total)) || ( albums.data.Count<=limit) ;
                        url = l.next;

                    }
                    catch (Exception e)
                    {
                        var s = e.Message;
                        errors++;
                        Console.WriteLine(s);
                        Console.WriteLine($"Error count: {errors}");
                    }

                    //test
                    //end = true;

                }
            }


            return albums;
        }


        public static FavoriteArtists GetUserArtists(string user_id)
        {
            var url = Urls.GetUserArtistsUrl(user_id);
            var artists = new FavoriteArtists();
            artists.data = new List<Artist>();
            string content = "";
            //using (var client = new HttpClient())
            {

                bool end = false;


                while (!end)
                {
                    using (var client = new HttpClient())
                    {
                        var response = client.GetAsync(url).Result;
                        content = response.Content.ReadAsStringAsync().Result;
                        var l = JsonConvert.DeserializeObject<FavoriteArtists>(content);
                        artists.data.AddRange(l.data);
                        end = (artists.data.Count == l.total) || (l.next == null);
                        url = l.next;
                    }



                }


            }







            return artists;

        }
    }
}
