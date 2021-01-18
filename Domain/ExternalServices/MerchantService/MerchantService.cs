using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ExternalServices.RequestModels;
using Refit;

namespace Domain.ExternalServices.MerchantService
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantApi _merchantApi;

        public MerchantService()
        {
            _merchantApi = RestService.For<IMerchantApi>("https://localhost:5003/api");
        }

        public async Task AddPayment(Payment payment)
        {
            var request = new AddPaymentRequest
            {
                Id = payment.Id,
                MerchantId = payment.MerchantId,
                Status = payment.Status
            };

            await _merchantApi.AddPayment(request);
        }

        public async Task DeletePayment(Guid id)
        {
            await _merchantApi.DeletePayment(id);
        }
    }
}