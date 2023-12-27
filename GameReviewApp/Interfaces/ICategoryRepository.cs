using GameReviewApp.Models;

namespace GameReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Game> GetGameByCategory(int categoryId);
        bool CategoriesExists(int id);
    }
}
