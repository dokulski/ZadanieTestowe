using Pumox.Model;
using Pumox.Server.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace Pumox.Server.Controllers
{
    public class CompanyApiController : ApiController
    {
        [HttpDelete]
        [Route("companies/{id}")]
        public IHttpActionResult EnterpriseDelete(long id)
        {
            try
            {
                var service = new PumoxService();
                var deleted = service.EnterpriseDelete(id).Result;
                if (!deleted)
                    return NotFound();

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Enterprise has been deleted"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPut]
        [Route("companies/{id}")]
        public IHttpActionResult EnterpriseUpdate(long id,[FromBody]EnterpriseUpdateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("The model is not valid");

                var service = new PumoxService();
                var enterpriseId = service.EnterpriseUpdate(id,model).Result;
                if (enterpriseId==null)
                    return NotFound();

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, enterpriseId));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("companies/{id}")]
        public IHttpActionResult EnterpriseGet(int id)
        {
            try
            {
                var service = new PumoxService();
                var enterpriseModel = service.EnterpriseGet(id).Result;
                if (enterpriseModel == null)
                    return NotFound();

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, service.EnterpriseGet(id).Result));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("companies")]
        public IHttpActionResult EnterpriseGetAll()
        {
            try
            {
                var service = new PumoxService();
                var result = service.EnterpriseGetAll().Result;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [Route("company/create")]
        public IHttpActionResult CompanyCreate([FromBody]EnterpriseCreateModel model)
        {
            long? id=0;
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("The model is not valid");

                var service = new PumoxService();
                id = service.CompanyCreate(model).Result;
                if (id == null)
                    return InternalServerError();

                return Json(id);
            }
            catch (HttpException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
