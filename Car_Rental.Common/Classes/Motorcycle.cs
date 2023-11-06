using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Motorcycle : Vehicle
{   
    public Motorcycle(int id, string regNo, VehicleTypes vehicleType, string make, int odometer, double costKm, int costDay) : base(id, regNo, vehicleType, make, odometer, costKm, costDay)
    {}
}
