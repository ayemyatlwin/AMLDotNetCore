using AMLDotNetCore.MiniKpayDomian.Features.User;

namespace AMLDotNetCore.MiniKpayAPI.Endpoints.User
{
    public static class UserEndpoints
    {
        public static void useUserServiceEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/user/changePin", (string mobileNo,string pin) =>
            {
                UserService userService = new UserService();
                var result = userService.ChangePin(mobileNo,pin);

                if (result is string errorMessage)
                {
                    return Results.BadRequest(new { Message = errorMessage });
                }

                return Results.Ok(new
                {
                    Message = "Pin Chnaged Successfully!",
                    Data = result
                });

            }).WithName("ChangePin")
          .WithOpenApi();

            app.MapGet("/user/balance/{mobileNo}", (string mobileNo) =>
            {
                UserService userService = new UserService();
                var result = userService.GetBalance(mobileNo);

                if (result is string errorMessage)
                {
                    return Results.BadRequest(new { Message = errorMessage });
                }

                return Results.Ok(new
                {
                    Message= "Balance",
                    Data = result
                });

            }).WithName("GetBalace")
            .WithOpenApi();

            app.MapPost("/user/createDeposit", (string mobileNo, string amount) =>
            {
                UserService userService = new UserService();
                var result = userService.CreateDeposit(mobileNo, amount);

                if (result is string errorMessage)
                {
                    return Results.BadRequest(new { Message = errorMessage });
                }
                return Results.Ok(new
                {
                    Message = "Deposit created successfully!",
                    Data = result
                });

            }).WithName("CreateDeposit")
              .WithOpenApi();

            app.MapPost("/user/createWithdraw", (string mobileNo, string amount) =>
            {
                UserService userService = new UserService();
                var result = userService.CreateWithdraw(mobileNo, amount);

                if (result is string errorMessage)
                {
                    return Results.BadRequest(new { Message = errorMessage });
                }
                return Results.Ok(new
                {
                    Message = "Withdrawn successfully!",
                    Data = result
                });

            }).WithName("CreateWithdraw")
             .WithOpenApi();

            




        }
    }
}
