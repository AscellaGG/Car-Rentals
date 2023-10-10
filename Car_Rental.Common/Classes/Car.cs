using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

public class Car : IVehicle
{
    public string RegNo { get; init; }
    public VehicleTypes VehicleType { get; init; }
    public string Make { get; init; }
    public int Odometer { get; set; }
    public double CostKm { get; set; }
    public int CostDay { get; set; }
    public VehicleStatuses VehicleStatus { get; set; }

    public Car(string regNo, VehicleTypes vehicleType, string make, int odometer, double costKm, int costDay, VehicleStatuses vehicleStatus) => 
        (RegNo, VehicleType, Make, Odometer, CostKm, CostDay, VehicleStatus) = (regNo, vehicleType, make, odometer, costKm, costDay, vehicleStatus);
}
