using MangmentBS.Core.Entities;
using MangmentBS.Core.Params.Building;
using MangmentBS.Core.Params.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Specifications.Users
{
    public class UserSpecifications : BaseSpecification<User, string>
    {
        public UserSpecifications()
        {
            ApplyIncludes();
        }
        public UserSpecifications(string id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
        public UserSpecifications(UserSpecificationsParams Params) : base(
            p =>
            (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))
            && (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))

            //&&(!Params.landid.HasValue || Params.landid ==p.LandId)
            )
        {
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "roleasc":
                        AddOrderBy(p => p.Role);
                        break;
                    case "roledesc":
                        AddOrderByDescending(p => p.Role);
                        break;
                    case "isactive":
                        AddOrderBy(p => p.IsActive);
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
            Includes.Add(p => p.Roles);
        }
    }
}
