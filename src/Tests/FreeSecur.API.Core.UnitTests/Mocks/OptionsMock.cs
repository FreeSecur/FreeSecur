using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.API.Core.UnitTests.Mocks
{
    public class OptionsMock<T> : IOptions<T>, IOptionsSnapshot<T>, IOptionsMonitor<T>
        where T : class
    {
        private readonly T _settings;

        public OptionsMock(T settings)
        {
            _settings = settings;
        }

        public T Value => _settings;
        public T CurrentValue => _settings;

        public T Get(string name) => _settings;

        public System.IDisposable OnChange(System.Action<T, string> listener)
        {
            throw new System.NotImplementedException();
        }

    }
}
