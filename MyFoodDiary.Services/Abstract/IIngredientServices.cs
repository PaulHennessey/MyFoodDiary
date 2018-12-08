using System.Collections.Generic;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Services.Abstract
{
    public interface IIngredientServices
    {
        //IEnumerable<Ingredient> GetIngredients(int userId);
        //Ingredient GetIngredient(int id);
        void CreateIngredient(string code, int mealId);
//        void UpdateIngredient(Ingredient ingredient);
        //void DeleteProduct(string code);
    }
}
