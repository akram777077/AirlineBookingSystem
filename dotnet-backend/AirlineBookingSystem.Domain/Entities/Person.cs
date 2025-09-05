using System;
using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a person entity.
/// </summary>
public class Person
{
    /// <summary>
    /// Gets or sets the ID of the person.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    public required string FirstName { get; set; }
    /// <summary>
    /// Gets or sets the middle name of the person.
    /// </summary>
    public string? MidName { get; set; }
    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    public required string LastName { get; set; }
    /// <summary>
    /// Gets or sets the date of birth of the person.
    /// </summary>
    public DateTime DateOfBirth { get; set; }
    /// <summary>
    /// Gets or sets the phone number of the person.
    /// </summary>
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// Gets or sets the email of the person.
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Gets or sets the image path of the person.
    /// </summary>
    public string? ImagePath { get; set; }
    /// <summary>
    /// Gets or sets the ID of the gender of the person.
    /// </summary>
    public int GenderId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the address of the person.
    /// </summary>
    public int AddressId { get; set; }
    /// <summary>
    /// Gets or sets the gender of the person.
    /// </summary>
    public required Gender Gender { get; set; }
    /// <summary>
    /// Gets or sets the address of the person.
    /// </summary>
    public required Address Address { get; set; }
    /// <summary>
    /// Gets or sets the user associated with this person.
    /// </summary>
    public User? User { get; set; }
}
