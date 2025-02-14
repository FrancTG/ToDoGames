using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Windows;
using ToDoGames.Model;

namespace ToDoGames.Core
{
    public static class DBConnection
    {
        private static SqliteConnection sqlite_conn;

        public static void StartDB()
        {
            sqlite_conn = new SqliteConnection("Data Source=tdgames.db;");
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            sqlite_conn.Open();
            CreateTablesIfNotExist();
        }

        public static void CreateTablesIfNotExist()
        {
            string tableName = "";

            SqliteCommand sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "SELECT name FROM sqlite_master WHERE type='table' and name='PlayedGames';";
            SqliteDataReader rdr = sqlcomm.ExecuteReader();

            while (rdr.Read())
            {
                tableName = rdr.GetString(0);
            }

            if (tableName == "")
            {
                sqlcomm = sqlite_conn.CreateCommand();
                sqlcomm.CommandText = "CREATE TABLE PlayedGames (id INTEGER PRIMARY KEY, name VARCHAR(300), released VARCHAR(50), background_image VARCHAR(300), added_date VARCHAR(12),score_graphics INTEGER, score_story INTEGER, score_gameplay INTEGER, score_bugs INTEGER, score_length INTEGER, score_performance INTEGER, score_music INTEGER, score_overall INTEGER, score_additional VARCHAR(300))";
                sqlcomm.ExecuteNonQuery();
            }

            tableName = "";

            sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "SELECT name FROM sqlite_master WHERE type='table' and name='GamesToPlay';";
            rdr = sqlcomm.ExecuteReader();

            while (rdr.Read())
            {
                tableName = rdr.GetString(0);
            }

            if (tableName == "")
            {
                sqlcomm = sqlite_conn.CreateCommand();
                sqlcomm.CommandText = "CREATE TABLE GamesToPlay (id INTEGER PRIMARY KEY, name VARCHAR(300), released VARCHAR(50), background_image VARCHAR(300), added_date VARCHAR(12), score_graphics INTEGER, score_story INTEGER, score_gameplay INTEGER, score_bugs INTEGER, score_length INTEGER, score_performance INTEGER, score_music INTEGER, score_overall INTEGER, score_additional VARCHAR(300))";
                sqlcomm.ExecuteNonQuery();
            }
        }

        public static void AddPlayedGame(PlayedGame pGame)
        {
            if (pGame.Id != 0)
            {
                try
                {
                    SqliteCommand sqlcomm = sqlite_conn.CreateCommand();
                    sqlcomm.CommandText = "INSERT INTO PlayedGames VALUES ( " + pGame.Id + ", \"" + pGame.Name + "\", \"" + pGame.Released + "\", \"" + pGame.BackgroundImage + "\", \"" + pGame.AddedDate + "\", " + pGame.Score.Graphics + ", " + pGame.Score.Story + ", " + pGame.Score.Gameplay + ", " + pGame.Score.Bugs + ", " + pGame.Score.Length + ", " + pGame.Score.Performance + ", " + pGame.Score.Music + ", " + pGame.Score.Overall + ", \"" + pGame.Score.AdditionalComment + "\")";
                    sqlcomm.ExecuteNonQuery();
                }
                catch (SqliteException)
                {
                    MessageBox.Show("Ya añadido.", "Info.");
                }
            }
        }

        public static List<PlayedGame> GetPlayedGames()
        {
            List<PlayedGame> games = new List<PlayedGame>();

            SqliteCommand sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "SELECT * FROM PlayedGames;";
            SqliteDataReader dr = sqlcomm.ExecuteReader();

            while (dr.Read())
            {
                games.Add(new PlayedGame(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), new Score(dr.GetInt32(5), dr.GetInt32(6), dr.GetInt32(7), dr.GetInt32(8), dr.GetInt32(9), dr.GetInt32(10), dr.GetInt32(11), dr.GetInt32(12), dr.GetString(13))));
            }

            return games;
        }

        public static void AddGameToPlay(PlayedGame pGame)
        {
            try
            {
                SqliteCommand sqlcomm = sqlite_conn.CreateCommand();
                sqlcomm.CommandText = "INSERT INTO GamesToPlay VALUES ( " + pGame.Id + ", \"" + pGame.Name + "\", \"" + pGame.Released + "\", \"" + pGame.BackgroundImage + "\", \"" + pGame.AddedDate + "\", " + pGame.Score.Graphics + ", " + pGame.Score.Story + ", " + pGame.Score.Gameplay + ", " + pGame.Score.Bugs + ", " + pGame.Score.Length + ", " + pGame.Score.Performance + ", " + pGame.Score.Music + ", " + pGame.Score.Overall + ", \"" + pGame.Score.AdditionalComment + "\")";
                sqlcomm.ExecuteNonQuery();
                MessageBox.Show("Guardado para jugarlo.", "Guardado");
            }
            catch (SqliteException)
            {
                MessageBox.Show("Ya añadido.", "Info.");
            }
        }

        public static List<PlayedGame> GetGamesToPlay()
        {
            List<PlayedGame> games = new List<PlayedGame>();

            SqliteCommand sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "SELECT * FROM GamesToPlay;";
            SqliteDataReader dr = sqlcomm.ExecuteReader();

            while (dr.Read())
            {
                games.Add(new PlayedGame(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), new Score(dr.GetInt32(5), dr.GetInt32(6), dr.GetInt32(7), dr.GetInt32(8), dr.GetInt32(9), dr.GetInt32(10), dr.GetInt32(11), dr.GetInt32(12), dr.GetString(13))));
            }

            return games;
        }

        public static void RemovePlayedGame(int gameId)
        {
            SqliteCommand sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "DELETE FROM PlayedGames WHERE id = \"" + gameId + "\";";
            sqlcomm.ExecuteNonQuery();
        }

        public static void RemoveGameToPlay(int gameId)
        {
            SqliteCommand sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "DELETE FROM GamesToPlay WHERE id = \"" + gameId +"\";";
            sqlcomm.ExecuteNonQuery();
        }

        public static void UpdatePlayedGame(PlayedGame game)
        {
            SqliteCommand sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "UPDATE PlayedGames SET score_graphics = " + game.Score.Graphics + ", score_story = " + game.Score.Story + ", score_gameplay = " + game.Score.Gameplay + ", score_bugs = " + game.Score.Bugs + ", score_length = " + game.Score.Length + ", score_performance = " + game.Score.Performance +", score_music = " + game.Score.Music + ", score_overall = " + game.Score.Overall + ", score_additional = \"" + game.Score.AdditionalComment + "\"  WHERE id = \"" + game.Id + "\";";
            sqlcomm.ExecuteNonQuery();
        }

        /*public static void ResetDB()
        {
            SqliteCommand sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "DELETE FROM GamesToPlay;";
            sqlcomm.ExecuteNonQuery();
            sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "DROP TABLE GamesToPlay;";
            sqlcomm.ExecuteNonQuery();
            sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "DELETE FROM PlayedGames;";
            sqlcomm.ExecuteNonQuery();
            sqlcomm = sqlite_conn.CreateCommand();
            sqlcomm.CommandText = "DROP TABLE PlayedGames;";
            sqlcomm.ExecuteNonQuery();
        }*/
    }
}
