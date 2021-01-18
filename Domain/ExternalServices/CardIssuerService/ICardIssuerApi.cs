using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace Domain.ExternalServices.CardIssuerService
{
    public interface ICardIssuerApi
    {
        [Post("/cardissuer/reserve")]
        Task<ApiResponse<object>> ReservePayment();

        [Post("/cardissuer/capture")]
        Task CapturePayment();

        [Post("/cardissuer/release")]
        Task<ApiResponse<object>> ReleasePayment();
    }
}