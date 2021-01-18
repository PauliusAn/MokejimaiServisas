using System;
using System.Threading.Tasks;
using Domain.ExternalServices.RequestModels;
using Refit;

namespace Domain.ExternalServices.MerchantService
{
    public interface IMerchantApi
    {
        [Post("/merchants/payments")]
        Task AddPayment([Body] AddPaymentRequest data);

        [Delete("/merchants/payments/{id}")]
        Task DeletePayment(Guid id);

    }
}