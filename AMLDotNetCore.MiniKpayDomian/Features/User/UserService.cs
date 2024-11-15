using AMLDotNetCore.MiniKpayDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.MiniKpayDomian.Features.User
{
    public class UserService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public string GetBalance(int Id)
        {
            var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x=>x.UserId== Id);
            
            return model.Balance;

    }
    }

    
   
}
    