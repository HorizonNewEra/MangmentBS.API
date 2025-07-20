using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Dtos.Flat;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Building;
using MangmentBS.Core.Params.Flat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Services.Interfaces
{
    public interface IFlatService
    {
        Task<PaginationResponse<FlatTableView>> GetAllFlatTableViewAsync(FlatSpecificationsParams flatSpecificationsParams);
        Task<FlatTableView> GetFlatTableViewByIdAsync(int id);
        Task<FlatDetailsView> GetFlatDetails(int Id);
        Task<ErrorResponce> AddFlat(AddFlatView view);
        Task<ErrorResponce> EditFlat(EditFlatView view);
    }
}
