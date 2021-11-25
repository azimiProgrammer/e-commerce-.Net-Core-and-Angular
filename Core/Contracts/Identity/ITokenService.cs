using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Identity
{
    public interface ITokenService
    {
        string Token(AppUser user);
    }
}
