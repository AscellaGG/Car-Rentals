using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IVehicle
{
    public string RegNo { get; init; }
    public VehicleTypes VehicleType { get; init; }
    public string Make { get; init; }
    public int Odometer { get; set; }
    public double CostKm { get; set; }
    public int CostDay { get; set; }

    public VehicleStatuses VehicleStatus { get; set; }
}
