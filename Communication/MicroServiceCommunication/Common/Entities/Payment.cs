using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    [Serializable]
    public class Payment
    {

        public int AmountToPay;
        public string CardNumber;
        public string Name;
    }
}
