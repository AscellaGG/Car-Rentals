using System.Linq.Expressions;
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Data.Interfaces;

public interface IData
{
    public List<T> Get<T>(Func<T, bool>? expression) where T : class;
    public T? Single<T>(Func<T, bool>? expression) where T : class;
    public void Add<T>(T item) where T : class;

    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }

    public void RentVehicle(IVehicle vehicle, IPerson customer);
    public void ReturnVehicle(IBooking booking, int kmReturned);

    public IEnumerable<IPerson> GetPersons();
    public IEnumerable<IBooking> GetBookings();
    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default);

    // Default Interface Methods
    //public string[] VehicleStatusNames => Retunera enum konstanterna
    //public string[] VehicleStatusNames => Retunera enum konstanterna
    //public VehicleTypes GetVehicleType(string name) => Retunera en enum konstants värde med hjälp av konstantens namn
}
