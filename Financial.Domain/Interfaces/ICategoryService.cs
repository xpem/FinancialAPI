using Financial.Models.Req;
using Financial.Models.Responses;

namespace Financial.Domain.Interfaces
{
    public interface ICategoryService
    {
        BaseResponse Create(CategoryReq categoryReq, int uid);
        BaseResponse Get(int uid);
    }
}