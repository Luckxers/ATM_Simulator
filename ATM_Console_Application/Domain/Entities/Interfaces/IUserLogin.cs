using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Console_Application.Domain.Entities.Interfaces
{
    public interface IUserLogin
    {
        void CheckUserCardNumAndPass();
    }
}
