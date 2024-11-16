using AMLDotNetCore.MiniKpayDatabase.Models;
using AMLDotNetCore.MiniKpayDomian.Validations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace AMLDotNetCore.MiniKpayDomian.Features.TranLog
{
    public class TranLogService
    {
        private readonly AppDbContext _db;
        private readonly TransferServiceValidation _validation;

        public TranLogService()
        {
            _db = new AppDbContext();
            _validation = new TransferServiceValidation();
        }

        public object CreateTransfer(string fromMobileNo, string toMobileNo, string amount, string pin, string? note)
        {
            var time = DateTime.Now;
            string currentTimeAsString = time.ToString();

            var validation = _validation.TransferValiation(fromMobileNo, toMobileNo, amount, pin);

            if (!validation.IsSuccess)
            {
                return validation.Message;
            }

            var fromModel = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == fromMobileNo);
            var toModel = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == toMobileNo);

            

           

            fromModel.Balance -= Int32.Parse(amount);

            toModel.Balance += Int32.Parse(amount);


            _db.TblUsers.Update(fromModel);
            _db.TblUsers.Update(toModel);
            _db.SaveChanges();

            TblTranLog tranLog = new TblTranLog();
            tranLog.FromMobileNo = fromMobileNo;
            tranLog.ToMobileNo = toMobileNo;
            tranLog.Amount = Int32.Parse(amount);
            tranLog.Note = note;
            tranLog.Time = currentTimeAsString;

            _db.TblTranLogs.Add(tranLog);
            _db.SaveChanges();
            return tranLog;

        }

    }
}
