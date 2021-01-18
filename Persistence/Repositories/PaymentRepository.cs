using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DatabaseContext _dbContext;

        public PaymentRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Payment payment)
        {
            await _dbContext.AddAsync(payment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Payment payment)
        {
            _dbContext.Update(payment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Payment> Get(Guid id)
        {
            return await _dbContext.Payments.FindAsync(id);
        }
    }
}