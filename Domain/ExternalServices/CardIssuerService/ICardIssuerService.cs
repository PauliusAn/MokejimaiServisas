using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.ExternalServices.CardIssuerService
{
    public interface ICardIssuerService
    {
        Task ReservePayment(Payment payment);

        Task CapturePayment(Payment payment);
    }
}