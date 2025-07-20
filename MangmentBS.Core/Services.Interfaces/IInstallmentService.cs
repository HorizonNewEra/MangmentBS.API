using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Dtos.Installment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Services.Interfaces
{
    public interface IInstallmentService
    {
        Task<InstallmentDetails> GetInstallmentByIdAsync(int Id);
        Task<ErrorResponce> PayInstallment(int Id);
    }
}
