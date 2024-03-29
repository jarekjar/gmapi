﻿using GlobalMentalityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalMentalityAPI.Interfaces
{
    public interface IEmergencyRepository
    {
        Task<Emergency> GetByUserID(Guid? id);
    }
}
