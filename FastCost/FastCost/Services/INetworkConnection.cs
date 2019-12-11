using System;
using System.Collections.Generic;
using System.Text;

namespace FastCost.Services
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();
    }
}
