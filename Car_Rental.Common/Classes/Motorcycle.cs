using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

public class Motorcycle : IVehicle
{
    public string RegNo { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
    public VehicleTypes VehicleType { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
    public string Make { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
    public int Odometer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public double CostKm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int CostDay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public VehicleStatuses VehicleStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
