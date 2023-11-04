using Car_Rental.Common.Enums;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using System.Linq.Expressions;
using System.Linq;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    readonly List<IPerson> _customers = new();
    readonly List<IBooking> _bookings = new();
    readonly List<IVehicle> _vehicles = new();

    public List<IBooking> bookings { get; set; }

    public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;
    public int NextPersonId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;
    public int NextBookingId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;

    public CollectionData()
    {
        bookings = new List<IBooking>();
        SeedData();

    }

    void SeedData() //Add items to lists
    {
        _vehicles.Add(new Car(NextBookingId, "ABC123", VehicleTypes.Combi, "Volvo", 10000, 1, 200, VehicleStatuses.Available));
        _vehicles.Add(new Car(NextVehicleId,"GHI537", VehicleTypes.Sedan, "Saab", 20000, 1, 100, VehicleStatuses.Booked));
        _vehicles.Add(new Car(NextVehicleId,"JFI236", VehicleTypes.Sedan, "Tesla", 1000, 3, 100, VehicleStatuses.Booked));
        _vehicles.Add(new Car(NextVehicleId,"ORT372", VehicleTypes.Van, "Jeep", 5000, 1.5, 300, VehicleStatuses.Available));
        _vehicles.Add(new Motorcycle(NextVehicleId, "OWN472", VehicleTypes.Motorcycle, "Yamaha", 30000, 0.5, 50, VehicleStatuses.Available));

        _customers.Add(new Customer(NextPersonId, "Jane", "Doe", 24467));
        _customers.Add(new Customer(NextPersonId, "John", "Doe", 23457));
        _customers.Add(new Customer(NextPersonId, "Bella", "Goth", 35734));
        _customers.Add(new Customer(NextPersonId, "Don", "Lothario", 54841));

        bookings.Add(new Booking(_vehicles[0], _customers[0], _vehicles[0].Odometer, new DateTime(2023, 9, 9))); //Get customer by ID?
        bookings.Add(new Booking(_vehicles[1], _customers[1], _vehicles[1].Odometer, new DateTime(2023, 9, 9))); 
        bookings[^1].ReturnVehicle(new DateTime(2023, 9, 10), 23000);
        bookings.Add(new Booking(_vehicles[4], _customers[^1], _vehicles[4].Odometer, new DateTime(2023, 9, 1)));
        bookings[^1].ReturnVehicle(new DateTime(2023, 9, 6), 35000);
    }

    public void AddVehicle(string regNo,VehicleTypes type, string make, int odometer, double costKm, int costDay)
    {
        if(type == VehicleTypes.Motorcycle)
        {
            _vehicles.Add(new Motorcycle(NextVehicleId, regNo, type, make, odometer, costKm, costDay, VehicleStatuses.Available));
        }
        else
        {
            _vehicles.Add(new Car(NextVehicleId, regNo, type, make, odometer, costKm, costDay, VehicleStatuses.Available));
        }
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



    public List<T> Get<T>(List<T> list, Func<T, bool>? expression = null) where T : class
    {
        // expression: p => p.Id > 10

        if (expression is null) return list;

        var result = list.Where(expression).ToList();
        if (result.Count == 0) return list;

        return result;
    }

    public T? Single<T>(Expression<Func<T, bool>>? expression)
    {
        throw new NotImplementedException();
    }

    public void Add<T>(T item)
    {
        throw new NotImplementedException();
    }

    public IBooking RentVehicle(int vehicleId, int customerId)
    {
        throw new NotImplementedException();
    }

    public IBooking ReturnVehicle(int vehicleId)
    {
        throw new NotImplementedException();
    }
}