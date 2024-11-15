using AMLDotNetCore.MiniKpayDomian.Features.User;

namespace AMLDotNetCore.MiniKpayAPI.Endpoints.User
{
    public static class UserEndpoints
    {
        public static void useUserServiceEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/user/balance/{id}", (int id) =>
            {
                UserService userService = new UserService();
                string result = userService.GetBalance(id);
                return Results.Ok(result);

            }).WithName("GetBalace")
            .WithOpenApi();
        }
    }
}
