using AMLDotNetCore.MiniDigitalWallet.Database.Models;
using AMLDotNetCore.MiniDigitalWallet.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.MiniDigitalWallet.Domain.Features.User
{

    public class WalletUserService
    {
        private readonly AppDbContext _appDbContext;

        public WalletUserService()
        {
            _appDbContext = new AppDbContext();
        }

        public async Task<Result<WalletUserResponseModel>> DepositAsync(string mobileNo, int amount)
        {
            Result<WalletUserResponseModel> model = new Result<WalletUserResponseModel>();
            var result = await _appDbContext.TblUsers.AsNoTracking().FirstOrDefaultAsync(x => x.MobileNo == mobileNo);
            if (result is null)
            {
                model = Result<WalletUserResponseModel>.ValidationError("User Not Found!");
                goto Result;

            }
            if (amount < 10000)
            {
                model = Result<WalletUserResponseModel>.ValidationError("At least 10000 deposit required!");
                goto Result;

            }
            result.Balance += amount;
            _appDbContext.Entry(result).State = EntityState.Modified;
            int resp = _appDbContext.SaveChanges();
            if (resp != 1)
            {
                model = Result<WalletUserResponseModel>.SystemError("System Error!");
            }
            WalletUserResponseModel userModel = new WalletUserResponseModel()
            {
                User = result,
            };
            model = Result<WalletUserResponseModel>.Success(userModel);

        Result:
            return model;

        }
    }
}
