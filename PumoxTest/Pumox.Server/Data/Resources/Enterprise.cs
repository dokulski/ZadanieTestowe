using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pumox.Server.Data.Resources
{
    [Table("dbo.Enterprises")]
    public class Enterprise
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
