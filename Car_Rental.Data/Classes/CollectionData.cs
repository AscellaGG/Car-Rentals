using Car_Rental.Common.Enums;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Car_Rental.Common.Extensions;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    private readonly List<IPerson> _customers = new();
    private readonly List<IBooking> _bookings = new();
    private readonly List<IVehicle> _vehicles = new();

    public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;
    public int NextPersonId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;
    public int NextBookingId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;

    public CollectionData() => SeedData();

    void SeedData() //Fake database
    {
        _vehicles.Add(new Car(NextVehicleId, "ABC123", VehicleTypes.Combi, "Volvo", 10000, 1, 200));
        _vehicles.Add(new Car(NextVehicleId,"GHI537", VehicleTypes.Sedan, "Saab", 20000, 1, 100));
        _vehicles.Add(new Car(NextVehicleId,"JFI236", VehicleTypes.Sedan, "Tesla", 1000, 3, 100));
        _vehicles.Add(new Car(NextVehicleId,"ORT372", VehicleTypes.Van, "Jeep", 5000, 1.5, 300));
        _vehicles.Add(new Motorcycle(NextVehicleId, "OWN472", VehicleTypes.Motorcycle, "Yamaha", 30000, 0.5, 50));

        _customers.Add(new Customer(NextPersonId, "Jane", "Doe", 24467));
        _customers.Add(new Customer(NextPersonId, "John", "Doe", 23457));
        _customers.Add(new Customer(NextPersonId, "Bella", "Goth", 35734));
        _customers.Add(new Customer(NextPersonId, "Don", "Lothario", 54841));

        _bookings.Add(new Booking(NextBookingId, _vehicles[0], _customers[0], _vehicles[0].Odometer, new DateTime(2023, 10, 9))); //Get customer by ID?
        _bookings.Add(new Booking(NextBookingId, _vehicles[1], _customers[1], _vehicles[1].Odometer, new DateTime(2023, 11, 2))); 
        _bookings[^1].Return(new DateTime(2023, 11, 10), 23000);
        _bookings.Add(new Booking(NextBookingId, _vehicles[4], _customers[^1], _vehicles[4].Odometer, new DateTime(2023, 11, 1)));
        _bookings[^1].Return(new DateTime(2023, 11, 6), 35000);
    }

    public List<T> Reflection<T>() where T : class
    {
        var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
                ?? throw new InvalidOperationException("Unsupported type");

        var value = collections.GetValue(this) ?? throw new InvalidDataException();

        return (List<T>)value;
    }

    public List<T> Get<T>(Func<T, bool>? expression) where T : class
    {
        var collection = Reflection<T>().AsQueryable();

        if (expression is null) return collection.ToList();

        return collection.Where(expression).ToList();
    }

    public T? Single<T>(Func<T, bool>? expression) where T: class
    {
        var collection = Reflection<T>().AsQueryable();

        if (expression is null) return null;

        return collection.SingleOrDefault(expression);
    }

    public void Add<T>(T item) where T : class
    {
        Reflection<T>().Add(item);
    }

    public IBooking RentVehicle(IVehicle vehicle, IPerson customer)
    {
        Add<IBooking>(new Booking(NextBookingId, vehicle, customer, vehicle.Odometer, DateTime.Now));
        _vehicles.Single(v => v.Id.Equals(vehicle.Id)).VehicleStatus = VehicleStatuses.Booked;
        return Single<IBooking>(b => b.Id == vehicle.Id);
    }


    public IBooking ReturnVehicle(IBooking booking, int distanceReturned)
    {
        if(distanceReturned < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        var vehicle = Single<IVehicle>(v => v.Id == booking.Vehicle.Id);

        booking.BookingSatus = BookingSatus.Closed;
        booking.DateReturned = DateTime.Now;
        booking.KmReturned = vehicle.Odometer + distanceReturned;
        var daysRented = DateTime.Now.Duration(booking.DateRented);

        booking.Cost = (daysRented * booking.Vehicle.CostDay) + (booking.KmReturned * booking.Vehicle.CostKm);

        vehicle.Odometer = (int)booking.KmReturned;
        vehicle.VehicleStatus = VehicleStatuses.Available;

        return booking;

    }    
}
