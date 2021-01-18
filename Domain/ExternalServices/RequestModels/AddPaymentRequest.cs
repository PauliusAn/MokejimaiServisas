using System;
using Shared.Enums;

namespace Domain.ExternalServices.RequestModels
{
    public class AddPaymentRequest
    {
        public Guid Id { get; set; }

        public Guid MerchantId { get; set; }

        public PaymentStatus Status { get; set; }
    }
}