using System;

using System.Data.SQLite;
using System.IO;
using HelperClasses;
using DeezerClasses;
using System.Collections.Generic;

namespace DatabaseClasses
{
    public class MainClass
    {


       string DatabaseFileName = "";

       public MainClass(string DatabaseFileName)
       {
            this.DatabaseFileName = DatabaseFileName;       
       }

        public void InsertUser(User user)
        {
            using (var m_dbConn = new SQLiteConnection("Data Source=" + DatabaseFileName + ";Version=3;"))
            {
                m_dbConn.Open();
                SQLiteCommand m_sqlCmd = new SQLiteCommand();
                m_sqlCmd.Connection = m_dbConn;
                m_sqlCmd.CommandText = $"Insert into Users(id,name) values({user.id},'{user.name}');";
                m_sqlCmd.ExecuteNonQuery();
            }
        }

        
        public void InsertArtists(List<Artist> artists, string user_id="0")
        {
            var sql = "BEGIN TRANSACTION;";
            sql +=$"delete from Artists where usr_id={user_id};";
            foreach(var a in artists)
            {
                sql +=$"insert into Artists(id,name,usr_id,link,picture_small) values('{a.id}','{a.name}',{user_id},'{a.link}','{a.picture_small}');";
                //if (user_id != "0")
                //    sql += $"insert into ArtistsUsersLinks(user_id,artist_id) values({user_id},'{a.id}');";
                

            }
            sql += "COMMIT;";



            using (var m_dbConn = new SQLiteConnection("Data Source=" + DatabaseFileName + ";Version=3;"))
            {
                m_dbConn.Open();
                SQLiteCommand m_sqlCmd = new SQLiteCommand();
                m_sqlCmd.Connection = m_dbConn;
                m_sqlCmd.CommandText = sql;
                m_sqlCmd.ExecuteNonQuery();
            }



        }


        public List<(Artist,Album)>  GetNewAlbums(string usr_id)
        {
            var l = new List<(Artist, Album)>();

            var sql = "select a.id,a.name,a.release_date,a.link,a.cover_small,ar.id,ar.name,ar.link,ar.picture_small from Albums a "+
                        "join Artists ar on ar.id = a.artist_id "+
                        $"where a.release_date >= date('now', '-30 days')  and ar.usr_id = {usr_id} "+
                        "order by a.release_date desc, a.name";

            using (var m_dbConn = new SQLiteConnection("Data Source=" + DatabaseFileName + ";Version=3;"))
            {
                m_dbConn.Open();
                SQLiteCommand m_sqlCmd = new SQLiteCommand(sql, m_dbConn);
                m_sqlCmd.Connection = m_dbConn;
                m_sqlCmd.CommandText = sql;



                var reader = m_sqlCmd.ExecuteReader();


                while (reader.Read())
                {
                    (Artist, Album) a;
                    a.Item1 = new Artist();
                    a.Item2 = new Album();

                    //Album
                    var album = a.Item2;
                    album.id = reader.GetString(0);
                    album.title = reader.GetString(1);
                    album.release_date = reader.GetString(2);
                    album.link = reader.GetString(3);
                    album.cover_small = reader.GetString(4);

                    //Artist
                    var artist = a.Item1;
                    artist.id = reader.GetString(5);
                    artist.name = reader.GetString(6);
                    artist.link = reader.GetString(7);
                    artist.picture_small = reader.GetString(8);
                    l.Add(a);
                }
            }
            return l;
        }


        public List<string> GetArtists()
        {
            var l = new List<string>();

            var sql = "select a.id from Artists a group by a.id";

            using (var m_dbConn = new SQLiteConnection("Data Source=" + DatabaseFileName + ";Version=3;"))
            {
                m_dbConn.Open();
                SQLiteCommand m_sqlCmd = new SQLiteCommand(sql,m_dbConn);
                m_sqlCmd.Connection = m_dbConn;
                m_sqlCmd.CommandText = sql;
                   


                    var reader = m_sqlCmd.ExecuteReader();


                    while (reader.Read())
                    {
                        l.Add(reader.GetString(0));



                    }

                    
                




            }



            return l;
        }


        public void InsertAlbums(List<Album> albums, string artist_id = "0")
        {
            var sql = "BEGIN TRANSACTION;";
            sql += $"delete from  Albums where artist_id={artist_id};";
            foreach (var a in albums)
            {
                sql += $"insert into Albums(id,name,release_date,artist_id,link,cover_small) values('{a.id}','{a.title.Replace("'","''")}','{a.release_date}','{artist_id}','{a.link}','{a.cover_small}');";
                //if (artist_id != "")
                  //  sql += $"insert into AlbumArtistsLinks(artist_id,album_id) values('{artist_id}','{a.id}');";


            }
            sql += "COMMIT;";

            using (var m_dbConn = new SQLiteConnection("Data Source=" + DatabaseFileName + ";Version=3;"))
            {
                m_dbConn.Open();
                SQLiteCommand m_sqlCmd = new SQLiteCommand();
                m_sqlCmd.Connection = m_dbConn;
                m_sqlCmd.CommandText = sql;
                m_sqlCmd.ExecuteNonQuery();
            }



        }



        public static void CreateDataBaseFile(string path)
       {
            if (!File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                MakeTables(path);
            }
       }

        static void MakeTables(string path)
        {

            using (var m_dbConn = new SQLiteConnection("Data Source=" + path + ";Version=3;"))
            {
                m_dbConn.Open();
                SQLiteCommand m_sqlCmd = new SQLiteCommand();
                m_sqlCmd.Connection = m_dbConn;
                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY, name TEXT)";
                m_sqlCmd.ExecuteNonQuery();

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Artists (id text PRIMARY KEY, name TEXT, usr_id INTEGER, link Text, picture_small TEXT)";
                m_sqlCmd.ExecuteNonQuery();

                //m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS ArtistsUsersLinks (user_id int, artist_id TEXT)";
                //m_sqlCmd.ExecuteNonQuery();

                m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Albums (id TEXT, name TEXT, release_date TEXT, artist_id TEXT, link Text, cover_small TEXT)";
                m_sqlCmd.ExecuteNonQuery();

                //m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS AlbumArtistsLinks (artist_id TEXT, album_id TEXT)";
                //m_sqlCmd.ExecuteNonQuery();



            }
        }




    }










    }

