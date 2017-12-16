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
                return Ok(new { status = true, data = model, message = "Transaction successed." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // GET: api/administrator/5
        [AllowAnonymous]
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
                else
                {
                    var model = _db.tblAdministrators.Find(id);
                    if (model != null)
                        return Ok(new { status = true, data = model, message = "Transaction successed." });
                    else
                        return Ok(new { status = false, data = "", message = "Not found." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
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
                    return Ok(new { status = false, data = model, message = "Not a valid credential" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // POST: api/administrator
        public IHttpActionResult Post([FromBody]AdministratorViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                    return Ok(new { status = false, data = "", message = errMessage });
                }

                var isAny = _db.tblAdministrators.Any(d => d.Email.ToLower() == model.Email.ToLower());
                if (isAny)
                    return Ok(new { status = false, data = "", message = "Please try another email id." });

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
                    return Ok(new { status = true, data = admin, message = "Transaction successed." });
                }
                return Ok(new { status = false, data = "", message = "Transaction failed." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        // PUT: api/administrator/5
        public IHttpActionResult Put(long? id, [FromBody]AdministratorViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "", message = "Please provide a valid id." });
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
                                return Ok(new { status = true, data = admin, message = "Transaction successed." });
                            else
                                return Ok(new { status = false, data = "", message = "Transaction failed." });
                        }
                        else
                        {
                            return Ok(new { status = false, data = "", message = "Please provide a valid administrator id." });
                        }
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "Not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        public IHttpActionResult Delete(long? id, bool status)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid admin id." });
                else
                {
                    var admin = _db.tblAdministrators.Find(id);
                    if (admin != null)
                    {
                        admin.IsActive = status;
                        _db.Entry(admin).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = admin, message = "Transaction successed." });
                        else
                            return Ok(new { status = false, data = "", message = "Transaction failed." });
                    }
                    else
                    {
                        return Ok(new { status = false, data = "", message = "Not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { status = false, data = "", message = "ex: " + ex.Message.ToString() });
            }
        }

        [NonAction]
        [AllowAnonymous]
        public bool RegisterAdmin(AdministratorViewModel model, out tblAdministrator data, out string message)
        {
            var status = false;
            data = null;
            try
            {
                var isAny = _db.tblAdministrators.Any(d => d.Email.ToLower() == model.Email.ToLower());
                if (isAny)
                    message = "This email id has been taken. Please try another email id.";

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
                    message = "Transaction successed.";
                    data = admin;
                }
                else
                {
                    message = "Transaction failed.";
                }
            }
            catch (Exception ex)
            {
                message = "ex: " + ex.Message.ToString();
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
                            message = "Transaction successed.";
                        }
                        else
                        {
                            message = "Transaction failed.";
                        }
                    }
                    else
                    {
                        message = "Please enter a valid email id.";
                    }
                }
                else
                {
                    message = "It is not a valid Admin information.";
                }
            }
            catch (Exception ex)
            {
                message = "ex: " + ex.Message.ToString();
            }
            return status;
        }

        [NonAction]
        public bool BusinessHourDefaultSetup(long serviceLocationId)
        {
            var result = false;
            try
            {
                var now = DateTime.Now;
                for (var i = 0; i < 7; i++)
                {
                    var businessHour = new tblBusinessHour()
                    {
                        WeekDayId = i,
                        IsStartDay = i == 1 ? true : false,
                        IsHoliday = i == 1 ? true : false,
                        From = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0, DateTimeKind.Utc),
                        To = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0, DateTimeKind.Utc),
                        IsSplit1 = false,
                        FromSplit1 = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0, DateTimeKind.Utc),
                        ToSplit1 = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0, DateTimeKind.Utc),
                        IsSplit2 = false,
                        FromSplit2 = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0, DateTimeKind.Utc),
                        ToSplit2 = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0, DateTimeKind.Utc),
                        ServiceLocationId = serviceLocationId
                    };
                    _db.tblBusinessHours.Add(businessHour);
                }
                var response = _db.SaveChanges();
                if (response > 0)
                    result = true;
                else
                    result = false;
            }
            catch
            {
                result = false;
            }
            return result;
        }

    }
}
