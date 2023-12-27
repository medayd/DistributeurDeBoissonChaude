using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Repository;

namespace DistributeurDeBoissonChaude.Api.Services
{
    public class DistributeurService : IDistributeurService
    {
        private const decimal _marge = 1.3m;
        private readonly IDistributeurRepository _distributeurRepository;
        public DistributeurService(IDistributeurRepository distributeurRepository)
        {
            _distributeurRepository = distributeurRepository;
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
        public List<Recette> GetAllRecipes() => _distributeurRepository.GetAllRecipes();

        public Recette GetRecipe(int id) => _distributeurRepository.GetRecipe(id)
                            ?? throw new InvalidOperationException("Recipe Not Found");

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
    }
}
