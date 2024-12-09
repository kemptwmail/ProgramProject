namespace FinalProject.Models
{
    public class Project
    {
        public int Id { get; set; } // Primary Key
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

    }
}
