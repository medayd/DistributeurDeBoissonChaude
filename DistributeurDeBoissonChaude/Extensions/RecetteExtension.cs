using DistributeurDeBoissonChaude.Api.Models;

namespace HotDrinkDistributor.Infrastructure.Extensions
{
    public static class RecetteExtensions
    {
        public static Recette ToDomainRecipe(this RecetteInfra recipeInfra) =>
            new()
            {
                Id = recipeInfra.Id,
                NomDeRecette = recipeInfra.NomDeRecette,
                Ingredients = recipeInfra.Ingredients?.Select(ToDomainIngredient).ToList() ?? new List<Ingredient>()
            };

        public static RecetteInfra ToInfraRecipe(this Recette recipe) =>
            new()
            {
                Id = recipe.Id,
                NomDeRecette = recipe.NomDeRecette,
                Ingredients = recipe.Ingredients?.Select(ToInfraIngredient).ToList() ?? new List<IngredientInfra>()
            };

        private static Ingredient ToDomainIngredient(this IngredientInfra ingredientInfra) =>
           new()
           {
               Product = new Produit
               {
                   Nom = ingredientInfra.Product.Nom,
                   Prix = ingredientInfra.Product.Prix,
                   Id = ingredientInfra.Product.Id
               },
               Quantity = ingredientInfra.Quantity
           };

        private static IngredientInfra ToInfraIngredient(this Ingredient ingredient) =>
           new()
           {
               Product = ingredient.Product != null ? new ProduitInfra
               {
                   Nom = ingredient.Product.Nom,
                   Prix = ingredient.Product.Prix,
                   Id = ingredient.Product.Id

               } : new ProduitInfra(),
               Quantity = ingredient.Quantity
           };
    }
}
