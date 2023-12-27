using DistributeurDeBoissonChaude.Api.Models;

namespace DistributeurDeBoissonChaude.Api.Repository
{
    public interface IDistributeurRepository
    {
        public Recette GetRecipe(int id) ;
        public List<Recette> GetAllRecipes() ;
        public void AddRecipe(Recette rec);
        public void UpdateRecipe(Recette rec);

        public void DeleteRecipe(int id);

    }
}
