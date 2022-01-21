using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using D5.Models;
using System.Globalization;

namespace D5.Controllers;

public class RookiesController : Controller
{
    static List<Person> persons = new List<Person>
    {
        new Person
        {
            FirstName = "Phuong",
            LastName = "Nguyen Nam",
            Gender = "Female",
            DateOfBirth = new DateTime(2001, 1, 22),
            PhoneNumber = "",
            BirthPlace = "Phu Tho",
            IsGraduated = false
        },
        new Person
        {
            FirstName = "Nam",
            LastName = "Nguyen Thanh",
            Gender = "Male",
            DateOfBirth = new DateTime(2001, 1, 20),
            PhoneNumber = "",
            BirthPlace = "Ha Noi",
            IsGraduated = false
        },
        new Person
        {
            FirstName = "Son",
            LastName = "Do Hong",
            Gender = "Male",
            DateOfBirth = new DateTime(2000, 11, 6),
            PhoneNumber = "",
            BirthPlace = "Ha Noi",
            IsGraduated = false
        },
        new Person
        {
            FirstName = "Huy",
            LastName = "Nguyen Duc",
            Gender = "Male",
            DateOfBirth = new DateTime(1996, 1, 26),
            PhoneNumber = "",
            BirthPlace = "Ha Noi",
            IsGraduated = false
        },
        new Person
        {
            FirstName = "Hoang",
            LastName = "Phuong Viet",
            Gender = "Male",
            DateOfBirth = new DateTime(1999, 2, 5),
            PhoneNumber = "",
            BirthPlace = "Ha Noi",
            IsGraduated = false
        },
        new Person
        {
            FirstName = "Long",
            LastName = "Lai Quoc",
            Gender = "Male",
            DateOfBirth = new DateTime(1997, 5, 30),
            PhoneNumber = "",
            BirthPlace = "Bac Giang",
            IsGraduated = false
        },
        new Person
        {
            FirstName = "Thanh",
            LastName = "Tran Chi",
            Gender = "Male",
            DateOfBirth = new DateTime(2000, 9, 18),
            PhoneNumber = "",
            BirthPlace = "Ha Noi",
            IsGraduated = false
        }
    };
    [Route("rookies/male")]
    [Route("rookies/male-members")]
    public IActionResult GetMaleMembers()
    {
        var results = from person in persons
                        where person.Gender == "Male"
                        select person;
            return Json(results);
    }
    [Route("rookies/oldest")]
    public IActionResult GetOldestMembers()
    {
        var maxAge = persons.Max(m => m.Age);
        var oldest = persons.First(m => m.Age == maxAge);
            return Json(oldest);
    }
    [Route("rookies/full-name")]
    public IActionResult GetFullname()
    {
        var fullNames = from person in persons
                        select person.FullName;
        return Json(fullNames);
    }
    [Route("rookies/split-people-by-birth-year")]
    public IActionResult SplitPeopleByBirthYear(int year)
    {
        var results = from person in persons
                      group person by person.DateOfBirth.Year.CompareTo(year) into grp
                      select new 
                      {
                          Key = grp.Key switch
                          {
                              -1 => $"Birth year less than {year}",
                              0 => $"Birth year equals than {year}",
                              1 => $"Birth year greater than {year}",
                              _ => string.Empty
                          },
                          Data = grp.ToList()
                      };
        return Json(results);
    }
    [Route("rookies/export")]
    public IActionResult Export()
    {
        var buffer = WriteCsvToMemory(persons); 
        var memoryStream = new MemoryStream(buffer);
        return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "people.csv" };
    }
    public byte[] WriteCsvToMemory(List<Person> records)
    {
        using (var stream = new MemoryStream())
        using (var writer = new StreamWriter(stream))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csvWriter.WriteRecords(records);
            writer.Flush();
            return stream.ToArray();
        }
    }
}