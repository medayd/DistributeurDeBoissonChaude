namespace DistributeurDeBoissonChaude.Api.Models
{
    public class RecetteInfra
    {
        public int Id { get; set; }
        public string NomDeRecette { get; set; }
        public List<IngredientInfra>? Ingredients { get; set; }
        public decimal Prix { get; set; }
    }

    public class IngredientInfra
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProduitInfra? Product { get; set; }
    }
    public class ProduitInfra
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
    }
}
