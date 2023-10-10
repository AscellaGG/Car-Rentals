
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
        return _db.GetBookings();
    }
}