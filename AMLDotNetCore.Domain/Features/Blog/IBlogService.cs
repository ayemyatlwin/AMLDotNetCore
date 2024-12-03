using AMLDotNetCore.DataBase.Models;

namespace AMLDotNetCore.Domain.Features.Blog
{
    public interface IBlogService
    {
        TblBlog CreateBlog(TblBlog blog);
        bool? DeleteBlog(int id);
        List<TblBlog> GetBlogs();
        TblBlog GetById(int id);
        TblBlog patchlog(int id, TblBlog blog);
        TblBlog UpdateBlog(int id, TblBlog blog);
    }
}