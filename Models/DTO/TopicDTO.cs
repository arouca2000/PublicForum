using System;

namespace PublicForum.Models.DTO
{
    public class TopicDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AspNetUsers User { get; set; }
        public DateTime CreateData { get; set; }
        public bool TopicCreator { get; set; }
    }
}