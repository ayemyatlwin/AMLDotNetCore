using AMLDotNetCore.MiniKpayDomian.Features.TranLog;

namespace AMLDotNetCore.MiniKpayAPI.Endpoints.TranLog
{
    public static class TranLogEndpoints
    {
        public static void useTranLogServiceEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/tranLog", (string fromMobileNo, string toMobileNo, string amount, string pin, string? note) =>
            {
                TranLogService tranLogService = new TranLogService();
                var result = tranLogService.CreateTransfer(fromMobileNo, toMobileNo, amount, pin, note);

                if (result is string errorMessage)
                {
                    return Results.BadRequest(new { Message = errorMessage });
                }
                return Results.Ok(new
                {
                    Message = "Transfer successfully!",
                    Data = result
                });

            }).WithName("CreateTranLog")
            .WithOpenApi();
        }
    }
}
