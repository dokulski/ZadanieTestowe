using Client.Requests;
using Newtonsoft.Json;
using Pumox.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client
{
    public static class Demo
    {
        public async static void DeleteEnterprise(long id)
        {
            try
            {
                var enterpriseRequest = new EnterpriseRequestHandler();
                Console.WriteLine(await enterpriseRequest.DeleteEnterprise(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void PostEnterprise(EnterpriseCreateModel model)
        {
                var request = new EnterpriseRequestHandler();
                var enterprise = new EnterpriseCreateModel();

                enterprise.Name = model.Name;
                enterprise.EstablishmentYear = model.EstablishmentYear;
                enterprise.Employees = new List<EmployeeCreateModel>();
               if (model.Employees != null)
               {
                   var list = model.Employees.ToList();
                   for (int i = 0; i < model.Employees.Count; ++i)
                   {
                       var employee = new EmployeeCreateModel();
                       employee.Firstname = list[i].Firstname;
                       employee.Lastname = list[i].Lastname;
                       employee.DateOfBirth = list[i].DateOfBirth;
                       employee.JobTitle = list[i].JobTitle;
                       enterprise.Employees.Add(employee);
                   }
               }
                var res = request.CreateCompany(enterprise).Result;
                Console.WriteLine(res);
        }

        public static void PrintAllJsonEnterprise()
        {
                var enterpriseRequest = new EnterpriseRequestHandler();
                var json = enterpriseRequest.LoadEnterpriseJson();
                var deserialised = JsonConvert.DeserializeObject<IEnumerable<EnterpriseModel>>(json.Result);
                var pretty = JsonConvert.SerializeObject(deserialised, Formatting.Indented);

                Console.WriteLine(pretty);
        }

        public static void PutEnterprise(EnterpriseUpdateModel model)
        {
            var enterpriseRequest = new EnterpriseRequestHandler();
            var resp = enterpriseRequest.UpdateEnterprise(model).Result;
            Console.WriteLine(resp);
        }

        public static void GetEnterprise(long id)
        {
            var enterpriseRequest = new EnterpriseRequestHandler();
            var resp = enterpriseRequest.DeleteEnterprise(id).Result;
            Console.WriteLine(resp);
        }

    }
}
