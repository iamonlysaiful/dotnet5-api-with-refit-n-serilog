using DotNet5APIDemo.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNet5APIDemo.Services
{
    public class DataProcessorService : SourceContentService
    {
        private readonly IHttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private List<ExpandoObject> source;
        public DataProcessorService(IHttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public override async Task<string> GetList(int page)
        {
            var responseBody = await _httpClient.GetUserList(_configuration["ReqRes:Path"], page);
            return responseBody;
        }

        public override DataProcessorService ExtractUserList(string json)
        {
            var users = JsonSerializer.Deserialize<UsersResponse>(json);
            source = users.data;

            Log.Information("User List Extracted.");
            return this;
        }

        public override IEnumerable<User> TransformUserList()
        {

            List<User> users = new List<User>();
            foreach (dynamic source_record in source)
            {
                var user = new User();
                user.id = Convert.ToInt32(source_record.id.ToString());
                user.email = source_record.email.ToString();
                user.name = source_record.first_name.ToString() + " " + source_record.last_name.ToString();
                user.avatar = source_record.avatar.ToString();
                user.attendance = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss.000 tt");
                users.Add(user);
            }

            Log.Information("User List Transformed.");
            return users;
        }


        public override async Task<string> Get(int id)
        {
            var responseBody = await _httpClient.GetSingleUser(_configuration["ReqRes:Path"], id);
            return responseBody;
        }

        public override DataProcessorService ExtractUser(string json)
        {
            var user = JsonSerializer.Deserialize<UserResponse>(json);
            source = new List<ExpandoObject>();
            source.Add(user.data);

            Log.Information("User Extracted.");
            return this;
        }

        public override User TransformUser()
        {
            dynamic source_record = source.FirstOrDefault();
            var user = new User();
            user.id = Convert.ToInt32(source_record.id.ToString());
            user.email = source_record.email.ToString();
            user.name = source_record.first_name.ToString() + " " + source_record.last_name.ToString();
            user.avatar = source_record.avatar.ToString();
            user.attendance = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss.000 tt");

            Log.Information("User Transformed.");
            return user;
        }
    }
}
