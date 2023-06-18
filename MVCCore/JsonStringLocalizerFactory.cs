using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace MVCCore
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IDistributedCache _cache;
        public JsonStringLocalizerFactory(IDistributedCache cashe)
        {
            _cache = cashe;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer(_cache);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalizer(_cache);
        }
    }
}
