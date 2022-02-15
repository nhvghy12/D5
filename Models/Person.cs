using System.ComponentModel.DataAnnotations;

namespace D5.Models;
public class Person : IComparable
{
    [Required, MaxLength(50)]
    public string? FirstName {get;set;}
    [MaxLength(10)]
    public string? LastName {get;set;}
    public string? Gender {get;set;}
    public DateTime DateOfBirth {get;set;}
    public string? PhoneNumber {get;set;}
    public string? BirthPlace {get;set;}
    public int Age 
    {
        get 
        {
            return DateTime.Now.Year - DateOfBirth.Year;
        }
    }
    public bool IsGraduated {get;set;}
    public int TotalDays 
    {
        get  
        {
            return (int)(DateTime.Now - DateOfBirth).TotalDays;
        }
    }

    public int CompareTo(object? obj)
    {
        return TotalDays.CompareTo(((Person)obj).TotalDays);
    }
    
    public string FullName
    {
        get
        {
            return $"{LastName} {FirstName}";
        }
    }
}