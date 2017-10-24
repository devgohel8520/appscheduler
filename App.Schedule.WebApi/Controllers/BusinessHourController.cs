using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessHourController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessHourController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businesshour
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblBusinessHours.ToList();
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
                    var model = _db.tblBusinessHours.Find(id);
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
        public IHttpActionResult Post([FromBody]BusinessHourViewModel model)
        {
            try
            {
                if (model != null && model.From > DateTime.Now)
                {
                    var businessHour = new tblBusinessHour()
                    {
                        WeekDayId = model.WeekDayId,
                        IsStartDay = model.IsStartDay,
                        IsHoliday = model.IsHoliday,
                        From = model.From,
                        To = model.To,
                        IsSplit1 = model.IsSplit1,
                        FromSplit1 = model.FromSplit1,
                        ToSplit1 = model.ToSplit1,
                        IsSplit2 = model.IsSplit2,
                        FromSplit2 = model.FromSplit2,
                        ToSplit2 = model.ToSplit2,
                        ServiceLocationId = model.ServiceLocationId
                    };
                    _db.tblBusinessHours.Add(businessHour);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = businessHour });
                    else
                        return Ok(new { status = false, data = "There was a problem." });
                }
                else
                {
                    return Ok(new { status = false, data = "Please enter a valid information." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // PUT: api/businesshour/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessHourViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var businessHour = _db.tblBusinessHours.Find(id);
                    if (businessHour != null)
                    {
                        businessHour.WeekDayId = model.WeekDayId;
                        businessHour.IsStartDay = model.IsStartDay;
                        businessHour.IsHoliday = model.IsHoliday;
                        businessHour.From = model.From;
                        businessHour.To = model.To;
                        businessHour.IsSplit1 = model.IsSplit1;
                        businessHour.FromSplit1 = model.FromSplit1;
                        businessHour.ToSplit1 = model.ToSplit1;
                        businessHour.IsSplit2 = model.IsSplit2;
                        businessHour.FromSplit2 = model.FromSplit2;
                        businessHour.ToSplit2 = model.ToSplit2;
                        businessHour.ServiceLocationId = model.ServiceLocationId;

                        _db.Entry(businessHour).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessHour });
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

        // DELETE: api/businesshour/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var businessHour = _db.tblBusinessHours.Find(id);
                    if (businessHour != null)
                    {
                        _db.tblBusinessHours.Remove(businessHour);
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
