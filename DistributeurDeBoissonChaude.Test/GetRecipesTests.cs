using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Repository;
using DistributeurDeBoissonChaude.Api.Services;
using FakeItEasy;
using Microsoft.Extensions.Options;

namespace DistributeurDeBoissonChaude.Api.Test
{
    public class GetRecipesTests
    {
        private IDistributeurRepository _mockRepository;
        private IDistributeurService _DistributeurService;
        private IOptions<MargeConfig> _mockConfig;
        private Produit cafe = new Produit { Id = 1, Nom = "Café", Prix = 1.0m };
        private Produit eau = new Produit { Id = 5, Nom = "Eau", Prix = 0.2m };
        private Recette allonge;

        [SetUp]
        public void Setup()
        {
            _mockRepository = A.Fake<IDistributeurRepository>();
            _mockConfig = A.Fake<IOptions<MargeConfig>>();
            _DistributeurService = new DistributeurService(_mockRepository, _mockConfig);
            allonge = new Recette
            {
                Id = 10,
                NomDeRecette = "Allongé",
                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Product = cafe, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 2 }
                }
            };

        }

        [Test]
        public void GetRecipe_WithValidId_ReturnsRecipe()
        {
            // Arrange
            int id = 10;
            A.CallTo(() => _mockRepository.GetRecipe(id)).Returns(allonge);
            A.CallTo(() => _mockConfig.Value.Marge).Returns(1.3m);
            // Act && Assert
            var result = _DistributeurService.GetRecipe(id);
            Assert.IsNotNull(result);
            Assert.That(result.Prix, Is.EqualTo(1.82m));
        }

        [Test]
        public void GetRecipe_WithInvalidId_Returns_InvalidOperationException()
        {
            // Arrange
            int id = 10;
            var recette = new Recette { Id = id, NomDeRecette = "Expresso", Ingredients = new List<Ingredient> { new Ingredient() } };
            A.CallTo(() => _mockRepository.GetRecipe(id)).Returns(null);
            A.CallTo(() => _mockConfig.Value.Marge).Returns(1.3m);
            // Act && Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _DistributeurService.GetRecipe(id));
            Assert.That(ex.Message, Is.EqualTo("Recipe Not Found"));
            A.CallTo(() => _mockRepository.UpdateRecipe(recette)).MustNotHaveHappened();
        }
    }
}