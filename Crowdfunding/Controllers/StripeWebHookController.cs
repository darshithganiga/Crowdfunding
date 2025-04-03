using FeatureObjects.Abstraction.Managers;
using FeatureObjects.Implementation.Managers;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Text.Json;

[ApiController]
[Route("stripe-webhook")]
public class StripeWebhookController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IInvestmentCalcManager _calcManager;
    public StripeWebhookController(IConfiguration configuration, IInvestmentCalcManager calcManager)
    {
        _configuration = configuration;
        _calcManager = calcManager;
    }

    [HttpPost]
    public async Task<IActionResult> HandleWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var stripeSecret = _configuration["Stripe:WebhookSecret"];

        try
        {
            Console.WriteLine($"🔹 Received Webhook: {json}");
            Console.WriteLine($"🔹 Stripe Signature: {Request.Headers["Stripe-Signature"]}");

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], stripeSecret);

            Console.WriteLine($"🔹 Event Type: {stripeEvent.Type}");

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;
                var metadata = session.Metadata;

                Console.WriteLine("🔹 Metadata received:");
                foreach (var key in metadata.Keys)
                {
                    Console.WriteLine($"{key}: {metadata[key]}");
                }

                int userId = int.Parse(metadata["userId"]);
                int campaignId = int.Parse(metadata["campaignId"]);
                decimal amountInvested = decimal.Parse(metadata["amountInvested"]);
                int sharesBought = int.Parse(metadata["sharesBought"]);
                decimal equityOwned = decimal.Parse(metadata["equityOwned"]);
                DateTime investmentDate = DateTime.Parse(metadata["investmentDate"]);

                Console.WriteLine($"✅ Processing payment for User {userId}, Campaign {campaignId}");

                bool success = await _calcManager.HandleSuccessfulPayment(
                    session.Id, userId, campaignId, sharesBought, amountInvested, equityOwned, investmentDate);

                if (!success)
                {
                    Console.WriteLine("❌ Error processing investment.");
                    return StatusCode(500, "Error processing investment.");
                }
            }

            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Webhook Error: {ex.Message}");
            return StatusCode(500, $"Webhook Error: {ex.Message}");
        }
    }

}
