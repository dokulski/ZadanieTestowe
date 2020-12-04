using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pumox.Server.Data.Resources
{
    [Table("dbo.Employees")]
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"Administrator|Developer|Architect|Manager")]
        public string JobTitle { get; set; }
        [ForeignKey("Enterprise")]
        [Required]
        public long Enterprise_Id { get; set; }
        public virtual Enterprise Enterprise { get; set; }
    }
}
