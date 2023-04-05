using NUnit.Framework;
using parking;
using System;

namespace Tests
{
    [TestFixture]
    public class ParkingLotTests
    {
        private IParkingLot _parkingLot;

        [SetUp]
        public void Setup()
        {
            _parkingLot = new ParkingLot();
        }

        [Test]
        public void TestParkCar_Success()
        {
            // Arrange
            string licensePlateNumber = "ABC-123";

            // Act
            _parkingLot.ParkCar(licensePlateNumber);

            // Assert
            Assert.AreEqual(1, _parkingLot.GetParkedCars().Count());
        }

        [Test]
        public void TestParkCar_DuplicateLicensePlate_Exception()
        {
            // Arrange
            string licensePlateNumber = "ABC-123";
            _parkingLot.ParkCar(licensePlateNumber);

            // Act & Assert
            Assert.Throws<LicencePlateException>(() => _parkingLot.ParkCar(licensePlateNumber));
        }

        [Test]
        public void TestUnparkCar_Success()
        {
            // Arrange
            string licensePlateNumber = "ABC-123";
            _parkingLot.ParkCar(licensePlateNumber);
            _parkingLot.PayParkedCar(licensePlateNumber, 20);

            // Act
            ParkedCar parkedCar = _parkingLot.UnparkCar(licensePlateNumber);

            // Assert
            Assert.AreEqual(0, _parkingLot.GetParkedCars().Count());
            Assert.AreEqual(licensePlateNumber, parkedCar.LicensePlate);
        }

        [Test]
        public void TestUnparkCar_ParkingNotPaid_Exception()
        {
            // Arrange
            var parkingLot = new ParkingLot();
            var licensePlateNumber = "ABC123";
            parkingLot.ParkCar(licensePlateNumber);

            // Act
            var exception = Assert.Throws<LicencePlateException>(() => parkingLot.UnparkCar(licensePlateNumber));

            // Assert
            Assert.AreEqual("Parking is not paid, Exit access is denied", exception?.Message);
        }
        [Test]
        public void TestGetPriceParkedCar_Success()
        {
            // Arrange
            var parkingLot = new ParkingLot();
            string licensePlateNumber = "ABC-123";
            parkingLot.ParkCar(licensePlateNumber);

            // Act
            ParkedCar parkedCar = parkingLot.GetParkedCars().First();
            parkedCar.DateOfEntry = DateTime.Now.AddHours(-1.5);
            int price = parkingLot.GetPriceParkedCar(licensePlateNumber);

            // Assert
            Assert.AreEqual(10 + 5, price);
        }
        [Test]
        public void TestGetPriceParkedCar_LicensePlateNotFound_Exception()
        {
            // Arrange
            IParkingLot parkingLot = new ParkingLot();
            string licensePlateNumber = "ABC-123";

            // Act + Assert
            Assert.Throws<LicencePlateException>(() => parkingLot.GetPriceParkedCar(licensePlateNumber));
        }
        [Test]
        public void TestPayParkedCar_Success()
        {
            // Arrange
            IParkingLot parkingLot = new ParkingLot();
            string licensePlateNumber = "ABC123";
            parkingLot.ParkCar(licensePlateNumber);
            int expectedPrice = parkingLot.GetPriceParkedCar(licensePlateNumber);

            // Act
            parkingLot.PayParkedCar(licensePlateNumber, expectedPrice);

            // Assert
            ParkedCar parkedCar = parkingLot.GetParkedCars().FirstOrDefault(c => c.LicensePlate == licensePlateNumber);
            Assert.IsNotNull(parkedCar);
            Assert.IsTrue(parkedCar?.ParkingPaid);
            Assert.AreEqual(expectedPrice, parkedCar?.PricePaid);
        }
        [Test]
        public void TestPayParkedCar_LicensePlateNotFound_Exception()
        {
            // Arrange
            var parkingLot = new ParkingLot();
            string licensePlateNumber = "1234";

            // Act and Assert
            Assert.Throws<LicencePlateException>(() => parkingLot.PayParkedCar(licensePlateNumber, 20));
        }
        [Test]
        public void TestPayParkedCar_InsufficientFunds_Exception()
        {
            // Arrange
            IParkingLot parkingLot = new ParkingLot();
            string licensePlateNumber = "ABC123";
            int money = 5;
            parkingLot.ParkCar(licensePlateNumber);

            // Act and assert
            Assert.Throws<InsufficienFundsException>(() => parkingLot.PayParkedCar(licensePlateNumber, money));
        }
        [Test]
        public void TestGetParkedCars_Success()
        {
            // Arrange
            IParkingLot parkingLot = new ParkingLot();
            string licensePlateNumber1 = "ABC123";
            string licensePlateNumber2 = "XYZ987";
            parkingLot.ParkCar(licensePlateNumber1);
            parkingLot.ParkCar(licensePlateNumber2);

            // Act
            var parkedCars = parkingLot.GetParkedCars();

            // Assert
            Assert.AreEqual(2, parkedCars.Count());
            Assert.IsTrue(parkedCars.Any(pc => pc.LicensePlate == licensePlateNumber1));
            Assert.IsTrue(parkedCars.Any(pc => pc.LicensePlate == licensePlateNumber2));
        }
    }
}