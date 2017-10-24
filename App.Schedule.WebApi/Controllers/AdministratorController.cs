using System;
using System.Web.Http;
using App.Schedule.Context;
using System.Linq;
using App.Schedule.Domains.Helpers;
using System.Data.Entity;
using App.Schedule.Domains.ViewModel;
using App.Schedule.Domains;

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
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/administrator/5
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
        public IHttpActionResult Get(string loginid, string password)
        {
            try
            {
                var pass = Security.Encrypt(password, true);
                var status = _db.tblAdministrators.Any(d => d.LoginId == loginid && d.Password == pass);
                return Ok(new { status = status, data = status==true ? "valid credential" : "Not a valid credential" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // POST: api/administrator
        public IHttpActionResult Post([FromBody]AdministratorViewModel model)
        {
            try
            {
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
                    return Ok(new { status = true, data = admin });
                else
                    return Ok(new { status = false, data = "There was a problem." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
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
                            admin.Email = model.Email;
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

    }
}
