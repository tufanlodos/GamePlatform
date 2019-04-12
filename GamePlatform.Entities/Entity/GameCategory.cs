namespace GamePlatform.Entities.Entity {
    public class GameCategory {
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        public bool? IsGenre { get; set; }
    }
}