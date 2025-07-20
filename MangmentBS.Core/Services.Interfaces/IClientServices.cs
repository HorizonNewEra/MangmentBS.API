using MangmentBS.Core.Dtos.Client;
using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Services.Interfaces
{
    public interface IClientServices
    {
        Task<PaginationResponse<ClientTableView>> GetAllClientTableViewAsync(ClientSpecificationsParams Params);
        Task<ClientTableView> GetClientTableViewByIdAsync(string ClientId);
        Task<List<AllClientsData>> GetAllClientsDataAsync();
        Task<ClientDetailsView> GetClientDetails(string ClientId);
        Task<List<ClientInstallmentView>> GetClientPaymentInstallments(int PaymentId);
        Task<ErrorResponce> AddClient(AddClientView view);
        Task<ErrorResponce> EditClient(EditClientView view);
    }
}
