using KoalaKit.Caching;
using PTP.DistributedCash.Abstraction;

namespace PTP.DistributedCash
{
    public class CaahService : ICashService
    {
        private readonly ICache Icash;
        public CaahService(ICache Icash)
        {
            this.Icash = Icash;
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            return await Icash.GetAsync<T>(key);
        }

        public async Task SetAsync<T>(string key, T data)
        {
            await Icash.SetAsync(key, data);
        }
    }
}