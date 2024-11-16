using AMLDotNetCore.MiniKpayDomian.Features.TranLog;

namespace AMLDotNetCore.MiniKpayAPI.Endpoints.TranLog
{
    public static class TranLogEndpoints
    {


        public static void useTranLogServiceEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/transactions", () =>
            {
                TranLogService tranLogService = new TranLogService();
                var lst = tranLogService.GetTransactionsHistory();
                return Results.Ok(lst);
            })
            .WithName("GetTransactions")
            .WithOpenApi();

            app.MapGet("/transactions/{id}", (int id) =>
            {
                TranLogService tranLogService = new TranLogService();
                var result = tranLogService.GetTransactionsHistoryById(id);
                if (result is string errorMessage)
                {
                    return Results.BadRequest(new { Message = errorMessage });
                }
                return Results.Ok(new
                {
                    Message = "Transaction History.",
                    Data = result
                });
            })
            .WithName("GetTransaction")
            .WithOpenApi();


            app.MapPost("/transfer", (string fromMobileNo, string toMobileNo, string amount, string pin, string? note) =>
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
