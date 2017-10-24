using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace App.Schedule.WebApi.Controllers
{
    public class BusinessCustomerController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public BusinessCustomerController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/BusinessCustomer
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblBusinessCustomers.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/BusinessCustomer/5
        public IHttpActionResult Get(long? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide valid ID." });
                else
                {
                    var model = _db.tblBusinessCustomers.Find(id);
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

        // POST: api/BusinessCustomer
        public IHttpActionResult Post([FromBody]BusinessCustomerViewMdoel model)
        {
            try
            {
                if (model != null)
                {
                    var businessCustomer = new tblBusinessCustomer()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        ProfilePicture = model.ProfilePicture,
                        Email = model.Email,
                        StdCode = model.StdCode,
                        PhoneNumber = model.PhoneNumber,
                        Add1 = model.Add1,
                        Add2 = model.Add2, 
                        City = model.City,
                        State = model.State,
                        Zip = model.Zip,
                        LoginId = model.LoginId,
                        Password = model.Password,
                        IsActive = model.IsActive,
                        Created = model.Created,
                        TimezoneId = model.TimezoneId,
                        ServiceLocationId = model.ServiceLocationId
                    };
                    _db.tblBusinessCustomers.Add(businessCustomer);
                    var response = _db.SaveChanges();
                    if (response > 0)
                        return Ok(new { status = true, data = businessCustomer });
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

        // PUT: api/BusinessCustomer/5
        public IHttpActionResult Put(long? id, [FromBody]BusinessCustomerViewMdoel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    if (model != null)
                    {
                        var businessCustomer = _db.tblBusinessCustomers.Find(id);
                        if (businessCustomer != null)
                        {
                            businessCustomer.FirstName = model.FirstName;
                            businessCustomer.LastName = model.LastName;
                            businessCustomer.ProfilePicture = model.ProfilePicture;
                            businessCustomer.Email = model.Email;
                            businessCustomer.StdCode = model.StdCode;
                            businessCustomer.PhoneNumber = model.PhoneNumber;
                            businessCustomer.Add1 = model.Add1;
                            businessCustomer.Add2 = model.Add2;
                            businessCustomer.City = model.City;
                            businessCustomer.State = model.State;
                            businessCustomer.Zip = model.Zip;
                            businessCustomer.LoginId = model.LoginId;
                            businessCustomer.Password = model.Password;
                            businessCustomer.IsActive = model.IsActive;
                            businessCustomer.Created = model.Created;
                            businessCustomer.TimezoneId = model.TimezoneId;
                            businessCustomer.ServiceLocationId = model.ServiceLocationId;

                            _db.Entry(businessCustomer).State = EntityState.Modified;
                            var response = _db.SaveChanges();
                            if (response > 0)
                                return Ok(new { status = true, data = businessCustomer });
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

        // DELETE: api/BusinessCustomer/5
        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var businessCustomer = _db.tblBusinessCustomers.Find(id);
                    if (businessCustomer != null)
                    {
                        businessCustomer.IsActive = !businessCustomer.IsActive;
                        _db.Entry(businessCustomer).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = businessCustomer });
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
