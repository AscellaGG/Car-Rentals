
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;
    

    public string VehicleError = string.Empty;
    public string CustomerError = string.Empty;
    public int SelectedSSN { get; set; }
    public int? DistanceReturned { get; set; }
    public bool InputDisabled { get; set; }

    public BookingProcessor(IData db) => _db = db;

    public IEnumerable<IBooking> GetBookings()
    {
        var bookings = _db.Get<IBooking>(b => b != null);
        return bookings.OrderByDescending(b => b.DateRented);
    }

    public IEnumerable<IPerson> GetCustomers()
    {
        return _db.Get<IPerson>(c => c != null);
    }

    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
    {
        //return _db.GetVehicles(status);
        return _db.Get<IVehicle>(v => v != null);
    }

    public IPerson GetPerson(int ssn)
    {
        var customer = _db.Single<IPerson>(c => c.SSN == ssn);
        return customer;
    }

    public IVehicle GetVehicle(int vehicleId) => _db.Single<IVehicle>(v => v.Id.Equals(vehicleId));
    
    public IBooking GetBooking(int vehicleID)
    {
        return _db.Single<IBooking>(b => b.Vehicle.Id == vehicleID);
    }

    public void AddVehicle(NewVehicle vehicle)
    {
        try
        {
            if (vehicle.Type == VehicleTypes.Motorcycle)
            {
                _db.Add<IVehicle>(new Motorcycle(_db.NextVehicleId, vehicle.RegNo, vehicle.Type, vehicle.Make, vehicle.Odometer, vehicle.CostKm, vehicle.CostDay));
            }
            else
            {
                _db.Add<IVehicle>(new Car(_db.NextVehicleId, vehicle.RegNo, vehicle.Type, vehicle.Make, vehicle.Odometer, vehicle.CostKm, vehicle.CostDay));
            }
            VehicleError = string.Empty;
        }
        catch (Exception ex)
        {
            VehicleError = ex.Message.ToString();
        }
    }

    public void AddPerson(NewPerson person)
    {
        try
        {
            _db.Add<IPerson>(new Customer(_db.NextPersonId, person.FirstName, person.LastName, person.SSN));
            CustomerError = string.Empty;
        }
        catch(Exception ex) 
        {
            CustomerError = ex.Message.ToString();
        }
    }

    public async Task<IBooking> RentVehicle(IPerson customer, IVehicle vehicle)
    {
        InputDisabled = true;
        await Task.Delay(500);
        InputDisabled = false;

        return _db.RentVehicle(vehicle, customer);
    }

    public async Task<IBooking> ReturnVehicle(IVehicle vehicle)
    {
        try
        {
            InputDisabled = true;
            await Task.Delay(1000);
            InputDisabled = false;

            VehicleError = string.Empty;
            return _db.ReturnVehicle(GetBooking(vehicle.Id), (int)DistanceReturned);
        }
        catch (Exception ex)
        {
            VehicleError = "Distance can not be empty or negative!";
            return null;
        }
    }

    public string[] VehicleStatusNames => _db.VehicleStatusNames();
    public string[] VehicleTypeNames => _db.VehicleTypeNames();
    public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);
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