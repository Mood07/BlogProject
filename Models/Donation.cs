using System;

namespace BlogProject.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime DonatedAt { get; set; }
    }
}
