using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Company.Common;
using Company.Data.Interface;
using System.Xml.Linq;

namespace Company.API.Controllers
{
   
    [ApiController]   
   [Route("[controller]")]
    public class CompanyDataController : ControllerBase
    {
        private readonly IDbService _db;
        public CompanyDataController(IDbService db) => _db = db;

        // GET: CompanyController/Details/5
        [HttpGet]
        public async Task<IResult> Get() =>
                Results.Ok(await _db.GetAsync<Company.Data.Company, CompanyDto>());

        [HttpGet]
        [Route("GetById")]
        public async Task<IResult> GetById(int id) =>
                Results.Ok(await _db.SingleAsync<Company.Data.Company, CompanyDto>(e =>e.Id.Equals(id)));

        // GET: CompanyController/Create
        [HttpPost]
        public async Task<IResult> Create(CompanyDto companyDto)
        {
            var entity = await _db.AddAsync<Company.Data.Company, CompanyDto>(companyDto);
            if (await _db.SaveChangesAsync())
            {
                return Results.Created($"{entity.Id}", entity);
            }
            else
            {
                return Results.BadRequest($"Couldn't add the {companyDto.CompanyName}");
            }
            
        }



        // GET: CompanyController/Edit/5
        [HttpPut]       
        public async Task<IResult> Edit(CompanyDto companyDto)
        {
            _db.Update<Company.Data.Company, CompanyDto>(companyDto.Id, companyDto);
            if (await _db.SaveChangesAsync())
                return Results.Ok("Updated");
            else
                return Results.BadRequest($"Couldn't update the {companyDto.CompanyName}");
        }



        // GET: CompanyController/Delete/5
        [HttpDelete]
        public async Task<IResult> Delete(int id)
        {
            if (await _db.DeleteAsync<Company.Data.Company>(id)) 
            {
                if (await _db.SaveChangesAsync())
                    return Results.Ok($"Deleted {id}");
                else 
                    return Results.BadRequest($"Couldn't delete the {id}");
            }
                
            else
                return Results.BadRequest($"Couldn't delete the {id}");
        }


    }
}
