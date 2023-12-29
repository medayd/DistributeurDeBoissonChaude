namespace DistributeurDeBoissonChaude.Api.Models
{
    public class Recette
    {
        public int Id { get; set; }
        public string NomDeRecette { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
    }
}
