using System.Security.Claims;
using DataStore.Abstraction.DTO;
using FeatureObjects.Abstraction.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace CrowdFunding.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/investments")]
    public class InvestmentCalcController : ControllerBase
    {
        private readonly IInvestmentCalcManager _manager;
        public InvestmentCalcController(IInvestmentCalcManager manager, IConfiguration configuration)
        {
            _manager = manager;
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"]; 
        }
        //Calculating amount to amount to invest  and equity 
        [HttpPost("calculate")]
        public async Task<ActionResult> CalculateInvestment([FromBody] InvestmentCalculationDTO investmentCalculationDTO)
        {
            if (investmentCalculationDTO.SharesToBuy <= 0)
            {
                return BadRequest("Invalid share count");
            }
            var result = await _manager.CalculateInvestment(investmentCalculationDTO);
            return Ok(result);
        }


        //it generates the session and returns the session id which will be used in front end to call the payment gateway

        [HttpPost("process-payment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdClaim);

            var successUrl = "http://localhost:5173/PaymentSuccess";
            var cancelUrl = "http://localhost:5173/payment-cancel";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                BillingAddressCollection = "required",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmount = (long)(request.AmountInvested * 100),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Investment in " + request.CampaignId
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                Metadata = new Dictionary<string, string>
                {
                    { "userId", userId.ToString() },
                    { "campaignId", request.CampaignId.ToString() },
                    { "amountInvested", request.AmountInvested.ToString("F2") },
                    { "investmentDate", request.InvestmentDate.ToString("yyyy-MM-dd HH:mm:ss") },
                    { "sharesBought", request.ShareBuyed.ToString() },
                    { "equityOwned", request.EquityOwned.ToString("F2") },
                }
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { sessionId = session.Id });
        }

        //returns the data to display in the payment success page


        [HttpGet("user-payments")]
        public async Task<ActionResult<InvestmentPaymentDTO>> GetUserPayments()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User ID not found in token.");

            int userId = int.Parse(userIdClaim);
            var payments = await _manager.GetInvestmentPayments(userId);

            return Ok(payments);
        }
    }

    
}