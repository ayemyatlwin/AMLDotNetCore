using AMLDotNetCore.MiniKpayDatabase.Models;
using AMLDotNetCore.MiniKpayDomian.Validations;
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
        private readonly AppDbContext _db;
        private readonly UserServiceValidation.UserValidation _validation;

        public UserService()
        {
            _db = new AppDbContext();
            _validation = new UserServiceValidation.UserValidation();
        }

        public object ChangePin(string mobileNo, string pin)
        {
            var validation = _validation.ChangePinValidation(mobileNo, pin);

            if (!validation.IsSuccess)
            {
                return validation.Message;
            }
            var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

            model.PinCode = pin;
            _db.TblUsers.Update(model);
            _db.SaveChanges();

            return model;

        }

        public object GetBalance(string mobileNo)
        {
            var validation = _validation.GetBalanceValidation(mobileNo);

            if (!validation.IsSuccess)
            {
                return validation.Message;
            }
            var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x=>x.MobileNo== mobileNo);

            return model;

        }

        public object CreateDeposit(string mobileNo, string Amount)
        {

            var validation = _validation.DepositValidation(mobileNo, Amount);

            if (!validation.IsSuccess)
            {
                return validation.Message;
            }
            var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x=>x.MobileNo== mobileNo);

            model.Balance += Int32.Parse(Amount);

            _db.TblUsers.Update(model);
            _db.SaveChanges();
            return model;
        }

        public object CreateWithdraw(string mobileNo, string Amount)
        {

            var validation = _validation.WithdrawValidation(mobileNo, Amount);

            if (!validation.IsSuccess)
            {
                return validation.Message;
            }
            var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

            model.Balance -= Int32.Parse(Amount);

            _db.TblUsers.Update(model);
            _db.SaveChanges();
            return model;
        }




    }



}
    