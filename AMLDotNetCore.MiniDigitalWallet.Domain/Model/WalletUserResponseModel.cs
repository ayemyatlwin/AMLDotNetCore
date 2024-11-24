using AMLDotNetCore.MiniDigitalWallet.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMLDotNetCore.MiniDigitalWallet.Domain.Model
{
    public class WalletUserResponseModel
    {
        public TblUser User { get; set; }
    }
}
