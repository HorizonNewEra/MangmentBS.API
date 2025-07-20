using MangmentBS.Core.Entities;
using MangmentBS.Core.Params.Building;
using MangmentBS.Core.Params.Flat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Specifications.Flats
{
    public class FlatSpecifications : BaseSpecification<Flat, int>
    {
        public FlatSpecifications()
        {
            ApplyIncludes();
        }
        public FlatSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
        public FlatSpecifications(FlatSpecificationsParams Params) : base(
            p =>
            (!Params.BuildingId.HasValue || Params.BuildingId == p.BuildingId)
            &&(string.IsNullOrEmpty(Params.ClientId) || Params.ClientId == p.ClientId)
            )
        {
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "sizeasc":
                        AddOrderBy(p => p.Size);
                        break;
                    case "sizedesc":
                        AddOrderByDescending(p => p.Size);
                        break;
                    case "flatnumberasc":
                        AddOrderBy(p => p.FlatNumber);
                        break;
                    case "flatnumberdesc":
                        AddOrderByDescending(p => p.FlatNumber);
                        break;
                    case "standerdpriceasc":
                        AddOrderBy(p => p.StanderdPrice);
                        break;
                    case "standerdpricedesc":
                        AddOrderByDescending(p => p.StanderdPrice);
                        break;
                    case "floorasc":
                        AddOrderBy(p => p.Floor);
                        break;
                    case "floordesc":
                        AddOrderByDescending(p => p.Floor);
                        break;
                    case "SellingPriceasc":
                        AddOrderBy(p => p.SellingPrice);
                        break;
                    case "sellingpricedesc":
                        AddOrderByDescending(p => p.SellingPrice);
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
            Includes.Add(p => p.FlatContractImages);
            Includes.Add(p => p.Payment);
        }
    }
}
