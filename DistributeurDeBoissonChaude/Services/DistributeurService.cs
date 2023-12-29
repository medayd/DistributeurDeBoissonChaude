using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Repository;
using HotDrinkDistributor.Infrastructure.Extensions;
using Microsoft.Extensions.Options;

namespace DistributeurDeBoissonChaude.Api.Services
{
    public class DistributeurService : IDistributeurService
    {
        private const decimal _marge = 1.3m;
        private readonly IDistributeurRepository _distributeurRepository;
        private readonly MargeConfig _config;
        public DistributeurService(IDistributeurRepository distributeurRepository, IOptions<MargeConfig> margeConfiguration)
        {
            _distributeurRepository = distributeurRepository;
            _config = margeConfiguration.Value;
        }

        public void AddRecipe(Recette recipe)
        {
            var allRecipes = GetAllRecipes();
            if (allRecipes.Any(existingRecipe => existingRecipe.NomDeRecette == recipe.NomDeRecette))
            {
                throw new InvalidOperationException("Recipe name must be unique.");
            }
            if (recipe.Ingredients == null || recipe.Ingredients.Count == 0)
            {
                throw new InvalidOperationException("A recipe must have ingredients.");
            }

            recipe.Id = allRecipes.Any() ? allRecipes.Select(r => r.Id).Max() + 1 : 1;

            _distributeurRepository.AddRecipe(recipe);
        }


        public void DeleteRecipe(int id)
        {
            var recette = _distributeurRepository.GetRecipe(id);
            if (recette != null)
            {
                _distributeurRepository.DeleteRecipe(id);
            }
            else
            {
                throw new InvalidOperationException("The id doesn't match any recipe");
            }
        }
        public List<RecetteInfra> GetAllRecipes()
        {
            var listRecettes = _distributeurRepository.GetAllRecipes();
            List<RecetteInfra> recettesInfra = listRecettes.Select(RecetteExtensions.ToInfraRecipe).Select(CalculPrixRecette).ToList();
            return recettesInfra;
        }
        public RecetteInfra GetRecipe(int id)
        {
            Recette recette = _distributeurRepository.GetRecipe(id);
            if (recette == null)
                throw new InvalidOperationException("Recipe Not Found");

            RecetteInfra recetteInfra = RecetteExtensions.ToInfraRecipe(recette);
            CalculPrixRecette(recetteInfra);
            return recetteInfra;
        }
        public void UpdateRecipe(Recette rec)
        {
            var recette = _distributeurRepository.GetRecipe(rec.Id);
            if (recette != null)
            {
                _distributeurRepository.UpdateRecipe(rec);
            }
            else
            {
                throw new InvalidOperationException("The id doesn't match any recipe");
            }

        }

        private RecetteInfra CalculPrixRecette(RecetteInfra recetteInfra)
        {
            recetteInfra.Prix = recetteInfra.Ingredients?.Sum(ingredient => (ingredient.Product?.Prix ?? 0) * ingredient.Quantity * _config.Marge) ?? 0;
            return recetteInfra;
        }
    }
}
