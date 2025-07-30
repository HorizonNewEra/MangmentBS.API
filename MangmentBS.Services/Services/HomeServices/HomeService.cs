using AutoMapper;
using MangmentBS.Core;
using MangmentBS.Core.Dtos.Home;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Services.Interfaces;
using MangmentBS.Core.Specifications.Installments;
using MangmentBS.Core.Specifications.Payments;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Services.Services.HomeServices
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork unitOfWork;
        public HomeService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        private async Task<List<HomeInstallmentDetails>> Getlist(List<Installment> installments)
        {
            var homeInstallmentDetails = new List<HomeInstallmentDetails>();
            foreach (var installment in installments)
            {
                var installmentDetails = new HomeInstallmentDetails();
                var payment = await unitOfWork.Repository<Payment, int>().GetByIdAsync(new PaymentSpecifications(installment.PaymentId));
                installmentDetails.InstallmentDate = installment.DueDate;
                installmentDetails.InstallmentDateString = HelperFn.GetDateString(installment.DueDate);
                installmentDetails.InstallmentAmount = installment.Amount;
                installmentDetails.Id = installment.Id;
                installmentDetails.Name = payment.Client.Name;
                installmentDetails.Address = payment.Building.Name;
                installmentDetails.Phone = payment.Client.PhoneNumber;
                homeInstallmentDetails.Add(installmentDetails);
            }
            return homeInstallmentDetails.OrderBy(p => p.InstallmentDate).ToList();
        }
        public async Task<HomeDetails> GetHomeDetails()
        {
            DateTime date = DateTime.Now;
            var View = new HomeDetails
            {
                TotalFees = 0,
                TotalResived = 0,
                TotalRest = 0,
                PerviousInstallments = new List<HomeInstallmentDetails>(),
                CurrentInstallments = new List<HomeInstallmentDetails>(),
                NextInstallments = new List<HomeInstallmentDetails>()
            };
            var allInstallments = await unitOfWork.Repository<Installment, int>().GetAllAsync(new InstallmentSpecifications());
            var perviousInstallment = await Getlist(allInstallments.Where(p => p.IsPaid == false && p.DueDate < date).ToList());
            var currentInstallment = await Getlist(allInstallments.Where(p => p.IsPaid == false && p.DueDate == date).ToList());
            var NextInstallment = await Getlist(allInstallments.Where(p => p.IsPaid == false && p.DueDate > date && p.DueDate <= date.AddDays(5)).ToList());
            View.PerviousInstallments = perviousInstallment;
            View.CurrentInstallments = currentInstallment;
            View.NextInstallments = NextInstallment;
            var monthInstallments = allInstallments.Where(p => p.DueDate.Month == date.Month && p.DueDate.Year == date.Year).ToList();
            double totalFees = monthInstallments.Sum(p => p.Amount);
            double totalReceived = monthInstallments.Where(p => p.IsPaid).Sum(p => p.Amount);
            double totalRest = monthInstallments.Where(p => !p.IsPaid).Sum(p => p.Amount);
            View.TotalFees = totalFees;
            View.TotalResived = totalReceived;
            View.TotalRest = totalRest;
            return View;
        }
        public async Task<AgendaView> GetAgenda(int Month, int Year)
        {
            var monthName = HelperFn.MonthNameAr(Month);
            if (monthName == null) return null;
            var agenda = new AgendaView();
            var allInstallments = await unitOfWork.Repository<Installment, int>().GetAllAsync();
            var allPayments = await unitOfWork.Repository<Payment, int>().GetAllAsync(new PaymentSpecifications());

            agenda.Month = Month;
            agenda.Year = Year;
            agenda.DayView = new List<AgendaDayView>();
            agenda.MonthName = monthName;
            for (int i = 1; i <= DateTime.DaysInMonth(Year, Month); i++)
            {
                var dayDate = new DateTime(Year, Month, i);
                var dayView = new AgendaDayView();
                dayView.DateString = HelperFn.GetDateString(dayDate);
                dayView.DayDetails = new List<AgendaDayDetailsView>();
                var specifiedInstallmentDay = allInstallments.Where(p => p.DueDate.Month == Month && p.DueDate.Year == Year && p.DueDate.Day == i).ToList();
                if (specifiedInstallmentDay.Count > 0)
                {
                    foreach (var installment in specifiedInstallmentDay)
                    {
                        var payment = allPayments.FirstOrDefault(p => p.Id == installment.PaymentId);
                        var dayDetails = new AgendaDayDetailsView
                        {
                            InstallmentId = installment.Id,
                            DueDateString = HelperFn.GetStringFromDate(installment.DueDate),
                            PaiedDateString = HelperFn.GetStringFromDate(installment.PaidDate),
                            Amount = installment.Amount,
                            IsPaid = installment.IsPaid,
                            ClientName = payment.Client.Name,
                            ClientPhone = payment.Client.PhoneNumber
                        };
                        dayView.DayDetails.Add(dayDetails);
                    }
                    dayView.Count = dayView.DayDetails.Count;
                    agenda.DayView.Add(dayView);
                }
            }
            return agenda;
        }
    }
}