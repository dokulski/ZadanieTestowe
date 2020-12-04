using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pumox.Model
{
    public class EnterpriseUpdateModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int EstablishmentYear { get; set; }
        public IEnumerable<EmployeeUpdateModel> Employees { get; set; }
        
    }
}
