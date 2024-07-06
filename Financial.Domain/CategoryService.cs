using Financial.Domain.Interfaces;
using Financial.Infra.Repos.Interfaces;
using Financial.Models.DTOs;
using Financial.Models.Req;
using Financial.Models.Responses;
using static Financial.Models.TransactionTypes;

namespace Financial.Domain
{
    public class CategoryService(ICategoryRepository categoryRepos) : ICategoryService
    {
        public BaseResponse Get(int uid)
        {
            List<Category>? categories = categoryRepos.Get(uid);
            List<CategoryRes> categoriesRes = [];

            if (categories != null && categories.Count > 0)
                foreach (Category category in categories)
                    categoriesRes.Add(
                        new()
                        {
                            Id = category.Id,
                            Name = category.Name,
                            Color = category.Color,
                            SystemDefault = category.SystemDefault,
                            Type = (int)category.Type,
                        });

            return new BaseResponse(categoriesRes);
        }

        public BaseResponse Create(CategoryReq categoryReq, int uid)
        {
            try
            {
                string? validateError = categoryReq.Validate();
                if (!string.IsNullOrEmpty(validateError)) return new BaseResponse(null, validateError);

                Category category = new()
                {
                    Name = categoryReq.Name,
                    Color = categoryReq.Color,
                    CreatedAt = DateTime.Now,
                    SystemDefault = false,
                    UserId = uid,
                    Type = (TransactionType)categoryReq.Type,
                };

                string? existingItemMsg = ValidateExistingCategory(category);

                if (existingItemMsg != null)
                    return new BaseResponse(null, existingItemMsg);

                int respExec = categoryRepos.Create(category);

                if (respExec == 1)
                {
                    CategoryRes resCategory = new()
                    {
                        Name = category.Name,
                        Color = category.Color,
                        SystemDefault = category.SystemDefault,
                        Id = category.Id,
                        Type = (int)category.Type
                    };
                    return new BaseResponse(resCategory);
                }
                else
                    return new BaseResponse(null, "Não foi possivel adicionar.");
            }
            catch { throw; }
        }

        protected string? ValidateExistingCategory(Category Category, int? id = null)
        {
            Category? respCategory = categoryRepos.GetByName(Category.UserId.Value, Category.Name);

            if ((respCategory is not null) && ((id is not null && respCategory.Id != id) || (id is null)))
                return "A Category with this Name has already been added.";

            return null;
        }
    }
}
