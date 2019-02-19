using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelperClasses;
using System.Web;
using DeezerClasses;
using Newtonsoft.Json;
using DatabaseClasses;

namespace MainWebSite.Controllers
{
    public class IndexController : Controller
    {
       
        public IActionResult Index()
        {
            var url=DeezerClasses.Urls.GetUrlForAuth();


            return Redirect(url);
        }


        public IActionResult Main(Models.MainModel m)
        {

            DatabaseClasses.MainClass.CreateDataBaseFile(Config.DatabaseFileName);

            m.user = GetUser(m.access_token);
            

            //DatabaseClasses.MainClass conn= new MainClass(Config.DatabaseFileName);
            //conn.InsertUser(m.user);


           // var artists = GetUserArtists(m.user.id);
           // conn.InsertArtists(artists.data,m.user.id);

           /* var albums = new List<Album>();

            foreach( var a in artists.data)
            {
                var l = GetArtistAlbums(a.id);
                albums.AddRange(l.data);
                conn.InsertAlbums(l.data, a.id);
                //test
                //break;
            }*/

            


            return View("Main", m);

        }


      
        public string GetHistory(string user_id,string access_token,string index)
        {

            string url = Urls.GetHistory(user_id,access_token,index);
            string content = "";
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                content = response.Content.ReadAsStringAsync().Result;
            }


            //var user = JsonConvert.DeserializeObject<User>(content);
            return content;

        }
       


        

      

        private static User GetUser(string access_token)
        {
            string url = Urls.GetUrlUserMe(access_token);
            string content = "";
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                content = response.Content.ReadAsStringAsync().Result;
            }


            var user = JsonConvert.DeserializeObject<User>(content);
            return user;
        }

      

        public IActionResult AfterAuth(string code, string state)
        {
            string url = Urls.GetUrlForAccessToken(code);

            string content = "";
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                content = response.Content.ReadAsStringAsync().Result;

                content = HttpUtility.ParseQueryString(content).Get("access_token");

            }

            

            Models.MainModel m = new Models.MainModel();

            m.access_token = content;
            return Main(m);

        }

        public string UpdateArtists(string usr_id)
        {

            try
            {
                var f = DeezerWorker.GetUserArtists(usr_id);
                DatabaseClasses.MainClass conn = new MainClass(Config.DatabaseFileName);
                conn.InsertArtists(f.data, usr_id);


            }
            catch(Exception e)
            {
                return e.Message;
            }


            return "OK";
        }

        public string LoadNewAlbums(string usr_id)
        {

            try
            {
              
                DatabaseClasses.MainClass conn = new MainClass(Config.DatabaseFileName);
                var l = conn.GetNewAlbums(usr_id);




                return JsonConvert.SerializeObject(l);

            }
            catch 
            {
                return "ERROR";
            }                      

        }



    }
}