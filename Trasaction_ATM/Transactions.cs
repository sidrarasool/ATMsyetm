using System;

namespace Trasaction_ATM
{
    public class Transactions
    {
        public string TransType { get; set; } //cashWtihdraw or CashTransfer
        public int SenderAccNo { get; set; }
        public int ReceiverAccNo { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
