using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultiDataSourceExample.Entity
{
    [Table("TestTable")]
    public class EntityA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
