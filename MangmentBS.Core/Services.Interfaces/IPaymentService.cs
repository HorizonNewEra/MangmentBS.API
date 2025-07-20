using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Dtos.Payment;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Building;
using MangmentBS.Core.Params.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaginationResponse<PaymentTableView>> GetAllPaymentTableViewAsync(PaymentSpecificationsParams paymentSpecificationsParams);
        Task<PaymentDetails> GetPaymentDetails(int Id);
        Task<SellFlatPaymentResponce> SellFlatPayment(SellFlatPaymentParams Params);
    }
}
