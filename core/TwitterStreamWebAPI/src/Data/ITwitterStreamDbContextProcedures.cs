﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public partial interface ITwitterStreamDbContextProcedures
    {
        Task<List<SpGetTop10HashTagsByHourWindowResult>> SpGetTop10HashTagsByHourWindowAsync(int? hourWindow, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
