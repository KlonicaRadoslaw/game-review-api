using GameReviewApp.Data;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;

namespace GameReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context) 
        {
            _context = context;
        }

        public bool CategoriesExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Game> GetGameByCategory(int categoryId)
        {
            return _context.GamesCategories.Where(g => g.CategoryId == categoryId).Select(x => x.Game).ToList();
        }
    }
}
