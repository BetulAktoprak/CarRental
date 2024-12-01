﻿using CarRental.Core.Entities;
using CarRental.Core.ViewModels;

namespace CarRental.Core.Services;
public interface ICarService : IService<Car>
{
    Task<List<CarWorkTimeViewModel>> GetAdminReportAsync(DateTime startDate, DateTime endDate);
}
