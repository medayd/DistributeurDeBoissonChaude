using DistributeurDeBoissonChaude.Api.Models;
using DistributeurDeBoissonChaude.Api.Repository;
using DistributeurDeBoissonChaude.Api.Services;
using FakeItEasy;
using Moq;
namespace DistributeurDeBoissonChaude.Api.Test
{
    public class AddRecipeTests
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
        public void AddRecipe_WithUniqueNameAndIngredients_Succeeds()
        {
            // Arrange
            var newRecipe = new Recette { NomDeRecette = "NewRecipe", Ingredients = new List<Ingredient> { new Ingredient() } };

            // Act && Assert
            Assert.DoesNotThrow(() => _DistributeurService.AddRecipe(newRecipe));
            A.CallTo(() => _mockRepository.AddRecipe(newRecipe)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Add_Recette_Already_Existant_Fail()
        {
            // Arrange
            var newRecipe = new Recette { NomDeRecette = "Expresso", Ingredients = new List<Ingredient> { new Ingredient() } };
            A.CallTo(()=> _mockRepository.GetAllRecipes()).Returns(new List<Recette> { newRecipe});

            // Act && Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _DistributeurService.AddRecipe(newRecipe));
            Assert.That(ex.Message, Is.EqualTo("Recipe name must be unique."));
            A.CallTo(() => _mockRepository.AddRecipe(newRecipe)).MustNotHaveHappened();
        }

        [Test]
        public void Test_Ajout_De_Recette_SansIngredients_Fail()
        {
            // Arrange
            var newRecipe = new Recette { NomDeRecette = "Expresso" };

            // Act && Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _DistributeurService.AddRecipe(newRecipe));
            Assert.That(ex.Message, Is.EqualTo("A recipe must have ingredients."));
            A.CallTo(() => _mockRepository.AddRecipe(newRecipe)).MustNotHaveHappened();
        }



    }
}