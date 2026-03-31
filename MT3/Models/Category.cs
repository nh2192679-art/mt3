using System.ComponentModel.DataAnnotations;

namespace MT3.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? IconClass { get; set; }

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
