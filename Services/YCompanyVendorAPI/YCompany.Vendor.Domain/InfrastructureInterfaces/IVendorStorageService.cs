﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCompany.Vendor.Domain.InfrastructureInterfaces
{
    public interface IVendorStorageService
    {
        Task<bool> CheckHealthAsync();
    }
}