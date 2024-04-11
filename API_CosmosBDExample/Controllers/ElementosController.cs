using API_CosmosBDExample.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_CosmosBDExample.Controllers
{
    [EnableCors("MiPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ElementosController : ControllerBase
    {

        private readonly ICosmosDbService _cosmosDbService;

        public ElementosController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }


        //Parte del GET api/Elementos

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT * FROM c "));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _cosmosDbService.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Elementos item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new {id=item.Id},item);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] Elementos item)
        {
            await _cosmosDbService.UpdateAsync(item.Id,item);
            return NoContent();

        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(string id)
        {
            await _cosmosDbService.DeleteAsync(id);
            return NoContent();
        }


      


    }
}
