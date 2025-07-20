using AutoMapper;
using MangmentBS.Core;
using MangmentBS.Core.Dtos.Client;
using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Dtos.Flat;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Clients;
using MangmentBS.Core.Params.Flat;
using MangmentBS.Core.Params.Installments;
using MangmentBS.Core.Params.Payment;
using MangmentBS.Core.Services.Interfaces;
using MangmentBS.Core.Specifications.Clients;
using MangmentBS.Core.Specifications.Flats;
using MangmentBS.Core.Specifications.Installments;
using MangmentBS.Core.Specifications.Payments;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Services.Services.ClientServices
{
    public class ClientService : IClientServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public ClientService(IUnitOfWork _unitOfWork, IConfiguration _configuration, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            configuration = _configuration;
            mapper = _mapper;
        }
        public async Task<ErrorResponce> AddClient(AddClientView view)
        {
            if (view.Id == null) return new ErrorResponce("400", "الرقم القومي مطلوب");
            if (view.Name == null) return new ErrorResponce("400", "الاسم مطلوب");
            if (view.PhoneNumber == null) return new ErrorResponce("400", "رقم الهاتف مطلوب");
            if (view.Address == null) return new ErrorResponce("400", "العنوان مطلوب");
            if (view.Id.Length != 14) return new ErrorResponce("400", "الرقم القومي يجب ان يكون 14 رقم");
            if (view.PhoneNumber.Length != 11) return new ErrorResponce("400", "رقم الهاتف يجب ان يكون 11 رقم");
            var client = await unitOfWork.Repository<Client, string>().GetByIdAsync(new ClientSpecifications(view.Id));
            if (client != null) return new ErrorResponce("400", "هذا العميل موجود بالفعل");
            await unitOfWork.Repository<Client, string>().AddAsync(new Client()
            {
                Id = view.Id,
                Name = view.Name,
                PhoneNumber = view.PhoneNumber,
                Address = view.Address
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce("200", "تم اضافة العميل بنجاح");
            return new ErrorResponce("400", "حدث خطأ أثناء اضافة العميل");
        }
        public async Task<ErrorResponce> EditClient(EditClientView view)
        {
            if (view.Id == null) return new ErrorResponce("400", "الرقم القومي مطلوب");
            var clients = await unitOfWork.Repository<Client, string>().GetAllAsync();
            var client = clients.FirstOrDefault(p => p.Id == view.Id);
            if (client == null) return new ErrorResponce("400", "هذا العميل غير موجود");
            unitOfWork.Repository<Client, string>().Update(new Client()
            {
                Id = client.Id,
                Name=view.Name??client.Name,
                PhoneNumber=view.PhoneNumber??client.PhoneNumber,
                Address=view.Address??client.Address
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce("200", "تم تعديل العميل بنجاح");
            return new ErrorResponce("400", "حدث خطأ أثناء تعديل العميل، يرجى المحاولة مرة أخرى");
        }
        public async Task<PaginationResponse<ClientTableView>> GetAllClientTableViewAsync(ClientSpecificationsParams Params)
        {
            var clients = await unitOfWork.Repository<Client, string>().GetAllAsync(new ClientSpecifications(Params));
            if (!clients.Any()) return null;
            var count = await unitOfWork.Repository<Client, string>().CountAsync(new ClientSpecifications(Params));
            var maped = mapper.Map<IEnumerable<ClientTableView>>(clients);
            return new PaginationResponse<ClientTableView>(Params.PageSize, Params.PageIndex, count, maped);
        }
        public async Task<ClientDetailsView> GetClientDetails(string Id)
        {
            var clint = await unitOfWork.Repository<Client, string>().GetByIdAsync(new ClientSpecifications(Id));
            if (clint == null) return null;
            var flats = await unitOfWork.Repository<Flat, int>().GetAllAsync(new FlatSpecifications(new FlatSpecificationsParams() { ClientId=clint.Id}));
            var payments = await unitOfWork.Repository<Payment, int>().GetAllAsync(new PaymentSpecifications(new PaymentSpecificationsParams() { ClientId = clint.Id }));
            var clientpayments = new List<ClientPaymentView>();
            var clientflats = new List<ClientFlatView>();
            foreach (var flat in flats)
            {
                clientflats.Add(new ClientFlatView()
                {
                    BuildingName = flat.Building.Name,
                    FlatNumber = flat.FlatNumber,
                    Floor=flat.Floor,
                    SellingPrice=flat.SellingPrice??0,
                    Size=flat.Size,
                    FlatContractImages= flat.FlatContractImages.Select(p => $"{configuration["BaseUrl"]}/{p.ImageLink}").ToList()
                });
            }
            foreach (var pay in payments)
            {
                clientpayments.Add(new ClientPaymentView()
                {
                    CollectingDay = pay.CollectingDay,
                    Description = pay.Description,
                    FullMonths = pay.FullMonths,
                    FullPrice = pay.FullPrice,
                    MonthlyPrice = pay.MonthlyPrice,
                    PaymentMethod = pay.PaymentMethod,
                    RestMonths = pay.RestMonths,
                    StartPrice= pay.StartPrice
                });
            }
            return new ClientDetailsView()
            {
                Id = clint.Id,
                Adress = clint.Address,
                Name = clint.Name,
                PhoneNumber = clint.PhoneNumber,
                ClientPayments = clientpayments,
                ClientFlats= clientflats
            };
        }
        public async Task<List<ClientInstallmentView>> GetClientPaymentInstallments(int PaymentId)
        {
            var installments = await unitOfWork.Repository<Installment, int>().GetAllAsync(new InstallmentSpecifications(new InstallmentSpecificationsParams() { PaymentId = PaymentId }));
            if (!installments.Any()) return null;
            var view = new List<ClientInstallmentView>();
            foreach (var installment in installments)
            {
                view.Add(new ClientInstallmentView()
                {
                    Amount = installment.Amount,
                    DueDate= installment.DueDate,
                    DueDateString = HelperFn.GetDateString(installment.DueDate),
                    IsPaid = installment.IsPaid,
                    PaidDateString = GetDate(installment.PaidDate),
                    PaidDateTime= GetTime(installment.PaidDate)
                });
            }
            return view.OrderBy(p=>p.DueDate).ToList();
        }
        private string GetDate(DateTime? date)
        {
            if (date == null) return null;
            return HelperFn.GetDateString(date.Value);
        }
        private string GetTime(DateTime? date)
        {
            if (date == null) return null;
            return HelperFn.GetTimeString(date.Value);
        }
        public async Task<ClientTableView> GetClientTableViewByIdAsync(string id)
        {
            var client = await unitOfWork.Repository<Client, string>().GetByIdAsync(new ClientSpecifications(id));
            var maped = mapper.Map<ClientTableView>(client);
            return maped;
        }
        public async Task<List<AllClientsData>> GetAllClientsDataAsync()
        {
            var clients = await unitOfWork.Repository<Client, string>().GetAllAsync();
            if (!clients.Any()) return null;
            var view = new List<AllClientsData>();
            foreach (var client in clients)
            {
                view.Add(new AllClientsData()
                {
                    Id = client.Id,
                    Name = client.Name,
                    PhoneNumber = client.PhoneNumber,
                    Address = client.Address
                });
            }
            return view.OrderBy(p => p.Name).ToList();
        }
    }
}
