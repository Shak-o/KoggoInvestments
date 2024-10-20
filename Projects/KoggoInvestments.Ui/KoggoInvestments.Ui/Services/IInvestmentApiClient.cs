﻿using KoggoInvestments.Ui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KoggoInvestments.Ui.Services
{
    public interface IInvestmentApiClient
    {
        Task<List<CheckStatusResponse>> GetMarketInfoAsync();
    }
}
