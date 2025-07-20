using AutoMapper;
using MangmentBS.Core;
using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Dtos.Payment;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Payment;
using MangmentBS.Core.Services.Interfaces;
using MangmentBS.Core.Specifications.Buildings;
using MangmentBS.Core.Specifications.Clients;
using MangmentBS.Core.Specifications.Flats;
using MangmentBS.Core.Specifications.Payments;
using MangmentBS.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Services.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public PaymentService(IUnitOfWork _unitOfWork, IMapper _mapper, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            configuration = _configuration;
        }
        public async Task<PaginationResponse<PaymentTableView>> GetAllPaymentTableViewAsync(PaymentSpecificationsParams Params)
        {
            var payments = await unitOfWork.Repository<Payment, int>().GetAllAsync(new PaymentSpecifications(Params));
            var count = await unitOfWork.Repository<Payment, int>().CountAsync(new PaymentSpecifications(Params));
            var maped = mapper.Map<IEnumerable<PaymentTableView>>(payments);
            return new PaginationResponse<PaymentTableView>(Params.PageSize, Params.PageIndex, count, maped);
        }
        public async Task<PaymentDetails> GetPaymentDetails(int Id)
        {
            var payment = await unitOfWork.Repository<Payment, int>().GetByIdAsync(new PaymentSpecifications(Id));
            if (payment == null) return null;
            var installmentviews = new List<PaymentInstallmentView>();
            foreach (var Installment in payment.Installments)
            {
                installmentviews.Add(new PaymentInstallmentView()
                {
                    Id = Installment.Id,
                    Amount = Installment.Amount,
                    IsPaid = Installment.IsPaid,
                    DueDateString = Installment.DueDate.ToString("dd/MM/yyyy"),
                    PaidDateString = Installment.PaidDate?.ToString("dd/MM/yyyy") ?? "لم يتم دفع القصت"
                });
            }
            var paymentview = new PaymentDetails()
            {
                Id = payment.Id,
                ClientId = payment.ClientId,
                BuildingId = payment.BuildingId,
                FlatId = payment.FlatId,
                ClientName = payment.Client?.Name ?? "لا يوجد عميل",
                ClientPhone = payment.Client?.PhoneNumber ?? "لا يوجد رقم هاتف",
                ClientAddress = payment.Client?.Address ?? "لا يوجد عنوان",
                BuildingName = payment.Building?.Name ?? "لا يوجد اسم عقار",
                BuildingLocation = payment.Building?.Location ?? "لا يوجد عنوان عقار",
                FlatNumber = payment.Flat?.FlatNumber ?? 0,
                FullPrice = payment.FullPrice,
                StartPrice = payment.StartPrice,
                PaymentMethod = payment.PaymentMethod,
                PaymentDescription = payment.Description,
                FullInstallment = payment.FullMonths,
                RestInstallment = payment.RestMonths,
                InstallmentPrice = payment.MonthlyPrice,
                CollectingDay = payment.CollectingDay,
                Installments = installmentviews
            };
            return paymentview;
        }
        public async Task<SellFlatPaymentResponce> SellFlatPayment(SellFlatPaymentParams Params)
        {
            var response = new SellFlatPaymentResponce();
            response.Status = "404";
            response.Message = "التاريخ غير صحيح";
            var client = await unitOfWork.Repository<Client, string>().GetByIdAsync(new ClientSpecifications(Params.ClientId));
            if (client == null)
            {
                response.Message = "العميل غير موجود";
                return response;
            }
            var flat = await unitOfWork.Repository<Flat, int>().GetByIdAsync(new FlatSpecifications(Params.FlatId));
            if (flat == null)
            {
                response.Message = "الشقه غير موجوده";
                return response;
            }
            if (flat.IsSold)
            {
                response.Message = "هذة الشقه مباع مسبقا";
                return response;
            }
            DateTime date= DateTime.Now;
            if (HelperFn.GetDateFromString(Params.CollectingDate).HasValue)
            date = HelperFn.GetDateFromString(Params.CollectingDate).Value;
            else return response;
            if(date.Day>25)
            {
                response.Message = "يجب ان يكون يوم التحصيل هو 25 او اقل";
                return response;
            }
            var pay = new Payment
            {
                ClientId = Params.ClientId,
                BuildingId = flat.BuildingId,
                FlatId = Params.FlatId,
                FullPrice = Params.FullPrice,
                StartPrice = Params.StartPrice,
                PaymentMethod = Params.PaymentMethod,
                Description = Params.Description,
                FullMonths = Params.FullMonths,
                RestMonths = Params.FullMonths,
                MonthlyPrice = Params.MonthlyPrice,
                CollectingDay = date.Day,
            };
            await unitOfWork.Repository<Payment, int>().AddAsync(pay);
            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)  return response;
            var payments = await unitOfWork.Repository<Payment, int>().GetAllAsync();
            var payment = payments.FirstOrDefault(p => p.FlatId == Params.FlatId && p.ClientId == Params.ClientId && p.BuildingId == flat.BuildingId);
            if (payment == null) return response;
            flat.SellingPrice = Params.FullPrice;
            flat.ClientId = Params.ClientId;
            flat.IsSold = true;
            flat.PaymentId = payment.Id;
            unitOfWork.Repository<Flat, int>().Update(flat);
            result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                return response;
            }
            var installment = new Installment();
            for (int i = 1; i <= Params.FullMonths; i++)
            {
                var install = new Installment
                {
                    Amount = Params.MonthlyPrice,
                    DueDate = date.AddMonths(i * Params.EveryManyMonth),
                    PaymentId = payment.Id
                };
                await unitOfWork.Repository<Installment, int>().AddAsync(install);
            }
            result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
            {
                flat.SellingPrice = 0;
                flat.ClientId = null;
                flat.IsSold = false;
                flat.PaymentId = null;
                unitOfWork.Repository<Payment, int>().Delete(payment);
                unitOfWork.Repository<Flat, int>().Update(flat);
                result = await unitOfWork.SaveChangesAsync();
                response.Message = "حدث خظأ اثناء عملية البيع";
                return response;
            }
            response.Status= "200";
            response.Message = "تم العمليه بنجاح";
            response.PaymentId = payment.Id;
            return response;
        }
    }
}
