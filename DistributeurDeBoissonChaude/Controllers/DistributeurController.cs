using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Services;
using HotDrinkDistributor.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DistributeurDeBoissonChaude.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributeurController : ControllerBase
    {
        private readonly IDistributeurService _distributeurService;
        public DistributeurController(IDistributeurService distributeurService) => _distributeurService = distributeurService;

        [HttpGet("{id}")]
        public IActionResult GetRecipe(int id) => Ok(_distributeurService.GetRecipe(id));

        [HttpGet]
        public IActionResult GetAllRecipes() => Ok(_distributeurService.GetAllRecipes());

        [HttpPost]
        public IActionResult AddRecipe([FromBody] RecetteInfra recetteInfra)
        {
            try
            {
                Recette recette = RecetteExtensions.ToDomainRecipe(recetteInfra);
                _distributeurService.AddRecipe(recette);
                return CreatedAtAction(nameof(GetRecipe), recette);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRcipe(int id)
        {
            try
            {
                _distributeurService.DeleteRecipe(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        public IActionResult UpdateRecipe([FromBody] RecetteInfra recetteInfra)
        {
            try
            {
                Recette recette = RecetteExtensions.ToDomainRecipe(recetteInfra);
                _distributeurService.UpdateRecipe(recette);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
