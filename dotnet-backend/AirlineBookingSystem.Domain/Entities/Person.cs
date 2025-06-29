using System;
using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public string? MidName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? ImagePath { get; set; }
    public int GenderId { get; set; }
    public int AddressId { get; set; }
    public required Gender Gender { get; set; }
    public required Address Address { get; set; }
    public User? User { get; set; }
}
