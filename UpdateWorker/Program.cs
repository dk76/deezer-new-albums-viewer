using System;
using HelperClasses;
using DatabaseClasses;


namespace UpdateWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            try
            {
                var conn = new MainClass(Config.DatabaseFileName);


                var f = DeezerWorker.GetUserArtists(HelperClasses.Config.myUserId);
                if(f!=null)
                    conn.InsertArtists(f.data, HelperClasses.Config.myUserId);




                var a = conn.GetArtists();

                foreach (var artist in a)
                {
                    var l=DeezerWorker.GetArtistAlbums(artist, 2);

                    if (l != null)
                        conn.InsertAlbums(l.data, artist);


                    foreach(var album in l?.data)
                    {
                        Console.WriteLine(album.title);

                    }

                }



                Console.WriteLine("End");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            

        }
    }
}
