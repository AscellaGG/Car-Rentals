using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{
    public int Id { get; set; }
    public IVehicle Vehicle { get; set; }
    public IPerson Customer { get; set; }
    public int KmRented { get; set; }
    public int? KmReturned { get; set; }
    public DateTime DateRented { get; set; }
    public DateTime? DateReturned { get; set; }
    public double? Cost { get; set; }
    public BookingSatus BookingSatus { get; set; }

    public Booking(int id, IVehicle vehicle, IPerson customer, int kmRented, DateTime dateRented)
    {
        Id = id;
        Vehicle = vehicle;
        Customer = customer;
        KmRented = kmRented;
        DateRented = dateRented;
        Cost = null;
        KmReturned = null;
        DateReturned = null;
        BookingSatus = default;
        if(BookingSatus == BookingSatus.Open)
        {
            vehicle.VehicleStatus = VehicleStatuses.Booked;
        }
        else
        {
            vehicle.VehicleStatus = VehicleStatuses.Available;
        }
    }

    public void Return(DateTime dateReturned, int kmReturned)
    {
        BookingSatus = BookingSatus.Closed;
        DateReturned = dateReturned;
        KmReturned= kmReturned;
        var daysRented = (dateReturned - DateRented).Days;
        Vehicle.VehicleStatus = VehicleStatuses.Available;

        Cost = daysRented * Vehicle.CostDay + (kmReturned-KmRented) * Vehicle.CostKm;
    }
}
