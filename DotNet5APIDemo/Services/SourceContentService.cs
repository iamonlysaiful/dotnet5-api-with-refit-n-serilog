using DotNet5APIDemo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace DotNet5APIDemo.Services
{
    public abstract class SourceContentService
    {
        public SourceContentService()
        {
            Timezone = TZConvert.GetTimeZoneInfo("Singapore Standard Time");
        }

        protected TimeZoneInfo Timezone { get; init; }

        public abstract Task<string> GetList(int pageNumber);
        public abstract Task<string> Get(int id);

        public abstract DataProcessorService ExtractUserList(string json);
        public abstract DataProcessorService ExtractUser(string json);

        public abstract IEnumerable<User> TransformUserList();
        public abstract User TransformUser();
    }
}
