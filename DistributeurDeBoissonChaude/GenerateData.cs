using DistributeurDeBoissonChaude.Api.Models;

namespace DistributeurDeBoissonChaude.Api
{
    public static class GenerateData
    {
        public static List<Recette> InitializeData()
        {
            var cafe = new Produit { Id = 1, Nom = "Café", Prix = 1.0m };
            var sucre = new Produit { Id = 2, Nom = "Sucre", Prix = 0.1m };
            var creme = new Produit { Id = 3, Nom = "Crème", Prix = 0.5m };
            var the = new Produit { Id = 4, Nom = "Thé", Prix = 2.0m };
            var eau = new Produit { Id = 5, Nom = "Eau", Prix = 0.2m };
            var chocolat = new Produit { Id = 6, Nom = "Chocolat", Prix = 1.0m };
            var lait = new Produit { Id = 7, Nom = "Lait", Prix = 0.4m };

            var expresso = new Recette
            {
                Id = 1,
                NomDeRecette = "Expresso",
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Product = cafe, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 1 }
                }
            };
            var allonge = new Recette
            {
                Id = 2,
                NomDeRecette = "Allongé",
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Product = cafe, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 2 }
                }
            };
            var cappuccino = new Recette
            {
                Id = 3,
                NomDeRecette = "Capuccino",
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Product = cafe, Quantity = 1 },
                    new Ingredient { Product = chocolat, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 1 },
                    new Ingredient { Product = creme, Quantity = 1 }
                }
            };
            var chocolate = new Recette
            {
                Id = 4,
                NomDeRecette = "Chocolat",
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Product = chocolat, Quantity = 3 },
                    new Ingredient { Product = lait, Quantity = 2 },
                    new Ingredient { Product = eau, Quantity = 1 },
                    new Ingredient { Product = sucre, Quantity = 1 }
                }
            };
            var tea = new Recette
            {
                Id = 5,
                NomDeRecette = "Tea",
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Product = the, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 2 }
                }
            };

            return new List<Recette> { expresso, allonge, cappuccino, chocolate, tea };

        }
    }
}
