using Car_Rental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Common.Classes;

public class Customer : IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Id { get; init; } // init - Kan endast sättas av konstruktorn, går inte att ändra

    public Customer(string firstName, string lastName, int id)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
    }
}
