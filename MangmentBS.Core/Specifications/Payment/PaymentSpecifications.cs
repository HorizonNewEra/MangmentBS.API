using MangmentBS.Core.Entities;
using MangmentBS.Core.Params.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Specifications.Payments
{
    public class PaymentSpecifications : BaseSpecification<Payment, int>
    {
        public PaymentSpecifications()
        {
            ApplyIncludes();
        }
        public PaymentSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
        public PaymentSpecifications(PaymentSpecificationsParams Params) : base(
            p =>
            (!Params.BuildingId.HasValue || Params.BuildingId == p.BuildingId)
            && (string.IsNullOrEmpty(Params.ClientId) || Params.ClientId == p.ClientId)
            && (!Params.CollectingDay.HasValue || Params.CollectingDay == p.CollectingDay)
            && (!Params.FlatId.HasValue || Params.FlatId == p.FlatId)
            )
        {
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "paymentmethodasc":
                        AddOrderBy(p => p.PaymentMethod);
                        break;
                    case "paymentmethoddesc":
                        AddOrderByDescending(p => p.PaymentMethod);
                        break;
                    case "clientasc":
                        AddOrderBy(p => p.Client.Name);
                        break;
                    case "clientdesc":
                        AddOrderByDescending(p => p.Client.Name);
                        break;
                    case "buildingasc":
                        AddOrderBy(p => p.Building.Name);
                        break;
                    case "buildingdesc":
                        AddOrderByDescending(p => p.Building.Name);
                        break;
                    case "collectingdayasc":
                        AddOrderBy(p => p.CollectingDay);
                        break;
                    case "collectingdaydesc":
                        AddOrderByDescending(p => p.CollectingDay);
                        break;
                    case "restmonthsasc":
                        AddOrderBy(p => p.RestMonths);
                        break;
                    case "restmonthsdesc":
                        AddOrderByDescending(p => p.RestMonths);
                        break;
                    case "fullfriceasc":
                        AddOrderBy(p => p.FullPrice);
                        break;
                    case "fullpricesdesc":
                        AddOrderByDescending(p => p.FullPrice);
                        break;
                    case "idasc":
                        AddOrderBy(p => p.Id);
                        break;
                    case "iddesc":
                        AddOrderByDescending(p => p.Id);
                        break;
                    default:
                        AddOrderBy(p => p.BuildingId);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.BuildingId);
            }
            ApplyIncludes();
            if (Params.PageSize > 0 && Params.PageIndex > 0)
            {
                ApplyPaging((Params.PageIndex - 1) * Params.PageSize, Params.PageSize);
            }
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Client);
            Includes.Add(p => p.Building);
            Includes.Add(p => p.Installments);
            Includes.Add(p => p.Flat);
        }
    }
}
