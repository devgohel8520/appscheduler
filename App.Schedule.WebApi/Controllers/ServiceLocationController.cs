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
    public class ServiceLocationController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public ServiceLocationController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/servicelocation
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblServiceLocations.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/servicelocation/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblServiceLocations.Find(id);
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

        // POST: api/servicelocation
        public IHttpActionResult Post([FromBody]ServiceLocationViewModel model)
        {
            try
            {
                var serviceLocation = new tblServiceLocation()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Add1 = model.Add1,
                    Add2 = model.Add2,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip,
                    CountryId = model.CountryId,
                    Created = DateTime.Now.ToUniversalTime(),
                    IsActive = model.IsActive,
                    BusinessId = model.BusinessId,
                    TimezoneId = model.TimezoneId
                };
                _db.tblServiceLocations.Add(serviceLocation);
                var response = _db.SaveChanges();
                if (response > 0)
                    return Ok(new { status = true, data = serviceLocation });
                else
                    return Ok(new { status = false, data = "There was a problem." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // PUT: api/servicelocation/5
        public IHttpActionResult Put(long? id, [FromBody]ServiceLocationViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var serviceLocation = _db.tblServiceLocations.Find(id);
                    if (serviceLocation != null)
                    {
                        serviceLocation.Name = model.Name;
                        serviceLocation.Description = model.Description;
                        serviceLocation.Add1 = model.Add1;
                        serviceLocation.Add2 = model.Add2;
                        serviceLocation.City = model.City;
                        serviceLocation.State = model.State;
                        serviceLocation.Zip = model.Zip;
                        serviceLocation.CountryId = model.CountryId;
                        serviceLocation.Created = DateTime.Now.ToUniversalTime();
                        serviceLocation.IsActive = model.IsActive;
                        serviceLocation.BusinessId = model.BusinessId;
                        serviceLocation.TimezoneId = model.TimezoneId;

                        _db.Entry(serviceLocation).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = serviceLocation });
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
                return BadRequest(ex.Message.ToString());
            }
        }

        // DELETE: api/servicelocation/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var serviceLocation = _db.tblServiceLocations.Find(id);
                    if (serviceLocation != null)
                    {
                        serviceLocation.IsActive = !serviceLocation.IsActive;
                        _db.Entry(serviceLocation).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = serviceLocation });
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
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
