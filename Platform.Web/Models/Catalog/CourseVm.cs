namespace Platform.Web.Models.Catalog
{
    public class CourseVm
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ShortDescription
        {
            get => Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description;
        }

        public string UserId { get; set; }
        public string Picture { get; set; }
        public string StockPictureUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public FeatureVm Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryVm Category { get; set; }
    }
}
