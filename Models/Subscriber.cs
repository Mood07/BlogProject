using System;

namespace BlogProject.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime SubscribedAt { get; set; }
    }
}
