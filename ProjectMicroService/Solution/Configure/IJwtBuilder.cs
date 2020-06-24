using System;
using System.Collections.Generic;
using System.Text;

namespace Configure
{
   public interface IJwtBuilder
    {
        string GetToken(Guid userId);
        Guid ValidateToken(string token);
    }
}
