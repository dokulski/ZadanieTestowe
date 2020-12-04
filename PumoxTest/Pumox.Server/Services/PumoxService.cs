using Pumox.Model;
using Pumox.Server.Data;
using Pumox.Server.Data.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;

namespace Pumox.Server.Services
{
    public interface IPumoxService
    {
        Task<long?> CompanyCreate(EnterpriseCreateModel model);
        Task<IEnumerable<EnterpriseModel>> EnterpriseGetAll();
        Task<EnterpriseModel> EnterpriseGet(int id);
        Task<long?> EnterpriseUpdate(long id,EnterpriseUpdateModel model);
        Task<bool> EnterpriseDelete(long id);
    }
    public class PumoxService : IPumoxService
    {
        public async Task<long?> CompanyCreate(EnterpriseCreateModel model)
        {
            long id = 0;
            try
            {
                using(var ctx = new dbPumox())
                {
                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        Enterprise enterprise = new Enterprise()
                        {
                            Name = model.Name,
                            EstablishmentYear = model.EstablishmentYear,
                            Employees = new List<Employee>()
                        };

                        if (model.Employees != null)
                        {
                            foreach (var employee in model.Employees)
                            {
                                enterprise.Employees.Add(new Employee
                                {
                                    FirstName = employee.Firstname,
                                    LastName = employee.Lastname,
                                    DateOfBirth = employee.DateOfBirth,
                                    JobTitle = employee.JobTitle,
                                });
                            }
                        }

                        ctx.Enterprises.Add(enterprise);
                        await ctx.SaveChangesAsync();
                        scope.Complete();
                        id = enterprise.Id;
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> EnterpriseDelete(long id)
        {
            try
            {
                using(var ctx = new dbPumox())
                {
                    using(var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var ent = await ctx.Enterprises.FindAsync(id);
                        ctx.Enterprises.Remove(ent);
                        ctx.SaveChanges();
                        scope.Complete();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<EnterpriseModel> EnterpriseGet(int id)
        {
            try
            {
                var enterpriseModel = new EnterpriseModel();
                enterpriseModel.Employees = new List<EmployeeModel>();
                using(var ctx = new dbPumox())
                {
                    var ent = await ctx.Enterprises.Where(x => x.Id == id).FirstOrDefaultAsync();
                    enterpriseModel = EnterpriseModelMapper.MapEnterprise(ent);
                }

                return enterpriseModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<EnterpriseModel>> EnterpriseGetAll()
        {
            try
            {
                var enterprises = new List<EnterpriseModel>();
                using(var ctx = new dbPumox())
                {
                    var enterpriseList = await ctx.Enterprises.ToListAsync();

                    if(enterpriseList!=null)
                        foreach(var item in enterpriseList)
                        {
                            var enterprise = EnterpriseModelMapper.MapEnterprise(item);
                            enterprises.Add(enterprise);
                        }
                }
                return enterprises;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<long?> EnterpriseUpdate(long id,EnterpriseUpdateModel model)
        {
            try
            {
                var enterprise = new Enterprise();

                using(var ctx = new dbPumox())
                {
                   using(var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                   {
                        enterprise = await ctx.Enterprises.Where(x => x.Id == id).FirstOrDefaultAsync();
                        if (enterprise == null)
                            return null;

                        enterprise.Name = model.Name;
                        enterprise.EstablishmentYear = model.EstablishmentYear;
                        CreateOrUpdateEmployee(model,enterprise);
                        await ctx.SaveChangesAsync();

                        scope.Complete();
                    }
                }

                return enterprise.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private void CreateOrUpdateEmployee(EnterpriseUpdateModel model, Enterprise enterprise)
        {
            if (model.Employees != null)
            {
                foreach (var modelItem in model.Employees)
                {
                    Employee employee = null;
                    if ((employee = enterprise.Employees?.FirstOrDefault(x => x.Id == modelItem.Id)) != null)
                    {
                        SetEmployeeValues(employee, modelItem);
                    }
                    else
                    {
                        employee = new Employee();
                        SetEmployeeValues(employee, modelItem);
                        enterprise.Employees.Add(employee);
                    }
                }
            }
        }

        private void SetEmployeeValues(Employee employee, EmployeeUpdateModel modelItem)
        {
            employee.FirstName = modelItem.Firstname;
            employee.LastName = modelItem.Lastname;
            employee.DateOfBirth = modelItem.DateOfBirth;
            employee.JobTitle = modelItem.JobTitle;
        }
    }
}
