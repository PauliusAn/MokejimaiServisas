using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.ExternalServices.MerchantService
{
    public interface IMerchantService
    {
        Task AddPayment(Payment payment);

        Task DeletePayment(Guid id);
    }
}