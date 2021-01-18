using Shared.Enums;

namespace RestAPI.Models.Requests
{
    public class UpdatePaymentRequest
    {
        public PaymentStatus Status { get; set; }
    }
}