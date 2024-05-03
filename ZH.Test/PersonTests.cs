using ZH.Gyakorlas.Feladat.Classes;
using ZH.Gyakorlas.Feladat.Exceptions;

namespace ZH.Test;

public class PersonTests
{
    PersonType person;


    [SetUp]
    public void Setup()
    {
        person = new PersonType(123456, "Asd János", 10, CountryType.Antegria);
    }

    [TestCase("Jorji Costava, 123456, 55, Obristan", "Jorji Costava", 123456, 55, CountryType.Obristan)]
    [TestCase("Zick Balázs, 420123, 69, Arstotzka", "Zick Balázs", 420123, 69, CountryType.Arstotzka)]
    public void CorrectParse(
     string input,
     string expectedName, 
     int expectedPassport,
     int expectedAge,
     CountryType expectedCountry
    )
    {
        PersonType person = PersonType.Parse(input);

        Assert.That(person, Is.Not.Null);
        Assert.That(person.Name, Is.EqualTo(expectedName));
        Assert.That(person.PassportNumber, Is.EqualTo(expectedPassport));
        Assert.That(person.Age, Is.EqualTo(expectedAge));
        Assert.That(person.Country, Is.EqualTo(expectedCountry));
    }


    [TestCase("Jorji Costava, 123456, 55 Obristan")]
    [TestCase("Jorji Costava, 1, 55 ,Obristan")]
    [TestCase("asd")]
    [TestCase("")]
    public void BadParse(string input)
    {
        Assert.Throws<PersonalInfoException>(()=>PersonType.Parse(input));
    }



    [TestCase("Asd János", 0)]
    [TestCase("A János", 1)]
    [TestCase("D János", -1)]
    public void CompareTest(object input, int expectedOrder)
    {
        Assert.That(person.CompareTo(input), Is.EqualTo(expectedOrder));
    }



}