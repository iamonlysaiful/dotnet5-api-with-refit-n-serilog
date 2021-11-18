using DotNet5APIDemo.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Dynamic;

using System.Threading.Tasks;

namespace DotNet5APIDemo.Services
{
    public class UserService : IUserService
    {
        private readonly SourceContentService _contentSvc;
        public UserService(SourceContentService contentSvc)
        {
            _contentSvc = contentSvc;
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                var responseBody = await _contentSvc.Get(id);

                var payload = _contentSvc
                    .ExtractUser(responseBody)
                    .TransformUser();

                return payload;
            }
            catch (Exception ex)
            {
                Log.Error($"Error Occured at UserService:GetUserById. Message: {ex.Message}");
                return null;
            }

        }

        public async Task<IEnumerable<User>> GetUsers(int page)
        {
            try
            {
                var responseBody = await _contentSvc.GetList(page);

                var payload = _contentSvc
                    .ExtractUserList(responseBody)
                    .TransformUserList();

                return payload;
            }
            catch (Exception ex)
            {
                Log.Error($"Error Occured at UserService:GetUserById. Message: {ex.Message}");
                return null;
            }
        }

    }
}
