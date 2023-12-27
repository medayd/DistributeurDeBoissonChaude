namespace DistributeurDeBoissonChaude.Api.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Produit Product { get; set; }
    }
}