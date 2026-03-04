using BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.IManagers
{
    public interface IAppUserManager
    {
        Task SignUp(DTOUserSignUp dTOUserSignUp);
    }
}
