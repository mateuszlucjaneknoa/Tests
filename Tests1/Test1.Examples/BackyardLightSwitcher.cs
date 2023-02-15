using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Examples
{
    internal class BackyardLightSwitcher
    {
        public static BackyardLightSwitcher Instance { get; }

        static BackyardLightSwitcher()
        {
            Instance = new BackyardLightSwitcher();
        }

        internal void TurnOn()
        {
            HttpClient client = new HttpClient();
            client.PostAsync("http://afjgasdkjfgsdkfumygsdjfumysdgumyasdfg.com", new StringContent("on"));
        }

        internal void TurnOff()
        {
            HttpClient client = new HttpClient();
            client.PostAsync("http://afjgasdkjfgsdkfumygsdjfumysdgumyasdfg.com", new StringContent("off"));
        }
    }
}
