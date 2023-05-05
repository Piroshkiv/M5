using lb1.Dtos.Responses;
using lb1.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb1.Services.Abstractions
{
    public interface IResourceService
    {
        Task<ResourceDto> GetResourceById(int id);
        Task<BasePageResponse<ResourceDto>> GetResourcePage(int id);
    }
}
