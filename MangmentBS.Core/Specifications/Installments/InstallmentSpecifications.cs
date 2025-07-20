using MangmentBS.Core.Entities;
using MangmentBS.Core.Params.Building;
using MangmentBS.Core.Params.Installments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Specifications.Installments
{
    public class InstallmentSpecifications : BaseSpecification<Installment, int>
    {
        public InstallmentSpecifications()
        {
            ApplyIncludes();
        }
        public InstallmentSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
        public InstallmentSpecifications(InstallmentSpecificationsParams Params) : base(
            p =>
            (!Params.PaymentId.HasValue || Params.PaymentId == p.PaymentId)
            &&(!Params.IsPaid.HasValue || Params.IsPaid == p.IsPaid)
            //&&(string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))
            )
        {
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "idasc":
                        AddOrderBy(p => p.Id);
                        break;
                    case "iddesc":
                        AddOrderByDescending(p => p.Id);
                        break;
                    case "amountasc":
                        AddOrderBy(p => p.Amount);
                        break;
                    case "amountdesc":
                        AddOrderByDescending(p => p.Amount);
                        break;
                    case "duedateasc":
                        AddOrderBy(p => p.DueDate);
                        break;
                    case "duedatedesc":
                        AddOrderByDescending(p => p.DueDate);
                        break;
                    default:
                        AddOrderBy(p => p.DueDate);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.DueDate);
            }
            ApplyIncludes();
            if (Params.PageSize > 0 && Params.PageIndex > 0)
            {
                ApplyPaging((Params.PageIndex - 1) * Params.PageSize, Params.PageSize);
            }
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Payment);
        }
    }
}
