using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Repository;

namespace DistributeurDeBoissonChaude.Api.Services
{
    public interface IDistributeurService
    {
        public Recette GetRecipe(int id);
        public List<Recette> GetAllRecipes();
        public void AddRecipe(Recette rec);
        public void UpdateRecipe(Recette rec);
        public void DeleteRecipe(int id);

    }
}
