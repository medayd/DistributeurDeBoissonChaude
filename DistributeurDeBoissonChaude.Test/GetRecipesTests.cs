using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Repository;
using DistributeurDeBoissonChaude.Api.Services;
using FakeItEasy;
using Moq;
namespace DistributeurDeBoissonChaude.Api.Test
{
    public class GetRecipesTests
    {
        private IDistributeurRepository _mockRepository;
        private IDistributeurService _DistributeurService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = A.Fake<IDistributeurRepository>();
            _DistributeurService = new DistributeurService(_mockRepository);
        }

        [Test]
        public void GetRecipe_WithValidId_ReturnsRecipe()
        {
            // Arrange
            int id = 10;
            var recipe = new Recette { Id = id, NomDeRecette = "NewRecipe", Ingredients = new List<Ingredient> { new Ingredient() } };
            A.CallTo(() => _mockRepository.GetRecipe(id)).Returns(recipe);
            // Act && Assert
            var result = _DistributeurService.GetRecipe(id);
            Assert.IsNotNull(result);
            Assert.That(recipe, Is.EqualTo(result));
        }

        [Test]
        public void GetRecipe_WithInvalidId_Returns_InvalidOperationException()
        {
            // Arrange
            int id = 10;
            var recette = new Recette { Id = id, NomDeRecette = "Expresso", Ingredients = new List<Ingredient> { new Ingredient() } };
            A.CallTo(() => _mockRepository.GetRecipe(id)).Returns(null);

            // Act && Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _DistributeurService.GetRecipe(id));
            Assert.That(ex.Message, Is.EqualTo("Recipe Not Found"));
            A.CallTo(() => _mockRepository.UpdateRecipe(recette)).MustNotHaveHappened();
        }
    }
}