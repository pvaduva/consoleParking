using System;
using parking;

namespace parking
{
    class Program
    {
        static void Main(string[] args)
        {
            IParkingLot parkingLot = new ParkingLot();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Parking Lot Menu:");
                Console.WriteLine("1. View parked cars");
                Console.WriteLine("2. Park a car");
                Console.WriteLine("3. Unpark a car");
                Console.WriteLine("4. Pay for parking");
                Console.WriteLine("5. Exit");

                Console.Write("Enter option number: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Parked cars:");
                        foreach (ParkedCar car in parkingLot.GetParkedCars())
                        {
                            Console.WriteLine($"License plate: {car.LicensePlate} Date of entry: {car.DateOfEntry} Parking paid: {car.ParkingPaid}");
                        }
                        break;
                    case "2":
                        Console.Write("Enter license plate number: ");
                        string licensePlate = Console.ReadLine();
                        try
                        {
                            parkingLot.ParkCar(licensePlate);
                            Console.WriteLine($"Car with license plate {licensePlate} has been parked.");
                        }
                        catch (LicencePlateException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "3":
                        Console.Write("Enter license plate number: ");
                        string licensePlateUnpark = Console.ReadLine();
                        try
                        {
                            ParkedCar unparkedCar = parkingLot.UnparkCar(licensePlateUnpark);
                            Console.WriteLine($"Car with license plate {licensePlateUnpark} has been unparked. Parking duration: {unparkedCar.timeSpent} Price paid: {unparkedCar.PricePaid}");
                        }
                        catch (LicencePlateException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "4":
                        Console.Write("Enter license plate number: ");
                        string licensePlatePay = Console.ReadLine();
                        Console.Write("Enter amount of money: ");
                        string moneyInput = Console.ReadLine();
                        int money;
                        if (int.TryParse(moneyInput, out money))
                        {
                            try
                            {
                                parkingLot.PayParkedCar(licensePlatePay, money);
                                Console.WriteLine($"Parking for car with license plate {licensePlatePay} has been paid.");
                            }
                            catch (LicencePlateException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (InsufficienFundsException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Amount of money must be an integer.");
                        }
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}