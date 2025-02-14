
namespace ToDoGames.Model
{
    public class Score
    {
        public int Graphics { get; set; }
        public int Story { get; set; }
        public int Gameplay { get; set; }
        public int Bugs { get; set; }
        public int Length { get; set; }
        public int Performance { get; set; }
        public int Music { get; set; }
        public int Overall { get; set; }
        public string AdditionalComment { get; set; }

        public Score(int graphics, int story, int gameplay, int bugs, int length, int performane, int music, int overall, string additionalComment)
        {
            Graphics = graphics;
            Story = story;
            Gameplay = gameplay;
            Bugs = bugs;
            Length = length;
            Performance = performane;
            Music = music;
            Overall = overall;
            AdditionalComment = additionalComment;
        }
    }
}
