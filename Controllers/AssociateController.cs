using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repopattern.Model;
using Repopattern.Repository;

namespace Repopattern.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssociateController : ControllerBase
    {
        private readonly IAssociateRepository _associateRepository;
        public AssociateController(IAssociateRepository associateRepository)
        {
            _associateRepository = associateRepository;
        }
        [EnableCors("addcors")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var _list= await _associateRepository.GetAll();
            return Ok(_list);

        }
       
        [HttpGet("{Id}")]
        public async Task<IActionResult> Getbycode(string Id)
        {
            var _obj = await _associateRepository.Get(Id);
            if(_obj == null)
            {
               return NotFound();
            }
            return Ok(_obj);

        }

        [HttpPost]
        public async Task<IActionResult> Create(Associate associate)
        {
            await _associateRepository.Create(associate);
            return Ok();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(string Id,Associate associate)
        {
            if(Id!=associate.Id)
            {
                return BadRequest();
            }
            await _associateRepository.Update(associate);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var _resp=await _associateRepository.Delete(Id);
            if (!_resp)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
