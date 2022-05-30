using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        //public IEnumerable<Employee> Get()
        //{
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        return entities.Employees.ToList();
        //    }
        //}

        //[BasicAuthentication]
        //public HttpResponseMessage Get(string gender = "All")
        //{
        //    string username = Thread.CurrentPrincipal.Identity.Name;
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        switch (username.ToLower())
        //        {
        //            case "male":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
        //            case "female":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
        //            default:
        //                return Request.CreateResponse(HttpStatusCode.BadRequest);
        //        }
        //    }
        //}


        public HttpResponseMessage Get()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            return Request.CreateResponse(HttpStatusCode.OK, username);
            //using (EmployeeDBEntities entities = new EmployeeDBEntities())
            //{
            //    //switch (username.ToLower())
            //    //{
            //    //    case "male":
            //    //        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
            //    //    case "female":
            //    //        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
            //    //    default:
            //    //        return Request.CreateResponse(HttpStatusCode.BadRequest);
            //    //}

            //    switch (username.ToLower())
            //    {
            //        case "male":
            //            return Request.CreateResponse(HttpStatusCode.OK, username);
            //        case "female":
            //            return Request.CreateResponse(HttpStatusCode.OK, username);
            //        default:
            //            return Request.CreateResponse(HttpStatusCode.BadRequest);
            //    }
            //}
        }

        public HttpResponseMessage Get(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee With ID:" + id + " Not Found!");
                }
            }
        }


        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                try
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
               
            }

        }


    }
}
