namespace SalesWebMvc.Models
{
    public class Notes
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Notes()
        {

        }
         
        public Notes(int id, string title, string content, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
        }
    }
}
