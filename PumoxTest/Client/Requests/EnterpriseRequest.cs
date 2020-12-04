using Newtonsoft.Json;
using Pumox.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Client.Requests
{
    public interface IPostRequest
    {
        Task<long> CreateCompany(EnterpriseCreateModel model);
        Task<IEnumerable<EnterpriseModel>> EnterpriseGetAll();
        Task<string> LoadEnterpriseJson();
        Task<string> DeleteEnterprise(long id);
        Task<string> UpdateEnterprise(EnterpriseUpdateModel model);
    }
    public class EnterpriseRequestHandler : IPostRequest
    {
        public  async Task<long> CreateCompany(EnterpriseCreateModel model)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/company/create");
                request.Method = "POST";
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    await writer.WriteAsync(JsonConvert.SerializeObject(model));
                    writer.Close();
                }

                var response = (HttpWebResponse)request.GetResponse();

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<long>(responseText);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<string> DeleteEnterprise(long id)
        {
            try
            {
                var msg = String.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/companies/"+id);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                using (var response = await request.GetResponseAsync())
                {
                    using(var reader = new StreamReader(response.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                    }
                }

                return msg;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        async Task<string> Get(HttpWebRequest request)
        {
            request.Method = "GET";
            request.ContentType = "application/json";

            using (var response = await request.GetResponseAsync())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = await reader.ReadToEndAsync();
                    return responseText;
                }
            }
        }

        public async Task<string> LoadSingle(long id)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/companies/"+id);
                return await Get(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public async Task<IEnumerable<EnterpriseModel>> EnterpriseGetAll()
        {
            try
            {
                var enterprises = await Task.Factory.StartNew(() =>
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EnterpriseModel>>(LoadEnterpriseJson().Result);
                });

                return enterprises;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> LoadEnterpriseJson()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/companies");
                return await Get(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<string> UpdateEnterprise(EnterpriseUpdateModel model)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/companies/"+model.Id);
                request.Method = "PUT";
                request.ContentType = "application/json";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    await writer.WriteAsync(JsonConvert.SerializeObject(model));
                    writer.Close();
                }

               
                var response = (HttpWebResponse)request.GetResponse();
             

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    return responseText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
