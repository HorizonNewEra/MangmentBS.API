using AutoMapper;
using MangmentBS.Core;
using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Building;
using MangmentBS.Core.Params.Flat;
using MangmentBS.Core.Services.Interfaces;
using MangmentBS.Core.Specifications;
using MangmentBS.Core.Specifications.Buildings;
using MangmentBS.Core.Specifications.Flats;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Services.Services.BuildingServices
{
    public class BuildingService : IBuildingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public BuildingService(IUnitOfWork _unitOfWork,IMapper _mapper, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            configuration = _configuration;
        }
        public async Task<PaginationResponse<BuildingTableView>> GetAllBuildingTableViewAsync(BuildingSpecificationsParams Params)
        {
            var buildings = await unitOfWork.Repository<Building, int>().GetAllAsync(new BuildingSpecifications(Params));
            var count = await unitOfWork.Repository<Building, int>().CountAsync(new BuildingSpecifications(Params));
            var maped= mapper.Map<IEnumerable<BuildingTableView>>(buildings);
            return new PaginationResponse<BuildingTableView>(Params.PageSize, Params.PageIndex,count, maped);
        }
        public async Task<BuildingTableView> GetBuildingTableViewByIdAsync(int Id)
        {
            var building = await unitOfWork.Repository<Building, int>().GetByIdAsync(new BuildingSpecifications(Id));
            var maped = mapper.Map<BuildingTableView>(building);
            return maped;
        }
        public async Task<BuildingDetailsView> GetBuildingDetails(int Id)
        {
            var building = await unitOfWork.Repository<Building, int>().GetByIdAsync(new BuildingSpecifications(Id));
            if (building == null) return null;
            var flatviews = new List<BuildingFlatView>();
            var flats=await unitOfWork.Repository<Flat, int>().GetAllAsync(new FlatSpecifications(new FlatSpecificationsParams() { BuildingId=Id}));
            if (flats.Count() > 0)
                foreach (var flat in flats)
                {
                    flatviews.Add(new BuildingFlatView()
                    {
                        Id = flat.Id,
                        ClientName = flat.Client?.Name ?? "لا يوجد عميل",
                        FlatNumber = flat.FlatNumber,
                        StanderdPrice = flat.StanderdPrice,
                        Floor = flat.Floor,
                        Size = flat.Size,
                        SellingPrice = flat.SellingPrice ?? 0,
                        FlatContractImage = flat.FlatContractImages.Select(k => $"{configuration["BaseUrl"]}/{k.ImageLink}").ToList()
                    });
                }
            return new BuildingDetailsView()
            {
                Id = Id,
                Description = building.Description,
                Name = building.Name,
                Location = building.Location,
                Size = building.Size,
                LandPrice = building.LandPrice,
                FlatCount = flatviews.Count(),
                ConstractionFees = building.ConstractionFees,
                BuildingContractImages = building.BuildingContractImages.Select(p => $"{configuration["BaseUrl"]}/{p.ImageLink}").ToList(),
                Flats = flatviews.OrderBy(p => p.FlatNumber).ToList()
            };
        }
        public async Task<ErrorResponce> AddBuilding(AddBuildingView view)
        {
            if (view.Name == null) return new ErrorResponce("400", "اسم العقار مطلوب");
            if (view.Location == null) return new ErrorResponce("400", "العنوان مطلوب");
            if (view.Size <= 0) return new ErrorResponce("400", "مساحة العقار مطلوبة ويجب أن تكون أكبر من 0");
            if (view.LandPrice <= 0) return new ErrorResponce("400", "سعر الارض مطلوب ويجب أن يكون أكبر من 0");
            if (view.ConstractionFees <= 0) return new ErrorResponce("400", "مصاريف البناء مطلوبة ويجب أن تكون أكبر من 0");
            await unitOfWork.Repository<Building, int>().AddAsync(new Building()
            {
               ConstractionFees= view.ConstractionFees,
               Description= view.Description,
               Name = view.Name,
               Location = view.Location,
               Size = view.Size,
               LandPrice = view.LandPrice
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce("200", "تم اضافة العقار بنجاح");
            return new ErrorResponce("400", "حدث خطأ أثناء إضافة العقار، يرجى المحاولة مرة أخرى");
        }
        public async Task<ErrorResponce> EditBuilding(EditBuildingView view)
        {
            if (view.Id == 0) return new ErrorResponce("400", "رقم العقار مطلوب");
            var buildings = await unitOfWork.Repository<Building, int>().GetAllAsync();
            var building = buildings.FirstOrDefault(p => p.Id == view.Id);
            if (building == null) return new ErrorResponce("404", "هذا العقار غير موجود");
            unitOfWork.Repository<Building, int>().Update(new Building()
            {
                Id= view.Id,
               ConstractionFees= view.ConstractionFees??building.ConstractionFees,
               Description= view.Description??building.Description,
               Name = view.Name??building.Name,
               Location = view.Location??building.Location,
               Size = view.Size ?? building.Size,
               LandPrice = view.LandPrice ?? building.LandPrice
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce("200", "تم تعديل العقار بنجاح");
            return new ErrorResponce("400", "حدث خطأ أثناء تعديل العقار، يرجى المحاولة مرة أخرى");
        }
    }
}
