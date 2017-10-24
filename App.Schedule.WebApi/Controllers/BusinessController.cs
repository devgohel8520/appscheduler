using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/business
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblBusinesses.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/business/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinesses.Find(id);
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

        // POST: api/business
        public IHttpActionResult Post([FromBody]BusinessViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var business = new tblBusiness()
                    {
                        Name = model.Name,
                        ShortName = model.ShortName,
                        IsInternational = model.IsInternational,
                        FaxNumbers = model.FaxNumbers,
                        PhoneNumbers = model.PhoneNumbers,
                        Logo = model.Logo,
                        Add1 = model.Add1,
                        Add2 = model.Add2,
                        City = model.City,
                        State = model.State,
                        CountryId = model.CountryId,
                        Email = model.Email,
                        Website = model.Website,
                        Created = DateTime.Now.ToUniversalTime(),
                        IsActive = model.IsActive,
                        Zip = model.Zip,
                        MembershipId = model.MembershipId,
                        BusinessCategoryId = model.BusinessCategoryId,
                        TimezoneId = model.TimezoneId
                    };
                    _db.tblBusinesses.Add(business);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = business });
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

        // PUT: api/business/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var business = _db.tblBusinesses.Find(id);
                    if (business != null)
                    {
                        business.Name = model.Name;
                        business.ShortName = model.ShortName;
                        business.IsInternational = model.IsInternational;
                        business.FaxNumbers = model.FaxNumbers;
                        business.PhoneNumbers = model.PhoneNumbers;
                        business.Logo = model.Logo;
                        business.Add1 = model.Add1;
                        business.Add2 = model.Add2;
                        business.City = model.City;
                        business.State = model.State;
                        business.CountryId = model.CountryId;
                        business.Email = model.Email;
                        business.Website = model.Website;
                        business.Created = DateTime.Now.ToUniversalTime();
                        business.IsActive = model.IsActive;
                        business.Zip = model.Zip;
                        business.MembershipId = model.MembershipId;
                        business.BusinessCategoryId = model.BusinessCategoryId;
                        business.TimezoneId = model.TimezoneId;

                        _db.Entry(business).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = business });
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

        // DELETE: api/business/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var business = _db.tblBusinesses.Find(id);
                    if (business != null)
                    {
                        business.IsActive = !business.IsActive;
                        _db.Entry(business).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = business });
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
