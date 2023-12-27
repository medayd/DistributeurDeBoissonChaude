using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Repository;
using DistributeurDeBoissonChaude.Api.Services;
using FakeItEasy;
using Moq;
namespace DistributeurDeBoissonChaude.Api.Test
{
    public class DeleteRecipeTests
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
        public void DeleteRecipe_Existant_Succeeds()
        {
            // Arrange
            var recipeToDelete = new Recette {Id = 10, NomDeRecette = "NewRecipe", Ingredients = new List<Ingredient> { new Ingredient() } };
            A.CallTo(() => _mockRepository.GetRecipe(10)).Returns(recipeToDelete);

            // Act && Assert
            Assert.DoesNotThrow(() => _DistributeurService.DeleteRecipe(recipeToDelete.Id));
            A.CallTo(() => _mockRepository.DeleteRecipe(recipeToDelete.Id)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void DeleteRecipe_Non_Existant_Failss()
        {
            // Arrange
            var recipeToDelete = new Recette { Id = 10, NomDeRecette = "NewRecipe", Ingredients = new List<Ingredient> { new Ingredient() } };
            A.CallTo(() => _mockRepository.GetRecipe(10)).Returns(null);

            // Act && Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _DistributeurService.DeleteRecipe(recipeToDelete.Id));
            Assert.That(exception.Message, Is.EqualTo("The id doesn't match any recipe"));
            A.CallTo(() => _mockRepository.DeleteRecipe(recipeToDelete.Id)).MustNotHaveHappened();
        }
    }
}