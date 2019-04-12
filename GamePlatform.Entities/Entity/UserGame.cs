namespace GamePlatform.Entities.Entity {
    public class UserGame {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}
