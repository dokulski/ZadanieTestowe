using Pumox.Model;
using Pumox.Server.Data.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pumox.Server.Data
{
    public static class EnterpriseModelMapper
    {
        public static EnterpriseModel MapEnterprise(Enterprise enterprise)
        {
            try
            {
                if (enterprise == null)
                    return null;

                var model = new EnterpriseModel();
                model.Id = enterprise.Id;
                model.Name = enterprise.Name;
                model.EstablishmentYear = enterprise.EstablishmentYear;
                model.Employees = MapEmployeeList(enterprise.Employees);

                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static ICollection<EmployeeModel> MapEmployeeList(ICollection<Employee> employees)
        {
            try
            {
                var list = new List<EmployeeModel>();

                if (list != null)
                {
                    var employeeModel = new EmployeeModel();
                    foreach(var item in employees)
                    {
                        var employee = new EmployeeModel();
                        employee.Id = item.Id;
                        employee.Firstname = item.FirstName;
                        employee.Lastname = item.LastName;
                        employee.DateOfBirth = item.DateOfBirth;
                        employee.JobTitle = item.JobTitle;
                        list.Add(employee);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
