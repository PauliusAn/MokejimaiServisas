using System.Threading.Tasks;
using Domain.Entities;
using Domain.ExternalServices.MerchantService;
using Refit;

namespace Domain.ExternalServices.CardIssuerService
{
    public class CardIssuerService : ICardIssuerService
    {
        private readonly ICardIssuerApi _cardIssuerApi;
        private readonly IMerchantService _merchantService;

        public CardIssuerService(IMerchantService merchantService)
        {
            _merchantService = merchantService;
            _cardIssuerApi = RestService.For<ICardIssuerApi>("https://localhost:5003");
        }

        public async Task ReservePayment(Payment payment)
        {
            await _cardIssuerApi.ReservePayment();
            await _merchantService.AddPayment(payment);
        }

        public async Task CapturePayment(Payment payment)
        {
            try
            {
                await _cardIssuerApi.CapturePayment();
            }
            catch (ApiException)
            {
                // Pritaikomas SAGA metodas atstatytant sistemą į pradinę būseną
                await _merchantService.DeletePayment(payment.Id);
                await _cardIssuerApi.ReleasePayment();

                throw;
            }
        }
    }
}