using AMLDotNetCore.Domain.Features.Blog;
using Microsoft.AspNetCore.Mvc;

namespace AMLDotNetCore.MinimalApi.Endpoints.Blogs;

public static class BlogServiceEndpoint
{

    public static void useBlogServiceEndPoint( this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] IBlogService blogService) =>
        {
            var lst = blogService.GetBlogs();
            return Results.Ok(lst);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] IBlogService blogService,int id) =>
        {

            var item = blogService.GetById(id);
            if (item is null)
            {
                return Results.BadRequest("No Data Found!");
            }
            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();


        app.MapPost("/blogs", ([FromServices] IBlogService blogService,TblBlog blog) =>
        {

            var item = blogService.CreateBlog(blog);
            return Results.Ok(blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();


        app.MapPut("/blogs/{id}", ([FromServices] IBlogService blogService,int id, TblBlog blog) =>
        {

            var item = blogService.UpdateBlog(id, blog);
            return Results.Ok(blog);
        })
        .WithName("EditBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] IBlogService blogService,int id) =>
        {

            var result = blogService.DeleteBlog(id);
            return Results.Ok(result);
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}
