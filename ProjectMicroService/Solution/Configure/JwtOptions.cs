using System;
using System.Collections.Generic;
using System.Text;

namespace Configure
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
