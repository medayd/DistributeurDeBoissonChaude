using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Services;
using HotDrinkDistributor.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace DistributeurDeBoissonChaude.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributeurController : ControllerBase
    {
        private readonly IDistributeurService _distributeurService;
        public DistributeurController(IDistributeurService distributeurService) => _distributeurService = distributeurService;

        [HttpGet("{id}")]
        public IActionResult GetReceipe(int id) => Ok(_distributeurService.GetRecipe(id));

        [HttpGet]
        public IActionResult GetAllRecipes() => Ok(_distributeurService.GetAllRecipes());

        [HttpPost]
        public IActionResult AddRecipe([FromBody] RecetteInfra recetteInfra)
        {
            try
            {
                Recette recette = RecipeExtensions.ToDomainRecipe(recetteInfra);
                _distributeurService.AddRecipe(recette);
                return CreatedAtAction(nameof(GetReceipe), recette);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
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
        public IActionResult UpdateReceipe([FromBody] RecetteInfra recetteInfra)
        {
            try
            {
                Recette recette = RecipeExtensions.ToDomainRecipe(recetteInfra);
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
