using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Vehicle : IVehicle
{
    public int Id { get; set;}
    public string RegNo { get; init; }
    public VehicleTypes VehicleType { get; init; }
    public string Make { get; init; }
    public int Odometer { get; set; }
    public double CostKm { get; set; }
    public int CostDay { get; set; }
    public VehicleStatuses VehicleStatus { get; set; }

    public Vehicle(int id, string regNo, VehicleTypes vehicleType, string make, int odometer, double costKm, int costDay) =>
    (Id, RegNo, VehicleType, Make, Odometer, CostKm, CostDay, VehicleStatus) = (id, regNo, vehicleType, make, odometer, costKm, costDay, VehicleStatuses.Available);
}
