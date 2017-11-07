using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace App.Schedule.WebApi.Controllers
{
    public class MembershipController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public MembershipController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/membership
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblMemberships.ToList();
                return Ok(new { status = true, data = model, message = "success" });
            }
            catch
            {
                return Ok(new { status = false, message = "There was a problem. Please try again later.", data = "" });
            }
        }

        // GET: api/membership/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, message = "Please provide a valid membership id.", data = "" });
                else
                {
                    var model = _db.tblMemberships.Find(id);
                    if (model != null)
                        return Ok(new { status = true, message = "", data = model });
                    else
                        return Ok(new { status = false, message = "Not found.", data = "" });
                }
            }
            catch
            {
                return Ok(new { status = false, message = "There was a problem. Please try again later.", data = "" });
            }
        }

        // POST: api/membership
        public IHttpActionResult Post([FromBody]MembershipViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var membership = new tblMembership()
                    {
                        Created = DateTime.Now.ToUniversalTime(),
                        Description = model.Description,
                        IsActive = model.IsActive,
                        Title = model.Title,
                        Benifits = model.Benifits,
                        IsUnlimited = model.IsUnlimited,
                        Price = model.Price,
                        TotalAppointment = model.TotalAppointment,
                        TotalCustomer = model.TotalCustomer,
                        TotalEmployee = model.TotalEmployee,
                        TotalLocation = model.TotalLocation,
                        TotalOffers = model.TotalLocation,
                        AdministratorId = model.AdministratorId
                    };
                    _db.tblMemberships.Add(membership);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, message = "success", data = membership });
                    else
                        return Ok(new { status = false, message = "There was a problem. Please try again later.", data = "" });
                }
                else
                {
                    return Ok(new { status = false, message = "There was a problem. Please try again later.", data = "" });
                }
            }
            catch
            {
                return Ok(new { status = false, message = "There was a problem. Please try again later.", data = "" });
            }
        }

        // PUT: api/membership/5
        public IHttpActionResult Put(long? id, [FromBody]MembershipViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, message = "Please provide a valid membership ID.", data = "" });
                else
                {
                    var membership = _db.tblMemberships.Find(id);
                    if (membership != null)
                    {
                        membership.Created = DateTime.Now.ToUniversalTime();
                        membership.Description = model.Description;
                        membership.IsActive = model.IsActive;
                        membership.Title = model.Title;
                        membership.Benifits = model.Benifits;
                        membership.IsUnlimited = model.IsUnlimited;
                        membership.Price = model.Price;
                        membership.TotalAppointment = model.TotalAppointment;
                        membership.TotalCustomer = model.TotalCustomer;
                        membership.TotalEmployee = model.TotalEmployee;
                        membership.TotalLocation = model.TotalLocation;
                        membership.TotalOffers = model.TotalLocation;
                        membership.AdministratorId = model.AdministratorId;

                        _db.Entry(membership).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, message = "success", data = membership });
                        else
                            return Ok(new { status = false, message = "There was a problem to update the data.", data="" });
                    }
                    else
                    {
                        return Ok(new { status = false, message = "Not a valid data to update. Please provide a valid id." });
                    }
                }
            }
            catch
            {
                return Ok(new { status = false, message = "There was a problem. Please try again later.", data = "" });
            }
        }

        // DELETE: api/membership/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid membership ID." });
                else
                {
                    var membership = _db.tblMemberships.Find(id);
                    if (membership != null)
                    {
                        membership.IsActive = !membership.IsActive;
                        _db.Entry(membership).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, message = "success", data = membership });
                        else
                            return Ok(new { status = false, message = "There was a problem to update the data.", data ="" });
                    }
                    else
                    {
                        return Ok(new { status = false, message = "Not a valid data to update. Please provide a valid id.", data =""});
                    }
                }
            }
            catch
            {
                return Ok(new { status = false, message = "Not a valid data to update. Please provide a valid id." });
            }
        }
    }
}
