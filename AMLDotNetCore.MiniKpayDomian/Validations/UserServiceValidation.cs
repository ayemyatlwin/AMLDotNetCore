using AMLDotNetCore.MiniKpayDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.MiniKpayDomian.Validations
{
    public  class UserServiceValidation
    {
        public class UserValidation
        {
            private readonly AppDbContext _db=new AppDbContext();

            public ValidationResult ChangePinValidation(string mobileNo,string pin)
            {
                var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

                if (model == null)
                {
                    return new ValidationResult
                    {
                        IsSuccess = false,
                        Message = "User Not Found!"
                    };
                }

                if (model.PinCode == pin)
                {
                    return new ValidationResult
                    {
                        IsSuccess = false,
                        Message = "Old Pin Can't be used again!"
                    };
                }


                if (pin.Length != 6 || !pin.All(char.IsDigit))
                {
                    return new ValidationResult
                    {
                        IsSuccess = false,
                        Message = "Pin must be exactly 6 digits and contain only numbers"
                    };
                }

                return new ValidationResult
                {
                    IsSuccess = true,
                };
            }


            public ValidationResult GetBalanceValidation(string mobileNo)
            {
                var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

                if (model == null)
                {
                    return new ValidationResult
                    {
                        IsSuccess = false,
                        Message = "User Not Found!"
                    };
                }
               
                return new ValidationResult
                {
                    IsSuccess = true,
                };
            }


            public ValidationResult DepositValidation(string mobileNo, string amount)
            {
                var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

                if (model == null)
                {
                    return new ValidationResult
                    {
                        IsSuccess = false,
                        Message = "User Not Found!"
                    };
                }
                if (Int32.Parse(amount) <= 0)
                {
                    return new ValidationResult
                    {
                        IsSuccess = false,
                        Message = "Invalid deposit amount. It must be greater than zero."
                    };
                }

                return new ValidationResult
                {
                    IsSuccess = true,
                    Message = "Deposit successful."
                };
            }

            public ValidationResult WithdrawValidation(string mobileNo, string amount)
            {
                var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

                if (model == null)
                {
                    return new ValidationResult
                    {
                        IsSuccess = false,
                        Message = "User Not Found!"
                    };
                }
                if (10000 >= Int32.Parse(model.Balance) - Int32.Parse(amount))
                {
                    return new ValidationResult
                    {
                        IsSuccess = false,
                        Message = "The balance must be not less than 10000 in your account!"
                    };
                }

                if (Int32.Parse(amount) <= 0)
                {
                    return new ValidationResult
                    {
                        IsSuccess = false,
                        Message = "Invalid withdraw amount. It must be greater than zero."
                    };
                }

                return new ValidationResult
                {
                    IsSuccess = true,
                    Message = "Withdraw successful."
                };
            }


        }

        public class ValidationResult
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

       
    }
}
