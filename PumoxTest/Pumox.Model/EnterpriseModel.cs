using System;
using System.Collections.Generic;


namespace Pumox.Model
{
    public class EnterpriseModel : EnterpriseBaseModel
    {
        public long Id { get; set; }
        public ICollection<EmployeeModel> Employees { get; set; }
    }
}
