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
            var result = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    result.Status = false;
                    result.Message = "Timezone id is required.";
                }
                else
                {
                    var model = _db.tblTimezones.Find(id);
                    if (model != null)
                    {
                        result.Status = true;
                        result.Data = new TimezoneViewModel()
                        {
                            AdministratorId = model.AdministratorId,
                            CountryId = model.CountryId,
                            Id = model.Id,
                            IsDST = model.IsDST,
                            Title = model.Title,
                            UtcOffset = model.UtcOffset
                        };
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "Please provide a valid timezone id";
                    }
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Ok(result);
        }

        // POST: api/timezone
        public IHttpActionResult Post([FromBody]TimezoneViewModel model)
        {
            var result = new ResponseViewModel<TimezoneViewModel>();
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
                {
                    result.Status = true;
                    result.Data = new TimezoneViewModel()
                    {
                        AdministratorId = model.AdministratorId,
                        CountryId = model.CountryId,
                        Id = model.Id,
                        IsDST = model.IsDST,
                        Title = model.Title,
                        UtcOffset = model.UtcOffset
                    };
                }
                else
                {
                    result.Status = false;
                    result.Message = "There was a problem. Please try again later.";
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Ok(result);
        }

        // PUT: api/timezone/5
        public IHttpActionResult Put(long? id, [FromBody]TimezoneViewModel model)
        {
            var result = new ResponseViewModel<TimezoneViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    result.Status = false;
                    result.Message = "Timezone id is required.";
                }
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
                        {
                            result.Status = true;
                            model.Id = timeZone.Id;
                            result.Data = model;
                        }
                        else
                        {
                            result.Status = false;
                            result.Message = "There was a problem. Please try again later.";
                        }
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "There was a problem. Please try again later.";
                    }
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "There was a problem. Please try again later.";
            }
            return Ok(result);
        }

        // DELETE: api/timeZone/5
        public IHttpActionResult Delete(int? id)
        {
            var result = new ResponseViewModel<string>();
            try
            {
                if (!id.HasValue)
                {
                    result.Status = false;
                    result.Message = "Timezone id is required.";
                }
                else
                {
                    var timezone = _db.tblTimezones.Find(id);
                    if (timezone != null)
                    {
                        _db.tblTimezones.Remove(timezone);
                        var response = _db.SaveChanges();
                        if (response > 0)
                        {
                            result.Status = true;
                            result.Message = "Successfully removed.";
                        }
                        else
                        {
                            result.Status = false;
                            result.Message = "There was a problem. Please try again later.";
                        }
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "Please provide a valid timezone Id.";
                    }
                }
            }
            catch
            {
                result.Status = false;
                result.Message = "ex: There was a problem. Please try again later.";
            }
            return Ok(result);
        }
    }
}