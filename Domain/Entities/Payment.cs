using System;
using Shared.Enums;

namespace Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public Guid MerchantId { get; set; }
    }
}