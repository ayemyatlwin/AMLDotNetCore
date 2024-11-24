using AMLDotNetCore.MiniDigitalWallet.Domain.Features.User;

namespace AMLDotNetCore.MiniDigitalWallet.Api.Endpoints.User
{
    public static  class WallerUserEndpoint
    {
        public static void UserEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/users", async (string mobileNo, int amount) =>
            {
               WalletUserService walletUserService = new WalletUserService();
                var model = await walletUserService.DepositAsync(mobileNo, amount);
                if (model.IsValidationError)
                {
                    return Results.BadRequest(model.Message);
                }
                if (model.IsSystemError)
                {
                    return Results.BadRequest(model.Message);
                }
                return Results.Ok(model);


            }).WithName("Deposit").WithOpenApi();
        }

    }
}
