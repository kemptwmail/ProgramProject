namespace FinalProject.Models
{
    public class Breakfast
    {
        public int Id { get; set; } // Primary Key
        public string FoodName { get; set; }
        public bool IsHealthy { get; set; } // Whether the food is healthy or not
        public string Ingredients { get; set; }
        public string PreparationTime { get; set; } // Time in minutes

    }
}
