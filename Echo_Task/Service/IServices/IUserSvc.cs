using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IUserSvc
    {
        public Task Register(UserRegister viewModel);

        public Task Login(UserLogin viewModel);

        public Task Logout();
    }
}
