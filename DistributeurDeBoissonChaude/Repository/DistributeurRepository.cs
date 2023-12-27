using DistributeurDeBoissonChaude.Api.Models;

namespace DistributeurDeBoissonChaude.Api.Repository
{
    public class DistributeurRepository : IDistributeurRepository
    {
        private List<Recette> _recettes;

        public DistributeurRepository()
        {
            _recettes = GenerateData.InitializeData();
        }

        public Recette GetRecipe(int id) => _recettes.FirstOrDefault(r => r.Id == id);
        public List<Recette> GetAllRecipes() => _recettes.ToList();
        public void AddRecipe(Recette rec) => _recettes.Add(rec);
        public void UpdateRecipe(Recette rec)
        {
            var recetteAModifier = GetRecipe(rec.Id);
            recetteAModifier.NomDeRecette = rec.NomDeRecette;
            recetteAModifier.Ingredients = rec.Ingredients;
        }

        public void DeleteRecipe(int id) => _recettes.Remove(GetRecipe(id));
    }
}
