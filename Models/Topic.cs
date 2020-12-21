using System;

namespace PublicForum.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AspNetUsers User { get; set; }
        public DateTime CreateData { get; set; }
    }
}