using AMLDotNetCore.DataBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMLDotNetCore.MinimalApi.Endpoints.Blogs;

public static class BlogEndpoint
{

    public static void useBlogEndPoint( this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
        {
            var model = db.TblBlogs.AsNoTracking().ToList();
            return Results.Ok(model);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] AppDbContext db , int id) =>
        {
            var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.BadRequest("No Data Found!");
            }
            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();


        app.MapPost("/blogs", ([FromServices] AppDbContext db,TblBlog blog) =>
        {
            var item = db.TblBlogs.Add(blog);
            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();


        app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db,int id, TblBlog blog) =>
        {
            var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.BadRequest("No Data Found!");
            }
            item.BlogId = id;
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthot = blog.BlogAuthot;
            item.BlogContent = blog.BlogContent;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("EditBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db,int id) =>
        {
            var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.BadRequest("No Data Found!");
            }

            db.Entry(item).State = EntityState.Deleted;
            db.SaveChanges();
            return Results.Ok();
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}
