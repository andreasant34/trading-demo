﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Models;

namespace Trading.Core.Interfaces
{
    public interface ITradingService
    {
        Task<IEnumerable<TradeDetails>> ListTradesByUserAsync(int userId);

        Task<int> CreateTradeAsync(TradeCreationDetails trade);
    }
}
