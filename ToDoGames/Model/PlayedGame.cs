using ToDoGames.Core;

namespace ToDoGames.Model
{
    public class PlayedGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Released { get; set; }
        public string BackgroundImage { get; set; }
        public string AddedDate { get; set; }
        public Score Score { get; set; }

        public PlayedGame()
        {
            Id = 0;
            Name = "undefined";
            Released = "00-00";
            BackgroundImage = "";
            AddedDate = "00-00";
            Score = null;
        }

        public PlayedGame(int Id, string Name, string Released, string BackgroundImage, string AddedDate, Score Score)
        {
            this.Id = Id;
            this.Name = Name;
            this.Released = Released;
            this.BackgroundImage = BackgroundImage;
            this.AddedDate = AddedDate;
            this.Score = Score;
        }

        public Result ToResult()
        {
            return new Result(this.Id, this.Name, this.Released, this.BackgroundImage);
        }
    }
}