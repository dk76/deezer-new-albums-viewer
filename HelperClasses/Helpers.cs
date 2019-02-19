using System;
using DeezerClasses;

namespace HelperClasses
{
    public class Config
    {

        public static bool isTest = false;

        public static string ClientId = !isTest?"xxxxxx": "xxxxxx";
        public static string ClientSecret =  !isTest?"xxxxxxx": "xxxxxxx";
        public static string DatabaseFileName =!isTest?"/home/user/data/mydb.db":"c:\\data\\mydb.db";
        public static string RedirectUri=!isTest?"https://xxxxxxxxx/Index/AfterAuth": "http://localhost:5000/Index/AfterAuth";
        public static string myUserId = "xxxxxxxxxx";

    }

   


}
