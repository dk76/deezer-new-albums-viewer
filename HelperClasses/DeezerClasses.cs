using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace DeezerClasses
{
    public class Urls
    {
        public static string GetArtistsAlbumsUrl(string artist_id, int limit=0)
        {
            return $"https://api.deezer.com/artist/{artist_id}/albums"+(limit>0?$"?limit={limit}":"");
        }

        public static string GetUserArtistsUrl(string user_id)
        {
            return $"https://api.deezer.com/user/{user_id}/artists?index=0";
        }

        public static string GetUrlUserMe(string access_token)
        {
            return $"https://api.deezer.com/user/me?access_token={access_token}";
        }

        public static string GetUrlForAuth()
        {
            return $"https://connect.deezer.com/oauth/auth.php?app_id={HelperClasses.Config.ClientId}&redirect_uri={HelperClasses.Config.RedirectUri}&perms=basic_access,listening_history ";

        }

        public static string GetUrlForAccessToken(string code)
        {
            return $"https://connect.deezer.com/oauth/access_token.php?app_id={HelperClasses.Config.ClientId}&secret={HelperClasses.Config.ClientSecret}&code={code}";
        }

        public static string GetHistory(string user_id,string access_token,string index="0")
        {
            return $"https://api.deezer.com/user/{user_id}/history?access_token={access_token}" +(index!="0"?$"&index={index}":"");
        }


    }



    public class User
    {

        /// <summary>
        /// 
        /// </summary>
        public Int64 id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string firstname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inscription_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture_small { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture_medium { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture_big { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture_xl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lang { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string is_kid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tracklist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
    }


    
    public class Artist
    {

        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture_small { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture_medium { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture_big { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture_xl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int nb_album { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int nb_fan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string radio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tracklist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int time_add { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
    }

    

    public class FavoriteArtists
    {

        /// <summary>
        /// 
        /// </summary>
        public List<Artist> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string checksum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string next { get; set; }
    }



    public class Album
    {

        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover_small { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover_medium { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover_big { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cover_xl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int genre_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int fans { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string release_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string record_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tracklist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string explicit_lyrics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
    }

    public class Albums
    {

        /// <summary>
        /// 
        /// </summary>
        public List<Album> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string next { get; set; }
    }

}
