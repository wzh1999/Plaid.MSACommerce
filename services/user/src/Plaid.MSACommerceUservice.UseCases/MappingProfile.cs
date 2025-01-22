using Plaid.MSACommerce.Uservice.Core.Entites;
using Plaid.MSACommerce.Uservice.UseCases.Commands;
using Plaid.MSACommerce.Uservice.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaid.MSACommerce.Uservice.UseCases
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, TbUser>();

            CreateMap<TbUser, UserDto>();
        }
    }
}
