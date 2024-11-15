using AMLDotNetCore.Domain.Features.Blog;

namespace AMLDotNetCore.MinimalApi.Endpoints.Blogs;

public static class BlogServiceEndpoint
{

    public static void useBlogServiceEndPoint( this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            BlogService blogService = new BlogService();
            var lst = blogService.GetBlogs();
            return Results.Ok(lst);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", (int id) =>
        {

            BlogService blogService = new BlogService();
            var item = blogService.GetById(id);
            if (item is null)
            {
                return Results.BadRequest("No Data Found!");
            }
            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();


        app.MapPost("/blogs", (TblBlog blog) =>
        {

            BlogService blogService = new BlogService();
            var item = blogService.CreateBlog(blog);
            return Results.Ok(blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();


        app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
        {

            BlogService blogService = new BlogService();
            var item = blogService.UpdateBlog(id, blog);
            return Results.Ok(blog);
        })
        .WithName("EditBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", (int id) =>
        {

            BlogService blogService = new BlogService();
            var result = blogService.DeleteBlog(id);
            return Results.Ok(result);
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}
