using System.Linq.Expressions;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Data.Interfaces;

public interface IData
{
    public List<T> Get<T>(List<T> list, Func<T, bool>? expression = null) where T : class;
    T? Single<T>(Expression<Func<T, bool>>? expression);

    public List<IBooking> bookings { get; set; }

    public void Add<T>(T item);
    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }

    IBooking RentVehicle(int vehicleId, int customerId);
    IBooking ReturnVehicle(int vehicleId);

    public IEnumerable<IPerson> GetPersons();
    public IEnumerable<IBooking> GetBookings();
    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default);

    // Default Interface Methods
    //public string[] VehicleStatusNames => Retunera enum konstanterna
    //public string[] VehicleStatusNames => Retunera enum konstanterna
    //public VehicleTypes GetVehicleType(string name) => Retunera en enum konstants värde med hjälp av konstantens namn
}
