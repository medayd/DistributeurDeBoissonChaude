using DistributeurDeBoissonChaude.Api.Models;

namespace HotDrinkDistributor.Infrastructure.Extensions
{
    public static class RecipeExtensions
    {
        public static Recette ToDomainRecipe(this RecetteInfra recipeInfra) =>
            new()
            {
                Id = recipeInfra.Id,
                NomDeRecette = recipeInfra.NomDeRecette,
                Ingredients = recipeInfra.Ingredients.Select(ToDomainIngredient).ToList(),
            };

        private static Ingredient ToDomainIngredient(this IngredientInfra ingredientInfra) =>
           new()
           {
               Product = new Produit
               {
                   Nom = ingredientInfra.Product.Nom,
                   Prix = ingredientInfra.Product.Prix
               },
               Quantity = ingredientInfra.Quantity
           };
    }
}
