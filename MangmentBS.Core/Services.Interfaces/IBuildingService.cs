using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Services.Interfaces
{
    public interface IBuildingService
    {
        Task<PaginationResponse<BuildingTableView>> GetAllBuildingTableViewAsync(BuildingSpecificationsParams buildingSpecificationsParams);
        Task<BuildingTableView> GetBuildingTableViewByIdAsync(int id);
        Task<BuildingDetailsView> GetBuildingDetails(int Id);
        Task<ErrorResponce> AddBuilding(AddBuildingView view);
        Task<ErrorResponce> EditBuilding(EditBuildingView view);
    }
}
