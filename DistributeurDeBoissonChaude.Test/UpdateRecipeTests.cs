using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Repository;
using DistributeurDeBoissonChaude.Api.Services;
using FakeItEasy;
using Moq;
namespace DistributeurDeBoissonChaude.Api.Test
{
    public class UpdateRecipeTests
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
        public void UpdateRecipe_Existant_Succeeds()
        {
            // Arrange
            var recipe = new Recette { NomDeRecette = "NewRecipe", Ingredients = new List<Ingredient> { new Ingredient() } };
            A.CallTo(() => _mockRepository.GetAllRecipes()).Returns(new List<Recette> { recipe });

            // Act && Assert
            Assert.DoesNotThrow(() => _DistributeurService.UpdateRecipe(recipe));
            A.CallTo(() => _mockRepository.UpdateRecipe(recipe)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void UpdateRecipe_Non_Existant_Fail()
        {
            // Arrange
            var recipe = new Recette { Id = 10, NomDeRecette = "Expresso", Ingredients = new List<Ingredient> { new Ingredient() } };
            A.CallTo(() => _mockRepository.GetRecipe(10)).Returns(null);

            // Act && Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _DistributeurService.GetRecipe(recipe.Id));
            Assert.That(ex.Message, Is.EqualTo("Recipe Not Found"));
        }
    }
}