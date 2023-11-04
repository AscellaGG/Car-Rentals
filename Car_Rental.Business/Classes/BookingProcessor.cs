
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    //Get data and return to main

    private readonly IData _db;

    public BookingProcessor(IData db) => _db = db; 
    
    public IEnumerable<IPerson> GetCustomers()
    {
        return _db.GetPersons();
    }

    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
    {
        return _db.GetVehicles(status);
    }

    public IEnumerable<IBooking> GetBookings()
    {
        return _db.Get<IBooking>(_db.bookings, c => c.BookingSatus.Equals(BookingSatus.Open));
    }

    public void AddVehicle(NewVehicle vehicle)
    {
        //_db.AddVehicle(vehicle.regNo,vehicle.type,vehicle.make,vehicle.odometer,vehicle.costKm,vehicle.costDay);
        
    }

    public void Rent()
    {

    }
}

public class NewVehicle
{
    public string regNo { get; set; }
    public VehicleTypes type { get; set; }
    public string make { get; set; }
    public int odometer { get; set; }
    public double costKm { get; set; }
    public int costDay { get; set; }
}