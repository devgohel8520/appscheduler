using System;
using System.Web.Http;
using App.Schedule.Context;
using System.Linq;
using App.Schedule.Domains.Helpers;
using System.Data.Entity;
using App.Schedule.Domains.ViewModel;
using App.Schedule.Domains;
using System.Web;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessEmployeeController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessEmployeeController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businessemployee
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblBusinessEmployees.ToList();
                return Ok(new { status = true, data = model, message ="success" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // GET: api/businessemployee/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessEmployees.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model, message = "success" });
                    else
                        return Ok(new { status = false, data = "", message = "Not found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // GET: api/businessemployee/?emailid=value&password=value
        public IHttpActionResult Get(string email, string password)
        {
            try
            {
                var loginSession = new LoginSessionViewModel();
                password = HttpContext.Current.Server.UrlDecode(password);
                var pass = Security.Encrypt(password, true);
                loginSession.Employee = _db.tblBusinessEmployees.Where(d => d.Email.ToLower() == email.ToLower() && d.Password
                == pass && d.IsActive == true).FirstOrDefault();
                if (loginSession.Employee != null)
                {
                    loginSession.Employee.Password = "";
                    var serviceLocation = _db.tblServiceLocations.Find(loginSession.Employee.ServiceLocationId);
                    if (serviceLocation != null)
                    {
                        loginSession.Business = _db.tblBusinesses.Find(serviceLocation.BusinessId);
                        return Ok(new { status = true, data = loginSession, message = "Valid credential" });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "Not a valid credential" });
                    }
                }
                else
                {
                    return Ok(new { status = false, data = "", message = "Not a valid credential" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // POST: api/businessemployee
        public IHttpActionResult Post([FromBody]BusinessEmployeeViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var businessEmployee = new tblBusinessEmployee()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        LoginId = model.LoginId,
                        Password = Security.Encrypt(model.Password, true),
                        Email = model.Email,
                        STD = model.STD,
                        PhoneNumber = model.PhoneNumber,
                        ServiceLocationId = model.ServiceLocationId,
                        IsAdmin = model.IsAdmin,
                        Created = DateTime.Now.ToUniversalTime(),
                        IsActive = model.IsActive
                    };
                    _db.tblBusinessEmployees.Add(businessEmployee);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = businessEmployee });
                    else
                        return Ok(new { status = false, data = "There was a problem." });
                }
                else
                {
                    return Ok(new { status = false, data = "Model is not valid." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // PUT: api/businessemployee/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessEmployeeViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var businessEmployee = _db.tblBusinessEmployees.Find(id);
                    if (businessEmployee != null)
                    {
                        if (businessEmployee.LoginId.ToLower() == model.LoginId.ToLower())
                        {
                            businessEmployee.FirstName = model.FirstName;
                            businessEmployee.LastName = model.LastName;
                            businessEmployee.LoginId = model.LoginId;
                            businessEmployee.Password = Security.Encrypt(model.Password, true);
                            businessEmployee.Email = model.Email;
                            businessEmployee.STD = model.STD;
                            businessEmployee.PhoneNumber = model.PhoneNumber;
                            businessEmployee.ServiceLocationId = model.ServiceLocationId;
                            businessEmployee.IsAdmin = model.IsAdmin;
                            businessEmployee.Created = DateTime.Now.ToUniversalTime();
                            businessEmployee.IsActive = model.IsActive;

                            _db.Entry(businessEmployee).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = businessEmployee });
                            else
                                return Ok(new { status = false, data = "There was a problem to update the data." });
                        }
                        else
                        {
                            return Ok(new { status = false, data = "Please provide a valid login id to update." });
                        }
                    }
                    else
                    {
                        return Ok(new { status = false, data = "Not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }

        // DELETE: api/businessemployee/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var businessEmployee = _db.tblBusinessEmployees.Find(id);
                    if (businessEmployee != null)
                    {
                        businessEmployee.IsActive = !businessEmployee.IsActive;
                        _db.Entry(businessEmployee).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessEmployee });
                        else
                            return Ok(new { status = false, data = "There was a problem to update the data." });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "Not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = ex.Message.ToString() });
            }
        }
    }
}
