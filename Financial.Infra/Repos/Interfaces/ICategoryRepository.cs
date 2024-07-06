using Financial.Models.DTOs;

namespace Financial.Infra.Repos.Interfaces
{
    public interface ICategoryRepository
    {
        int Create(Category category);
        int Delete(Category category);
        List<Category>? Get(int uid);
        Category? GetById(int uid, int id);
        Category? GetByName(int uid, string name);
        int Update(Category category);
    }
}