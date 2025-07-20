using MangmentBS.Core.Entities;
using MangmentBS.Core.Params.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Specifications.Clients
{
    public class ClientSpecifications : BaseSpecification<Client, string>
    {
        public ClientSpecifications()
        {
            ApplyIncludes();
        }
        public ClientSpecifications(string id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
        public ClientSpecifications(ClientSpecificationsParams Params) : base(
            p =>
            (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))
            //&&(!Params.landid.HasValue || Params.landid ==p.LandId)
            )
        {
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "idasc":
                        AddOrderBy(p => p.Id);
                        break;
                    case "iddesc":
                        AddOrderByDescending(p => p.Id);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
            ApplyIncludes();
            if (Params.PageSize > 0 && Params.PageIndex > 0)
            {
                ApplyPaging((Params.PageIndex - 1) * Params.PageSize, Params.PageSize);
            }
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Flats);
            Includes.Add(p => p.Payments);
        }
    }
}
