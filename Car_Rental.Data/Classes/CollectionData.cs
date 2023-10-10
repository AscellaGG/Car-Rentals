using Car_Rental.Common.Enums;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    readonly List<IPerson> _customers = new();
    readonly List<IBooking> _bookings = new();
    readonly List<IVehicle> _vehicles = new();

    public CollectionData() => SeedData();

    void SeedData() //Add items to lists
    {
        _vehicles.Add(new Car("ABC123", VehicleTypes.Combi, "Volvo", 10000, 1, 200, VehicleStatuses.Available));
        _vehicles.Add(new Car("GHI537", VehicleTypes.Sedan, "Saab", 20000, 1, 100, VehicleStatuses.Booked));
        _vehicles.Add(new Car("JFI236", VehicleTypes.Sedan, "Tesla", 1000, 3, 100, VehicleStatuses.Booked));
        _vehicles.Add(new Car("ORT372", VehicleTypes.Van, "Jeep", 5000, 1.5, 300, VehicleStatuses.Available));
        _vehicles.Add(new Car("OWN472", VehicleTypes.Motorcycle, "Yamaha", 30000, 0.5, 50, VehicleStatuses.Available));

        _customers.Add(new Customer("Jane", "Doe", 24467));
        _customers.Add(new Customer("John", "Doe", 23457));
        _customers.Add(new Customer("Bella", "Goth", 35734));
        _customers.Add(new Customer("Don", "Lothario", 54841));

        _bookings.Add(new Booking(_vehicles[0], _customers[0], _vehicles[0].Odometer, new DateTime(2023, 9, 9))); //Get customer by ID?
        _bookings.Add(new Booking(_vehicles[1], _customers[1], _vehicles[1].Odometer, new DateTime(2023, 9, 9))); 
        _bookings[^1].ReturnVehicle(new DateTime(2023, 9, 10), 23000);
        _bookings.Add(new Booking(_vehicles[4], _customers[^1], _vehicles[4].Odometer, new DateTime(2023, 9, 1)));
        _bookings[^1].ReturnVehicle(new DateTime(2023, 9, 6), 35000);
    }

    public IEnumerable<IPerson> GetPersons() => _customers;
    public IEnumerable<IBooking> GetBookings() => _bookings;
    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
    {
        if (status != default)
        {
            return _vehicles.Where(v => v.VehicleStatus.Equals(status));
        }

        return _vehicles;
    }
}