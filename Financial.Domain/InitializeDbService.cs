using Financial.Infra;
using Financial.Infra.Repos.Interfaces;
using Financial.Models.DTOs;
using Financial.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Domain
{
    public interface IInitializeDbService
    {
        BaseResponse Execute();
    };

    public class InitializeDbService(IInitializeDBRepo initializeDBRepo) : IInitializeDbService
    {
        public BaseResponse Execute()
        {
            initializeDBRepo.CreateInitialValues();
            return new BaseResponse("") { Success = true };
        }
    }
}
