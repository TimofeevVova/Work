global using NUnit.Framework;
using Models;
using Services.Exceptions;
using Services;

[TestFixture]
public class ClientServiceTests
{
    // проверка создания нового клиента метода AddNewClient
    [Test]
    public void AddNewClient_ValidData_ReturnsClient()
    {
        string firstName = "Женя";
        string lastName = "Попович";
        DateTime dateOfBirth = new DateTime(1998, 5, 4);
        string address = "Бендеры";
        string passportData = "N22556045";

        Dictionary<Client, List<Account>> Data = ClientService.AddNewClient(firstName, lastName, dateOfBirth, address, passportData);

        // проверяем, что данные добавлены в словарь
        Assert.IsNotNull(Data);

        // создаем отдельного клиента и его аккаунты
        Client newClient = Data.Keys.FirstOrDefault();
        List<Account> accounts = Data.Values.FirstOrDefault();

        // проверка на null
        Assert.IsNotNull(newClient);
        Assert.IsNotNull(accounts);

        // проверка значений
        Assert.That(newClient.FirstName, Is.EqualTo(firstName));
        Assert.That(newClient.LastName, Is.EqualTo(lastName));
        Assert.That(newClient.DateOfBirth, Is.EqualTo(dateOfBirth));
        Assert.That(newClient.Address, Is.EqualTo(address));
        Assert.That(newClient.GetPasportData(), Is.EqualTo(passportData));
    }

    // проверка на возраст метода AddNewClient
    [Test]
    public void AddNewClient_InvalidAge_ReturnsEmptyData()
    {
        DateTime dateOfBirth = DateTime.Today.AddYears(-17);

        Dictionary<Client, List<Account>> Data = ClientService.AddNewClient("Женя", "Попович", dateOfBirth, "Бендеры");

        Assert.IsNotNull(Data);
        Assert.IsEmpty(Data);
    }

    // проверка на отсутствие паспортных жаных метода AddNewClient
    [Test]
    public void AddNewClient_MissingPassportData_ReturnsEmptyData()
    {
        DateTime dateOfBirth = new DateTime(1998, 5, 4);

        Dictionary<Client, List<Account>> Data = ClientService.AddNewClient("Женя", "Попович", dateOfBirth, "Бендеры");

        Assert.IsNotNull(Data);
        Assert.IsEmpty(Data);
    }
}