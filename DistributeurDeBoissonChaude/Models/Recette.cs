namespace DistributeurDeBoissonChaude.Api.Models
{
    public class Recette
    {
        public int Id { get; set; }
        public string NomDeRecette { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public decimal Prix => Ingredients?.Sum(ingredient => (ingredient.Product?.Prix ?? 0) * ingredient.Quantity * 1.3m) ?? 0;
    }
}
