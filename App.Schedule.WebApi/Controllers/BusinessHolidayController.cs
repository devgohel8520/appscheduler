using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessHolidayController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessHolidayController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businesshour
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblBusinessHolidays.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/businesshour/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessHolidays.Find(id);
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

        // POST: api/businesshour
        public IHttpActionResult Post([FromBody]BusinessHolidayViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var busineessHoliday = new tblBusinessHoliday()
                    {
                        OnDate = model.OnDate,
                        Type = model.Type,
                        ServiceLocationId = model.ServiceLocationId
                    };
                    _db.tblBusinessHolidays.Add(busineessHoliday);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = busineessHoliday });
                    else
                        return Ok(new { status = false, data = "There was a problem." });
                }
                else
                {
                    return Ok(new { status = false, data = "There was a problem." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // PUT: api/businesshour/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessHolidayViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    if (model != null)
                    {
                        var businessHoliday = _db.tblBusinessHolidays.Find(id);
                        if (businessHoliday != null)
                        {
                            businessHoliday.OnDate = model.OnDate;
                            businessHoliday.Type = model.Type;
                            businessHoliday.ServiceLocationId = model.ServiceLocationId;

                            _db.Entry(businessHoliday).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = businessHoliday });
                            else
                                return Ok(new { status = false, data = "There was a problem to update the data." });
                        }
                    }
                    return Ok(new { status = false, data = "Not a valid data to update. Please provide a valid id." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // DELETE: api/businesshour/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var businessHoliday = _db.tblBusinessHolidays.Find(id);
                    if (businessHoliday != null)
                    {
                        _db.tblBusinessHolidays.Remove(businessHoliday);
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = "Successfully removed." });
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
