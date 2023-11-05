﻿using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IBooking
{
    public int Id { get; set;}
    public IVehicle Vehicle { get; set; }
    public IPerson Customer { get; set; }
    public int KmRented { get; set; }
    public int? KmReturned { get; set; }

    public DateTime DateRented { get; set; }
    public DateTime? DateReturned { get; set; }
    public double? Cost {  get; set; }
    public BookingSatus BookingSatus { get; set; }

    public void Return(DateTime dateReturned, int kmReturned);
}