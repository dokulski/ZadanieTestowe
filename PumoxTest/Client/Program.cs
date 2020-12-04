using Client.Requests;
using Pumox.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var model = new EnterpriseCreateModel();
                model.Name = "SomeName";
                model.EstablishmentYear = 1990;
                model.Employees = new List<EmployeeCreateModel>() 
                {
                    new EmployeeCreateModel()
                    {
                        Firstname = "Mike",
                        Lastname = "Michaels",
                        DateOfBirth = DateTime.ParseExact("1980-11-11","yyyy-mm-dd", CultureInfo.InvariantCulture),
                        JobTitle = "Administrator",
                    },
                    new EmployeeCreateModel()
                    {
                        Firstname = "Jake",
                        Lastname = "Ramirez",
                        DateOfBirth = DateTime.ParseExact("1990-11-17", "yyyy-mm-dd", CultureInfo.InvariantCulture),
                        JobTitle = "Administrator",
                    },
                    new EmployeeCreateModel()
                    {
                        Firstname = "Suzy",
                        Lastname = "Ramirez",
                        DateOfBirth = DateTime.ParseExact("1992-06-27", "yyyy-mm-dd", CultureInfo.InvariantCulture),
                        JobTitle = "Developer",
                    },
                };

                Demo.PostEnterprise(model);
                Demo.PrintAllJsonEnterprise();

                var putModel = new EnterpriseUpdateModel();
                putModel.Id = 19;
                putModel.Name = "SomeNamex";
                putModel.Employees = new List<EmployeeUpdateModel>();
                putModel.EstablishmentYear = 1999;

                Demo.PutEnterprise(putModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
