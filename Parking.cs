using System;
using System.Collections.Generic;
using System.Linq;

namespace parking
{
    public interface IParkingLot
    {
        IEnumerable<ParkedCar> GetParkedCars();
        void ParkCar(string licensePlateNumber);
        ParkedCar UnparkCar(string licensePlateNumber);
        int GetPriceParkedCar(string licencePlateNumber);
        void PayParkedCar(string licencePlateNumber, int money);
        int GetNumberOfFreeSpots();
    }

    public class ParkingLot : IParkingLot
    {
        private readonly Dictionary<string, ParkedCar> _parkedCars = new Dictionary<string, ParkedCar>();
        private readonly object _lock = new object();
        private readonly int MAX_PARKING_SPOTS = 10;

        public IEnumerable<ParkedCar> GetParkedCars()
        {
            return _parkedCars.Values!;
        }

        public void ParkCar(string licensePlateNumber)
        {
            lock(_lock)
            {
                if (_parkedCars.Count >= MAX_PARKING_SPOTS) {
                    throw new ParkingLotFullException("The parking lot is full");
                }
                if (_parkedCars.ContainsKey(licensePlateNumber)) {
                    throw new LicencePlateException("Car with the same license plate already exists");
                }
                _parkedCars.Add(licensePlateNumber, new ParkedCar {
                    LicensePlate = licensePlateNumber,
                    DateOfEntry = DateTime.Now,
                    ParkingPaid = false
                });
            }
        }

        public void PayParkedCar(string licencePlateNumber, int money)
        {
            if (!_parkedCars.ContainsKey(licencePlateNumber))
            {
                throw new LicencePlateException($"Parked car with licence plate {licencePlateNumber} not found.");
            }
            int price = GetPriceParkedCar(licencePlateNumber);
            if (money < price) {
                throw new InsufficienFundsException($"{money} is not enough money The parking price is {price}");
            }
            ParkedCar parkedCar = _parkedCars[licencePlateNumber];
            parkedCar.ParkingPaid = true;
            parkedCar.PricePaid = money;
            parkedCar.timeSpent = DateTime.Now - parkedCar.DateOfEntry;
        }

        public int GetPriceParkedCar(string licencePlateNumber)
        {
            if (!_parkedCars.ContainsKey(licencePlateNumber))
            {
                throw new LicencePlateException($"Parked car with licence plate {licencePlateNumber} not found.");
            }
            ParkedCar parkedCar = _parkedCars[licencePlateNumber];
            TimeSpan timeSpan = DateTime.Now - parkedCar.DateOfEntry;
            return 10 + (int)Math.Ceiling(timeSpan.TotalHours - 1) * 5;
        }

        public ParkedCar UnparkCar(string licensePlateNumber)
        {
            ParkedCar parkedCar;
            lock(_lock)
            {
                if (!_parkedCars.ContainsKey(licensePlateNumber)) {
                    throw new LicencePlateException("Car with this license plate does not exist");
                }
                if (!_parkedCars[licensePlateNumber].ParkingPaid) {
                    throw new LicencePlateException("Parking is not paid, Exit access is denied");
                }
                parkedCar = _parkedCars[licensePlateNumber];
                _parkedCars.Remove(licensePlateNumber);
            }
            return parkedCar;
        }
        public int GetNumberOfFreeSpots()
        {
            return MAX_PARKING_SPOTS - _parkedCars.Count;
        }
    }
}