global using NUnit.Framework;
using Models;
using Services.Exceptions;
using Services;
using Services.Storage;

[TestFixture]
public class ClientServiceTests
{
    // �������� �������� ������ ������� ������ AddNewClient
    [Test]
    public void AddNewClient_ValidData_ReturnsClient()
    {
        string firstName = "����";
        string lastName = "�������";
        DateTime dateOfBirth = new DateTime(1998, 5, 4);
        string address = "�������";
        string passportData = "N22556045";

        Dictionary<Client, List<Account>> Data = ClientService.AddNewClient(firstName, lastName, dateOfBirth, address, passportData);

        // ���������, ��� ������ ��������� � �������
        Assert.IsNotNull(Data);

        // ������� ���������� ������� � ��� ��������
        Client newClient = Data.Keys.FirstOrDefault();
        List<Account> accounts = Data.Values.FirstOrDefault();

        // �������� �� null
        Assert.IsNotNull(newClient);
        Assert.IsNotNull(accounts);

        // �������� ��������
        Assert.That(newClient.FirstName, Is.EqualTo(firstName));
        Assert.That(newClient.LastName, Is.EqualTo(lastName));
        Assert.That(newClient.DateOfBirth, Is.EqualTo(dateOfBirth));
        Assert.That(newClient.Address, Is.EqualTo(address));
        Assert.That(newClient.GetPasportData(), Is.EqualTo(passportData));
    }

    // �������� �� ������� ������ AddNewClient
    [Test]
    public void AddNewClient_InvalidAge_ReturnsEmptyData()
    {
        DateTime dateOfBirth = DateTime.Today.AddYears(-17);

        Dictionary<Client, List<Account>> Data = ClientService.AddNewClient("����", "�������", dateOfBirth, "�������");

        Assert.IsNotNull(Data);
        Assert.IsEmpty(Data);
    }

    // �������� �� ���������� ���������� ����� ������ AddNewClient
    [Test]
    public void AddNewClient_MissingPassportData_ReturnsEmptyData()
    {
        DateTime dateOfBirth = new DateTime(1998, 5, 4);

        Dictionary<Client, List<Account>> Data = ClientService.AddNewClient("����", "�������", dateOfBirth, "�������");

        Assert.IsNotNull(Data);
        Assert.IsEmpty(Data);
    }


    private ClientStorage storage;
    private ClientService service;

    [SetUp]
    public void Setup()
    {
        // �������������� ClientStorage � �������� ��� � ClientService
        storage = new ClientStorage();
        service = new ClientService(storage);
    }

    [Test]
    public void GetYoungestClient_NoClients_ReturnsNull()
    {
        // Arrange: ��� �������� � ���������
        // Act: �������� ����� GetYoungestClient()
        Client youngestClient = service.GetYoungestClient();

        // Assert: �������, ��� ��������� ����� ����� null
        Assert.IsNull(youngestClient);
    }

    [Test]
    public void GetOldestClient_NoClients_ReturnsNull()
    {
        // Arrange: ��� �������� � ���������
        // Act: �������� ����� GetOldestClient()
        Client oldestClient = service.GetOldestClient();

        // Assert: �������, ��� ��������� ����� ����� null
        Assert.IsNull(oldestClient);
    }

    [Test]
    public void GetAverageAge_NoClients_ReturnsZero()
    {
        // Arrange: ��� �������� � ���������
        // Act: �������� ����� GetAverageAge()
        double averageAge = service.GetAverageAge();

        // Assert: �������, ��� ��������� ����� ����� 0
        Assert.AreEqual(0, averageAge);
    }
}