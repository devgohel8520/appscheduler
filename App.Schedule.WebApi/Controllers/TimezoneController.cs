using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace App.Schedule.WebApi.Controllers
{
    public class TimezoneController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public TimezoneController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/timezone
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblTimezones.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/timezone/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblTimezones.Find(id);
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

        // POST: api/timezone
        public IHttpActionResult Post([FromBody]TimezoneViewModel model)
        {
            try
            {
                var timeZone = new tblTimezone()
                {
                    Title = model.Title,
                    IsDST = model.IsDST,
                    UtcOffset = model.UtcOffset,
                    CountryId = model.CountryId,
                    AdministratorId = model.AdministratorId
                };
                _db.tblTimezones.Add(timeZone);
                var response = _db.SaveChanges();
                if (response > 0)
                    return Ok(new { status = true, data = timeZone });
                else
                    return Ok(new { status = false, data = "There was a problem." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // PUT: api/timezone/5
        public IHttpActionResult Put(long? id, [FromBody]TimezoneViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var timeZone = _db.tblTimezones.Find(id);
                    if (timeZone != null)
                    {
                        timeZone.Title = model.Title;
                        timeZone.IsDST = model.IsDST;
                        timeZone.UtcOffset = model.UtcOffset;
                        timeZone.CountryId = model.CountryId;
                        timeZone.AdministratorId = model.AdministratorId;

                        _db.Entry(timeZone).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = timeZone });
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

        // DELETE: api/timeZone/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var timeZone = _db.tblTimezones.Find(id);
                    if (timeZone != null)
                    {
                        _db.tblTimezones.Remove(timeZone);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = "Successfully removed record." });
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
