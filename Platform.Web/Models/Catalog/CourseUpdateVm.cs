using System.ComponentModel.DataAnnotations;

namespace Platform.Web.Models.Catalog
{
    public class CourseUpdateVm
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedTime { get; set; }
        public FeatureVm Feature { get; set; }
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        public IFormFile PhotoFormFile { get; set; }

    }
}
