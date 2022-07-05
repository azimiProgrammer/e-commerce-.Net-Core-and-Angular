namespace Core.Entities
{
    public class SlideShow : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long DocumentId { get; set; }
        public string Url { get; set; }
    }
}