using System;
using System.Collections.Generic;
using System.Text;

namespace Configure
{
   public interface IEncryptor
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);

    }
}
