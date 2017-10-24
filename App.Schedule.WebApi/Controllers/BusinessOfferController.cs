using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;


namespace App.Schedule.WebApi.Controllers
{
    public class BusinessOfferController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessOfferController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/businessoffer
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblBusinessOffers.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/businessoffer/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessOffers.Find(id);
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

        // POST: api/businessoffer
        public IHttpActionResult Post([FromBody]BusinessOfferViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var businessOffer = new tblBusinessOffer()
                    {
                        BusinessEmployeeId = model.BusinessEmployeeId,
                        Code = model.Code,
                        Created = model.Created,
                        Description = model.Description,
                        IsActive = model.IsActive,
                        Name = model.Name,
                        ValidFrom = model.ValidFrom,
                        ValidTo = model.ValidTo
                    };
                    _db.tblBusinessOffers.Add(businessOffer);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = businessOffer });
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

        // PUT: api/businessoffer/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessOfferViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    if (model != null)
                    {
                        var businessOffer = _db.tblBusinessOffers.Find(id);
                        if (businessOffer != null)
                        {
                            businessOffer.BusinessEmployeeId = model.BusinessEmployeeId;
                            businessOffer.Code = model.Code;
                            businessOffer.Created = model.Created;
                            businessOffer.Description = model.Description;
                            businessOffer.IsActive = model.IsActive;
                            businessOffer.Name = model.Name;
                            businessOffer.ValidFrom = model.ValidFrom;
                            businessOffer.ValidTo = model.ValidTo;

                            _db.Entry(businessOffer).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = businessOffer });
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

        // DELETE: api/businessoffer/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var businessOffer = _db.tblBusinessOffers.Find(id);
                    if (businessOffer != null)
                    {
                        businessOffer.IsActive = !businessOffer.IsActive;
                        _db.Entry(businessOffer).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessOffer });
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
