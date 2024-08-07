using Island.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Island.Core.Application.Interfaces.Authentication
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
