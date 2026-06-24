using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LekuTrans.Data.Models;
using LekuTrans.Data.Enums;
using LekuTrans.Data.Repositories;

namespace LekuTrans.Services
{
    internal class VehicleService
    {
        private readonly IRepository<Vehicle> _repository;

        public VehicleService(IRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        public async Task<Vehicle> AddVehicle(string licensePlate, string model, decimal capacityKg, decimal capacityM3, VehicleStatus vehicleStatus)
        {
            Vehicle vehicle = new Vehicle()
            {
                LicensePlate = licensePlate,
                Model = model,
                CapacityKg = capacityKg,
                CapacityM3 = capacityM3,
                Status = vehicleStatus
            };

            await _repository.CreateAsync(vehicle);
            await _repository.SaveAsync();

            return vehicle;
        }

         public async Task<IEnumerable<Vehicle>> GetAll()
        {
            IEnumerable<Vehicle> vehicles = await _repository.GetAllAsync();

            return vehicles;
        }

        public async Task<IEnumerable<Vehicle>> GetAvailable()
        {
            IEnumerable<Vehicle> vehicles = await _repository.GetAllAsync();

            List<Vehicle> resultList = new List<Vehicle>();

            foreach (Vehicle vehicle in vehicles)
            {
                if(vehicle.Status == VehicleStatus.Свободен)
                {
                    resultList.Add(vehicle);
                }
            }

            return resultList;

        }

        public async Task<Vehicle> UpdateStatus(long id, VehicleStatus newStatus)
        {
            Vehicle vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
            {
                throw new InvalidOperationException($"Машина с ID {id} не найдена.");
            }

            vehicle.Status = newStatus;

            _repository.Update(vehicle);

            await _repository.SaveAsync();

            return vehicle;
        }

        public async Task Delete(long id)
        {
            Vehicle vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
            {
                throw new InvalidOperationException($"Машина с ID {id} не найдена.");
            }

            _repository.Delete(id);

            await _repository.SaveAsync();
        }
    }
}
