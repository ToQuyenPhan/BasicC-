﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileLibrary.BusinessObject;

namespace AutomobileLibrary.Repository
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();
        Car GetCarByID(int carID);
        void InsertCar(Car car);
        void DeleteCar(int carID);
        void UpdateCar(Car car);
    }
}
