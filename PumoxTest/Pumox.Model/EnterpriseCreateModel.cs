using Pumox.Model;
using System.Collections.Generic;

namespace Pumox.Model
{
    public class EnterpriseCreateModel : EnterpriseBaseModel
    {
        public ICollection<EmployeeCreateModel> Employees { get; set; } = new List<EmployeeCreateModel>();
    }
}
