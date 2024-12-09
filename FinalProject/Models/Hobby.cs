namespace FinalProject.Models
{
    public class Hobby
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public int DifficultyLevel { get; set; } // Scale 1-10
        public bool IsOutdoor { get; set; } // Whether the hobby is typically outdoor

    }
}
