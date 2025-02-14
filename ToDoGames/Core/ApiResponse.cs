using System.Collections.Generic;
using ToDoGames.Model;

namespace ToDoGames.Core
{
    public class ApiResponse
    {
        public List<Result> results { get; set; }
    }

    public class Result
    {
        public string name { get; set; }
        public string released { get; set; }
        public string background_image { get; set; }
        public int id { get; set; }

        public Result(int id, string name, string released, string background_image)
        {
            this.id = id;
            this.name = name;
            this.released = released;
            this.background_image = background_image;
        }

        public PlayedGame ToPlayedGame()
        {
            return new PlayedGame(id, name, released, background_image, "", new Score(0,0,0,0,0,0,0,0,""));
        }
    }
}
