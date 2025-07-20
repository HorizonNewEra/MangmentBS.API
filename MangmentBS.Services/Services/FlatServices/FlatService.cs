using AutoMapper;
using MangmentBS.Core;
using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Dtos.Flat;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Flat;
using MangmentBS.Core.Services.Interfaces;
using MangmentBS.Core.Specifications.Buildings;
using MangmentBS.Core.Specifications.Flats;
using MangmentBS.Core.Specifications.Payments;
using MangmentBS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Services.Services.FlatServices
{
    public class FlatService : IFlatService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public FlatService(IUnitOfWork _unitOfWork,IMapper _mapper,IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            configuration = _configuration;
        }
        public async Task<ErrorResponce> AddFlat(AddFlatView view)
        {
            if (view.FlatNumber == null) return new ErrorResponce("400","رقم الشقة مطلوب");
            if (view.Floor == null) return new ErrorResponce("400", "رقم الطابق مطلوب");
            if (view.StanderdPrice == null) return new ErrorResponce("400", "السعر المبدئ مطلوب");
            if (view.BuildingId == null) return new ErrorResponce("400", "رقم المبنى مطلوب");
            if (view.Size == null) return new ErrorResponce("400", "مساحة الشقة مطلوبة");
            await unitOfWork.Repository<Flat, int>().AddAsync(new Flat()
            {
                BuildingId=view.BuildingId,
                FlatNumber=view.FlatNumber,
                Floor=view.Floor,
                Size=view.Size,
                StanderdPrice = view.StanderdPrice
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce("200", "تم اضافة الشقة بنجاح");
            return new ErrorResponce("400", "حدث خطأ أثناء اضافة الشقة");
        }
        public async Task<ErrorResponce> EditFlat(EditFlatView view)
        {
            if (view.Id == null || view.Id == 0) return new ErrorResponce("400", "رقم الشقة مطلوب");
            var flats = await unitOfWork.Repository<Flat, int>().GetAllAsync();
            var flat = flats.FirstOrDefault(p => p.Id == view.Id);
            if (flat == null) return new ErrorResponce("400", "هذه الشقة غير موجودة");
            unitOfWork.Repository<Flat, int>().Update(new Flat()
            {
                Id = view.Id,
                BuildingId=view.BuildingId??flat.BuildingId,
                FlatNumber=view.FlatNumber?? flat.FlatNumber,
                Floor=view.Floor??flat.Floor,
                Size = view.Size ?? flat.Size,
                StanderdPrice=view.StanderdPrice??flat.Size
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) new ErrorResponce("200", "تم تعديل الشقة بنجاح");
            return new ErrorResponce("400", "حدث خطأ أثناء تعديل الشقة");
        }
        public async Task<PaginationResponse<FlatTableView>> GetAllFlatTableViewAsync(FlatSpecificationsParams Params)
        {
            var flats = await unitOfWork.Repository<Flat, int>().GetAllAsync(new FlatSpecifications(Params));
            if (!flats.Any()) return null;
            var count = await unitOfWork.Repository<Flat, int>().CountAsync(new FlatSpecifications(Params));
            var maped = mapper.Map<IEnumerable<FlatTableView>>(flats);
            return new PaginationResponse<FlatTableView>(Params.PageSize, Params.PageIndex, count, maped);
        }
        public async Task<FlatDetailsView> GetFlatDetails(int Id)
        {
            var flat = await unitOfWork.Repository<Flat, int>().GetByIdAsync(new FlatSpecifications(Id));
            if (flat == null) return null;
            var payment = await unitOfWork.Repository<Payment, int>().GetByIdAsync(new PaymentSpecifications(flat.PaymentId ?? -1));
            var flatInstallmentView = new List<FlatInstallmentView>();
            if(payment!=null)
            foreach (var install in payment.Installments)
            {
                flatInstallmentView.Add(new FlatInstallmentView() {
                    Id = install.Id,
                    Amount = install.Amount,
                    DueDate = install.DueDate,
                    DueDateString=HelperFn.GetDateString(install.DueDate),
                    IsPaid = install.IsPaid,
                    PaidDate = install.PaidDate,
                    PaidDateString= HelperFn.GetStringFromDate(install.PaidDate)
                });
            }
            var FlatDetails = new FlatDetailsView();
            FlatDetails.BuildingName = flat.Building.Name;
            FlatDetails.BuildingId = flat.BuildingId;
            FlatDetails.FlatNumber = flat.FlatNumber;
            FlatDetails.StanderdPrice = flat.StanderdPrice;
            FlatDetails.Floor = flat.Floor;
            FlatDetails.Size = flat.Size;
            FlatDetails.SellingPrice = flat.SellingPrice ?? 0;
            FlatDetails.IsSold = flat.IsSold;
            FlatDetails.Installments = flatInstallmentView;
            FlatDetails.FlatContractImages = flat.FlatContractImages.Select(p => $"{configuration["BaseUrl"]}/{p.ImageLink}").ToList();
            if (flat.Client == null)
            {
                FlatDetails.ClientName = "لا يوجد عميل";
            }
            else
            {
                FlatDetails.ClientName = flat.Client.Name;
                FlatDetails.ClientId = flat.Client.Id;
                FlatDetails.ClientPhone = flat.Client.PhoneNumber;
            }
            if(payment != null)
            {
                FlatDetails.PaymentId = flat.Payment.Id;
                FlatDetails.PaymentMethod = flat.Payment.PaymentMethod;
                FlatDetails.Description = flat.Payment.Description;
                FlatDetails.CollectingDay = flat.Payment.CollectingDay;
                FlatDetails.FullMonths = flat.Payment.FullMonths;
                FlatDetails.FullPrice = flat.Payment.FullPrice;
                FlatDetails.MonthlyPrice = flat.Payment.MonthlyPrice;
                FlatDetails.RestMonths = flat.Payment.RestMonths;
                FlatDetails.StartPrice = flat.Payment.StartPrice;
            }
            return FlatDetails;
        }
        public async Task<FlatTableView> GetFlatTableViewByIdAsync(int id)
        {
            var flats = await unitOfWork.Repository<Flat, int>().GetByIdAsync(new FlatSpecifications(id));
            var maped = mapper.Map<FlatTableView>(flats);
            return maped;
        }
    }
}
