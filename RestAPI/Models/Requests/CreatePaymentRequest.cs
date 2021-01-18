using System.Collections.Generic;

namespace RestAPI.Models.Requests
{
    public class CreatePaymentRequest
    {
        public decimal Amount { get; set; }

        public Dictionary<string, decimal> ReceiptContent { get; set; }
    }
}