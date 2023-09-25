global using NUnit.Framework;
using Models;
using Services.Exceptions;
using Services;
using Services.Storage;

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


    private ClientStorage storage;
    private ClientService service;

    [SetUp]
    public void Setup()
    {
        // Инициализируем ClientStorage и передаем его в ClientService
        storage = new ClientStorage();
        service = new ClientService(storage);
    }

    [Test]
    public void GetYoungestClient_NoClients_ReturnsNull()
    {
        // Arrange: Нет клиентов в хранилище
        // Act: Вызываем метод GetYoungestClient()
        Client youngestClient = service.GetYoungestClient();

        // Assert: Ожидаем, что результат будет равен null
        Assert.IsNull(youngestClient);
    }

    [Test]
    public void GetOldestClient_NoClients_ReturnsNull()
    {
        // Arrange: Нет клиентов в хранилище
        // Act: Вызываем метод GetOldestClient()
        Client oldestClient = service.GetOldestClient();

        // Assert: Ожидаем, что результат будет равен null
        Assert.IsNull(oldestClient);
    }

    [Test]
    public void GetAverageAge_NoClients_ReturnsZero()
    {
        // Arrange: Нет клиентов в хранилище
        // Act: Вызываем метод GetAverageAge()
        double averageAge = service.GetAverageAge();

        // Assert: Ожидаем, что результат будет равен 0
        Assert.AreEqual(0, averageAge);
    }
}