using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232Project.Entities
{
    [Table("option_values")]
    public class OptionValue
    {
        [Key]
        public int Id { get; set; }

        public string Name
        { get; set; }

        [Required]
        public int OptionId { get; set; }

        [ForeignKey("OptionId")]
        public Option Option { get; set; }
    }
}
