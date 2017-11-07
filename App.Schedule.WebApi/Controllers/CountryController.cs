using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using App.Schedule.Context;
using App.Schedule.Domains;
using App.Schedule.Domains.ViewModel;

namespace App.Schedule.WebApi.Controllers
{
    public class CountryController : ApiController
    {
        private readonly AppScheduleDbContext _db;

        public CountryController()
        {
            _db = new AppScheduleDbContext();
        }

        // GET: api/country
        public IHttpActionResult Get()
        {
            try
            {
                var model = _db.tblCountries.ToList();
                return Ok(new { status = true, data = model });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // GET: api/country/5
        public IHttpActionResult Get(long? id)
        {
            var result = new ResponseViewModel<CountryViewModel>();
            try
            {
                if (!id.HasValue)
                {
                    result.Status = false;
                    result.Message = "Please provide a valid id.";
                }
                else
                {
                    var model = _db.tblCountries.Find(id);
                    if (model != null)
                    {
                        result.Status = true;
                        result.Data = new CountryViewModel()
                        {
                            AdministratorId = model.AdministratorId,
                            CurrencyCode = model.CurrencyCode,
                            CurrencyName = model.CurrencyName,
                            Id = model.Id,
                            ISO = model.ISO,
                            ISO3 = model.ISO3,
                            Name = model.Name,
                            PhoneCode = model.PhoneCode
                        };
                    }
                    else
                    {
                        result.Status = false;
                        result.Message = "Country not found.";
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

        // POST: api/country
        public IHttpActionResult Post([FromBody]CountryViewModel model)
        {
            var result = new ResponseViewModel<CountryViewModel>();
            try
            {
                if (!model.PhoneCode.HasValue)
                    model.PhoneCode = 0;

                var country = new tblCountry()
                {
                    Name = model.Name,
                    CurrencyCode = model.CurrencyCode,
                    CurrencyName = model.CurrencyName,
                    ISO = model.ISO,
                    ISO3 = model.ISO3,
                    PhoneCode = model.PhoneCode.Value,
                    AdministratorId = model.AdministratorId,
                };
                _db.tblCountries.Add(country);
                var response = _db.SaveChanges();
                if (response > 0)
                {
                    model.Id = country.Id;
                    result.Status = true;
                    result.Message = "Successfully added";
                    result.Data = model;
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
                result.Message = "ex: There was a problem. Please try again later.";
            }
            return Ok(result);
        }

        // PUT: api/country/5
        public IHttpActionResult Put(long? id, [FromBody]CountryViewModel model)
        {
            try
            {
                if (!id.HasValue)
                    return Ok(new { status = false, data = "Please provide a valid ID." });
                else
                {
                    var country = _db.tblCountries.Find(id);
                    if (country != null)
                    {
                        country.Name = model.Name;
                        country.ISO = model.ISO;
                        country.ISO3 = model.ISO3;
                        country.PhoneCode = model.PhoneCode.Value;
                        country.CurrencyCode = model.CurrencyCode;
                        country.CurrencyName = model.CurrencyName;

                        _db.Entry(country).State = EntityState.Modified;
                        var response = _db.SaveChanges();
                        if (response > 0)
                            return Ok(new { status = true, data = country });
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

        // DELETE: api/country/5
        public IHttpActionResult Delete(int? id)
        {
            var result = new ResponseViewModel<string>();
            try
            {
                if (!id.HasValue)
                {
                    result.Status = false;
                    result.Message = "Please provide a valid country id.";
                }
                else
                {
                    var country = _db.tblCountries.Find(id);
                    if (country != null)
                    {
                        _db.tblCountries.Remove(country);
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
                        result.Message = "Please provide a valid country Id.";
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
