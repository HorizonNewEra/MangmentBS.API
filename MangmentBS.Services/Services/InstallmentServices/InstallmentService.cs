using AutoMapper;
using MangmentBS.Core;
using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Dtos.Installment;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Services.Interfaces;
using MangmentBS.Core.Specifications.Flats;
using MangmentBS.Core.Specifications.Installments;
using MangmentBS.Core.Specifications.Payments;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Services.Services.InstallmentServices
{
    public class InstallmentService : IInstallmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public InstallmentService(IUnitOfWork _unitOfWork, IMapper _mapper, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            configuration = _configuration;
        }
        public async Task<InstallmentDetails> GetInstallmentByIdAsync(int Id)
        {
            var installmet = await unitOfWork.Repository<Installment, int>().GetByIdAsync(new InstallmentSpecifications(Id));
            if (installmet == null) return null;
            var payment = await unitOfWork.Repository<Payment, int>().GetByIdAsync(new PaymentSpecifications(installmet.PaymentId));
            if (payment == null) return null;
            var installmentDetails =new InstallmentDetails
            {
                Id = installmet.Id,
                PaymentId = installmet.PaymentId,
                ClientId = payment.ClientId,
                BuildingId = payment.BuildingId,
                FlatId = payment.FlatId,
                Amount = installmet.Amount,
                DueDateString = installmet.DueDate.ToString("dd/MM/yyyy"),
                IsPaid = installmet.IsPaid,
                PaidDateString = installmet.PaidDate?.ToString("dd/MM/yyyy"),
                ClientName = payment.Client.Name,
                ClientPhone = payment.Client.PhoneNumber,
                ClientAddress = payment.Client.Address,
                BuildingName = payment.Building.Name,
                BuildingLocation = payment.Building.Location,
                FlatNumber = payment.Flat.FlatNumber,
                Floor = payment.Flat.Floor,
                FlatSize = payment.Flat.Size,
                SellingPrice = payment.Flat.SellingPrice ?? 0
            };
            return installmentDetails;
        }
        public async Task<ErrorResponce> PayInstallment(int Id)
        {
            var installment = await unitOfWork.Repository<Installment, int>().GetByIdAsync(new InstallmentSpecifications(Id));
            if (installment == null) return new ErrorResponce("404", $"الاقساطة برقم {Id} غير موجودة");
            if (installment.IsPaid) return new ErrorResponce("400", $"الاقساطة برقم {Id} مدفوعة بالفعل");
            installment.IsPaid = true;
            installment.PaidDate = DateTime.Now;
            unitOfWork.Repository<Installment, int>().Update(installment);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce("200", $"تم دفع الاقساطة بنجاح");
            return new ErrorResponce("400", $"حدث خطأ أثناء دفع الاقساطة برقم {Id}");
        }
    }
}
