
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    //Get data and return to main

    private readonly IData _db;

    public int SelectedSSN { get; set; }

    public int ReturnedKM { get; set; }

    public BookingProcessor(IData db) => _db = db;

    public IEnumerable<IPerson> GetCustomers()
    {
        return _db.GetPersons();
    }

    public IPerson GetPerson(int ssn)
    {
        var customer = _db.Single<IPerson>(c => c.SSN == ssn);
        return customer;
    }

    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
    {
        //return _db.GetVehicles(status);
        return _db.Get<IVehicle>(v => v != null);
    }

    public IEnumerable<IBooking> GetBookings()
    {
        var bookings = _db.Get<IBooking>(b => b != null);
        return bookings.OrderByDescending(b => b.DateRented);
    }
    public IBooking GetBooking(int vehicleID)
    {
        return _db.Single<IBooking>(b => b.Vehicle.Id == vehicleID);
    }

    public void AddVehicle(NewVehicle vehicle)
    {
        if (vehicle.Type == VehicleTypes.Motorcycle)
        {
            _db.Add<IVehicle>(new Motorcycle(_db.NextVehicleId, vehicle.RegNo, vehicle.Type, vehicle.Make, vehicle.Odometer, vehicle.CostKm, vehicle.CostDay, VehicleStatuses.Available));
        }
        else
        {
            _db.Add<IVehicle>(new Car(_db.NextVehicleId, vehicle.RegNo, vehicle.Type, vehicle.Make, vehicle.Odometer, vehicle.CostKm, vehicle.CostDay, VehicleStatuses.Available));
        }
    }

    public void AddPerson(NewPerson person)
    {
        _db.Add<IPerson>(new Customer(_db.NextPersonId, person.FirstName, person.LastName, person.SSN));
    }

    public void Rent(IPerson customer, IVehicle vehicle)
    {
        _db.RentVehicle(vehicle, customer);
    }

    public void ReturnVehicle(IVehicle vehicle)
    {
        _db.ReturnVehicle(GetBooking(vehicle.Id), ReturnedKM);
    }
}

public class NewVehicle
{
    public string RegNo { get; set; }
    public VehicleTypes Type { get; set; }
    public string Make { get; set; }
    public int Odometer { get; set; }
    public double CostKm { get; set; }
    public int CostDay { get; set; }
}

public class NewPerson
{
    public int SSN { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; } 
}