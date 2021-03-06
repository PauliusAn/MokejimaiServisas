﻿using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ExternalServices.CardIssuerService;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models.Requests;
using Shared.Enums;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICardIssuerService _cardIssuerService;

        public PaymentsController(
            IPaymentRepository paymentRepository,
            ICardIssuerService cardIssuerService)
        {
            _paymentRepository = paymentRepository;
            _cardIssuerService = cardIssuerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                Status = PaymentStatus.Created,
                MerchantId = Guid.Parse("87889a5c-a699-4f77-8fd5-4bca3933a4bc") // Hardcoded for simplicity. In real life merchant id would be taken from JWT
            };

            await _paymentRepository.Add(payment);
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}] Naujas mokėjimas sukurtas. Mokėjimo ID: {payment.Id}");

            return Ok(new {paymentId = payment.Id});
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> AcceptPayment(Guid id, [FromBody] UpdatePaymentRequest request)
        {
            if (request.Status != PaymentStatus.Accepted)
            {
                return BadRequest(new {error = $"Payment status can not be {request.Status}"});
            }

            var payment = await _paymentRepository.Get(id);
            if (payment == null)
            {
                return NotFound();
            }

            payment.Status = request.Status;

            await _paymentRepository.Update(payment);
            Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}] Vartotojas patvirtino mokėjimą. Mokėjimo ID: {payment.Id}");

            try
            {
                await _cardIssuerService.ReservePayment(payment);
                await _cardIssuerService.CapturePayment(payment);
            }
            catch(Exception)
            {
                payment.Status = PaymentStatus.Created;
                await _paymentRepository.Update(payment);
                Console.WriteLine($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}] Mokėjimo statusas grąžintas į pradinę padėtį. Mokėjimo ID: {payment.Id}");

                return StatusCode(500);
            }

            return Ok();
        }
    }
}
