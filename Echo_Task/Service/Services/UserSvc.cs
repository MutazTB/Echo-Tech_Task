using Domain.DTOs;
using Repositories.IRepositories;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserSvc : IUserSvc
    {
        private IUserRepo _UserRepo;

        public UserSvc(IUserRepo userRepo)
        {
            _UserRepo = userRepo;
        }
        public async Task Login(UserLogin viewModel)
        {
            await _UserRepo.Login(viewModel);
        }

        public async Task Logout()
        {
            await _UserRepo.Logout();
        }

        public async Task Register(UserRegister viewModel)
        {
            await _UserRepo.Register(viewModel);
        }
    }
}
