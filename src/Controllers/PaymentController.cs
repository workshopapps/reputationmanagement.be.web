using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayStack.Net;
using src.Data;
using src.Entities;
using src.Models;

namespace src.Controllers;

[ApiController]
[Route("api")]
public class PaymentController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly ApplicationDbContext _context;

    private readonly string Ptoken;

    private PayStackApi Pstack {get; set;}

    public PaymentController(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
        Ptoken = _configuration["Payment:PaystackSK"];
        Pstack = new PayStackApi(Ptoken);
    }

    [HttpPost("MakePayment")]
    public async Task<IActionResult> Payment(Payment payment)
    {
        TransactionInitializeRequest request = new()
        {
            AmountInKobo = payment.Amount * 100,
            Email = payment.Email,
            Reference = Generate().ToString(),
            Currency = "NGN"
        };
        TransactionInitializeResponse response = Pstack.Transactions.Initialize(request);
        if(response.Status)
        {
            var transaction = new Transaction
            {
                Amount = payment.Amount,
                Email = payment.Email,
                TrxRef = request.Reference,
                OrderNo = payment.OrderNo,
            };
            
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return Ok(response.Data.AuthorizationUrl);
        }
        return BadRequest(response.Message);
    }

    [HttpGet("VerifyPayment")]
    public async Task<IActionResult> Verify(string reference)
    {
        TransactionVerifyResponse response = Pstack.Transactions.Verify(reference);

        if (response.Data.Status == "success")
        {
            var transaction = _context.Transactions.Where(x => x.TrxRef == reference).FirstOrDefault();
            if (transaction != null)
            {
                var review = _context.Reviews.Where(x => x.ReviewId.ToString() == transaction.OrderNo).FirstOrDefault();
                if(review != null)
                {
                    review.Status = StatusType.paid;
                    _context.Reviews.Update(review);
                }
                 

                transaction.Status = true;
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
                return Ok("Transaction Verified");
            };
        }
        return BadRequest(response.Data.GatewayResponse);
    }

    public static int Generate()
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        return rand.Next(100000000, 999999999);
    }

}