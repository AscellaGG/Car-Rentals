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

    public IBooking RentVehicle(IVehicle vehicle, IPerson customer);
    public IBooking ReturnVehicle(int vehicleID, int kmReturned);

    // Default Interface Methods
    public string[] VehicleStatusNames() => Enum.GetNames(typeof(VehicleStatuses));
    public string[] VehicleTypeNames() => Enum.GetNames(typeof(VehicleTypes));
    public VehicleTypes GetVehicleType(string name) => (VehicleTypes)Enum.Parse(typeof(VehicleTypes), name);
}
