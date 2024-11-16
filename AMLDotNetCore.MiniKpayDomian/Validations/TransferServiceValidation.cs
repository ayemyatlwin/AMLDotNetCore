using AMLDotNetCore.MiniKpayDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.MiniKpayDomian.Validations
{
    public class TransferServiceValidation
    {
        private readonly AppDbContext _db = new AppDbContext();


        public ValidationResult GetTranHistoryValidation(int id)
        {
            var model = _db.TblTranLogs.AsNoTracking().FirstOrDefault(x => x.TransactionId == id);

            if (model == null)
            {
                return new ValidationResult
                {
                    IsSuccess = false,
                    Message = "Transaction Not Found!"
                };
            }

            return new ValidationResult
            {
                IsSuccess = true,
            };
        }


        public ValidationResult TransferValiation(string fromMobileNo, string toMobileNo, string amount, string pin)
        {
            var fromModel = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == fromMobileNo);
            var toModel = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == toMobileNo);


            if (fromMobileNo == toMobileNo)
            {
                return new ValidationResult
                {
                    IsSuccess = false,
                    Message = "Phone numbers must not be same!"
                };
            }

            if (fromModel.Balance < Int32.Parse(amount))
            {
                return new ValidationResult
                {
                    IsSuccess = false,
                    Message = "Transfer amount must not greater than your current balance!"
                };
            }

            if (10000 >= fromModel.Balance - Int32.Parse(amount))
            {
                return new ValidationResult
                {
                    IsSuccess = false,
                    Message = "The balance must be not less than 10000 in your account!"
                };
            }

            if (fromModel.PinCode != pin)
            {
                return new ValidationResult
                {
                    IsSuccess = false,
                    Message = "Invalid PinCode!!!"
                };
            }

            if (fromModel == null)
            {
                return new ValidationResult
                {
                    IsSuccess = false,
                    Message = "Transfer User Not Found!"
                };
            }
            if (toModel == null)
            {
                return new ValidationResult
                {
                    IsSuccess = false,
                    Message = "Recipient User Not Found!"
                };
            }
            if (Int32.Parse(amount) <= 0)
            {
                return new ValidationResult
                {
                    IsSuccess = false,
                    Message = "Invalid transfer amount. It must be greater than zero."
                };
            }
            return new ValidationResult
            {
                IsSuccess = true,
                Message = "Transfer Successful!"
            };

        }

    }
    public class ValidationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

}
