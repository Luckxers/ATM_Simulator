using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Console_Application.Domain.Enums
{
    public enum AppMenu
    {
        CheckBalance = 1, 
        PlaceDeposit, 
        MakeWithdrawal, 
        InternalTransfer, 
        ViewTransaction, 
        Logout 
    }
}
