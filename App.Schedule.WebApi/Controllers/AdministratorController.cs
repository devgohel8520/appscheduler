using System;
using System.Web;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Domains;
using App.Schedule.Context;
using App.Schedule.Domains.Helpers;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    [Authorize]
    public class AdministratorController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public AdministratorController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/administrator
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblAdministrators.ToList();
                return Ok(new { status = true, message = "success", data = model });
            }
            catch
            {
                return Ok(new { status = false, message = "There was a problem. Please try again later.", data = "" });
            }
        }

        // GET: api/administrator/5
        [AllowAnonymous]
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblAdministrators.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model });
                    else
                        return Ok(new { status = false, data = "Not found." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/administrator/?loginid=value&password=value
        [AllowAnonymous]
        public IHttpActionResult Get(string loginid, string password)
        {
            try
            {
                password = HttpContext.Current.Server.UrlDecode(password);
                var pass = Security.Encrypt(password, true);
                var model = _db.tblAdministrators.Where(d => d.Email.ToLower() == loginid.ToLower() && d.Password
                == pass && d.IsActive == true).FirstOrDefault();
                if (model != null)
                {
                    model.Password = "";
                    return Ok(new { status = true, data = model, message = "Valid credential" });
                }
                else
                {
                    return Ok(new { status = false, data = model, message = "No a Valid credential" });
                }
                //return Ok(new { status = status, data = status == true ? "valid credential" : "Not a valid credential" });
            }
            catch(Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: "  + ex.Message.ToString()});
            }
        }

        // POST: api/administrator
        [AllowAnonymous]
        public IHttpActionResult Post([FromBody]AdministratorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    return Ok(new { status = false, message = errMessage , data = "" });
                }

                var isAny = _db.tblAdministrators.Any(d => d.Email.ToLower() == model.Email.ToLower());
                if (isAny)
                    return Ok(new { status = false, message = "Please try another email id.", data="" });

                var admin = new tblAdministrator()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    LoginId = model.LoginId,
                    Password = Security.Encrypt(model.Password, true),
                    IsAdmin = model.IsAdmin,
                    IsActive = model.IsActive,
                    ContactNumber = model.ContactNumber,
                    Created = DateTime.Now.ToUniversalTime(),
                    AdministratorId = model.AdministratorId,
                };

                _db.tblAdministrators.Add(admin);
                var response = _db.SaveChanges();

                if (response > 0)
                {
                    return Ok(new { status = true, message = "Successfully created.", data =admin });
                }
                return Ok(new { status = false, message = "There was a problem. Please try again later.", data="" });
            }
            catch(Exception ex)
            {
                return Ok(new { status = false, message = "ex: "+ex.Message.ToString(), data ="" });
            }
        }

        // PUT: api/administrator/5
        public IHttpActionResult Put(long? id, [FromBody]AdministratorViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var admin = _db.tblAdministrators.Find(id);
                    if (admin != null)
                    {
                        if (admin.LoginId.ToLower() == model.LoginId.ToLower())
                        {
                            admin.FirstName = model.FirstName;
                            admin.LastName = model.LastName;
                            admin.Password = Security.Encrypt(model.Password, true);
                            admin.IsAdmin = model.IsAdmin;
                            admin.IsActive = model.IsActive;
                            admin.ContactNumber = model.ContactNumber;

                            _db.Entry(admin).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = admin });
                            else
                                return Ok(new { status = false, data = "There was a problem to update the data." });
                        }
                        else
                        {
                            return Ok(new { status = false, data = "Please provide a valid login id." });
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
                return BadRequest(ex.Message.ToString());
            }
        }

        public IHttpActionResult Delete(long? id, bool status)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var admin = _db.tblAdministrators.Find(id);
                    if (admin != null)
                    {
                        admin.IsActive = status;
                        _db.Entry(admin).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = admin });
                        else
                            return Ok(new { status = false, data = "There was a problem to update the data." });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "Not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch
            {
                return Ok(new { status = false, data = "ex: Not a valid data to update. Please provide a valid id." });
            }
            //try
            //{
            //    if (String.IsNullOrEmpty(email))
            //        return Ok(new { status = false, data = "Please provide a valid email id." });
            //    else
            //    {
            //        var admin = _db.tblAdministrators.Where(d => d.Email.ToLower() == email.ToLower()).FirstOrDefault();
            //        //var isRegistered = _db.tblAdministrators.Any(d => d.Email.ToLower() == email.ToLower());
            //        if (admin != null)
            //        {
            //            _db.tblAdministrators.Remove(admin);
            //            var response = _db.SaveChanges();
            //            if (response > 0)
            //                return Ok(new { status = true, data = admin });
            //            else
            //                return Ok(new { status = false, data = "There was a problem to update the data." });
            //        }
            //        else
            //        {
            //            return Ok(new { status = false, data = "Not a valid data to update. Please provide a valid id." });
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message.ToString());
            //}
        }

        [NonAction]
        [AllowAnonymous]
        public bool RegisterAdmin(AdministratorViewModel model, out string message)
        {
            var status = false;
            try
            {
                var isAny = _db.tblAdministrators.Any(d => d.Email.ToLower() == model.Email.ToLower());
                if (isAny)
                    message = "Please try another email id.";

                var admin = new tblAdministrator()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    LoginId = model.Email,
                    Password = Security.Encrypt(model.Password, true),
                    IsAdmin = model.IsAdmin,
                    IsActive = model.IsActive,
                    ContactNumber = model.ContactNumber,
                    Created = DateTime.Now.ToUniversalTime(),
                    AdministratorId = model.AdministratorId,
                };

                _db.tblAdministrators.Add(admin);
                var response = _db.SaveChanges();
                if (response > 0)
                {
                    status = true;
                    message = "Successed.";
                }
                else
                {
                    message = "Failed.";
                }
            }
            catch(Exception ex)
            {
                var errmsg = ex.Message.ToString();
                if (ex.InnerException != null)
                    errmsg += "; " + ex.InnerException.Message.ToString();

                message = "There was a problem. Please try again later." + errmsg;
            }
            return status;
        }

        [NonAction]
        public bool UpdateAdmin(AdministratorViewModel model, out string message)
        {
            var status = false;
            try
            {
                var admin = _db.tblAdministrators.Find(model.Id);
                if (admin != null)
                {
                    if (admin.Email.ToLower() == model.Email.ToLower())
                    {
                        admin.FirstName = model.FirstName;
                        admin.LastName = model.LastName;
                        admin.Password = Security.Encrypt(model.Password, true);
                        admin.IsAdmin = model.IsAdmin;
                        admin.IsActive = model.IsActive;
                        admin.ContactNumber = model.ContactNumber;

                        _db.Entry(admin).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                        {
                            status = true;
                            message = "Successed.";
                        }
                        else
                        {
                            message = "Failed.";
                        }
                    }
                    else
                    {
                        message = "Please enter a valid email id.";
                    }
                }
                else
                {
                    message = "Not a valid admin.";
                }
            }
            catch
            {
                message = "There was a problem. Please try again later.";
            }
            return status;
        }
    }
}
