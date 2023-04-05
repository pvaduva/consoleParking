namespace parking{

    public class ParkedCar{
    public string? LicensePlate { get; set; }
    public DateTime DateOfEntry { get; set; }
    public bool ParkingPaid { get; set; }
    public int PricePaid { get; set; }
    public TimeSpan timeSpent { get; set; }

    }
}