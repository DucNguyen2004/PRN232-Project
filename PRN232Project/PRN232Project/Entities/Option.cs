using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232Project.Entities
{
    [Table("options")]
    public class Option
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProductOption> ProductOptions { get; set; }
        public ICollection<OptionValue> OptionValues { get; set; }
    }
}
