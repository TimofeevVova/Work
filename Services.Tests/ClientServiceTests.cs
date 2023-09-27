global using NUnit.Framework;
using Models;
using Services.Exceptions;
using Services;
using Services.Storage;
using Moq;
using System.Net;

[TestFixture]
public class ClientServiceTests
{
    private IClientStorage storage;

    [SetUp]
    public void Setup()
    {
        storage = new ClientStorage();
    }

    [Test]
    public void Add_NewClient_AddsClientToStorage()
    {
        // Arrange
        string firstName = "Игорь";
        string lastName = "Новиков";
        DateTime dateOfBirth = new DateTime(1990, 1, 1);
        string address = "Москва";
        string email = "igor2234@gmail.com";
        string phoneNumber = "1234567890";
        string PasportData = "88005553535";

        // Act
        IClientStorage storage = new ClientStorage();
        Client newClient = storage.Add(firstName, lastName, dateOfBirth, address, email, phoneNumber, PasportData);

        // Assert
        Assert.IsNotNull(newClient);
        Assert.AreEqual(firstName, newClient.FirstName);
        Assert.AreEqual(lastName, newClient.LastName);
        Assert.AreEqual(dateOfBirth, newClient.DateOfBirth);
        Assert.AreEqual(address, newClient.Address);
        Assert.AreEqual(email, newClient.Email);
        Assert.AreEqual(phoneNumber, newClient.PhoneNumber);
    }

    [Test]
    public void Update_ExistingClient_UpdatesClientData()
    {
        // Arrange
        IClientStorage storage = new ClientStorage();
        Client originalClient = storage.Add("Игорь", "Новиков", new DateTime(1990, 1, 1), "Москва", "igor2234@gmail.com", "1234567890", "23423423424");
        string newFirstName = "Никита";
        string newLastName = "НеНовиков";
        DateTime newDateOfBirth = new DateTime(1985, 2, 15);
        string newAddress = "Питер";
        string newEmail = "nekit2236@gmail.com";
        string newPhoneNumber = "9876543210";
        string PasportData = "9876543210";

        // Act
        Client updatedClient = storage.Update(originalClient, newFirstName, newLastName, newDateOfBirth, newAddress, newEmail, newPhoneNumber, PasportData);

        // Assert
        Assert.AreEqual(newFirstName, updatedClient.FirstName);
        Assert.AreEqual(newLastName, updatedClient.LastName);
        Assert.AreEqual(newDateOfBirth, updatedClient.DateOfBirth);
        Assert.AreEqual(newAddress, updatedClient.Address);
        Assert.AreEqual(newEmail, updatedClient.Email);
        Assert.AreEqual(newPhoneNumber, updatedClient.PhoneNumber);
    }

    [Test]
    public void DeleteClient_ValidClient_DeletesClient()
    {
        // Arrange
        var storageMock = new Mock<IClientStorage>();
        var clientService = new ClientService(storageMock.Object);
        var clientToDelete = new Client
        {
            FirstName = "Игорь",
            LastName = "Новиков",
            DateOfBirth = new DateTime(1990, 1, 1),
            Address = "Москва",
            Email = "igor2234@gmail.com",
            PhoneNumber = "1234567890",
        };

        // Act
        storageMock.Setup(storage => storage.Delete(clientToDelete));
        storageMock.Object.Delete(clientToDelete);

        // Assert
        storageMock.Verify(storage => storage.Delete(clientToDelete), Times.Once);
    }

    [Test]
    public void AddAccount_ValidClient_AddsAccount()
    {
        // Arrange
        IClientStorage storage = new ClientStorage();
        Client client = storage.Add("Игорь", "Новиков", new DateTime(1990, 1, 1), "Москва", "igor2234@gmail.com", "1234567890", "23423423424");

        // Act
        Dictionary<Client, List<Account>> data = storage.AddAccount(client);
        List<Account> accounts = data[client];

        // Assert
        Assert.AreEqual(1, accounts.Count);
    }

    [Test]
    public void UpdateAccount_ValidClientAndAccount_UpdatesAccount()
    {
        // Arrange
        IClientStorage storage = new ClientStorage();
        Client client = storage.Add("Игорь", "Новиков", new DateTime(1990, 1, 1), "Москва", "igor2234@gmail.com", "1234567890", "23423423424");
        Dictionary<Client, List<Account>> data = storage.AddAccount(client);
        List<Account> accounts = data[client];
        Account account = accounts.First();

        // Act
        Dictionary<Client, List<Account>> updatedData = storage.UpdateAccount(client, account.AccountId, 1000);

        // Assert
        Account updatedAccount = updatedData[client].First();
        Assert.AreEqual(1000, updatedAccount.Amount);
    }

    [Test]
    public void DeleteAccount_ValidClientAndAccount_DeletesAccount()
    {
        // Arrange
        IClientStorage storage = new ClientStorage();
        Client client = storage.Add("Игорь", "Новиков", new DateTime(1990, 1, 1), "Москва", "igor2234@gmail.com", "1234567890", "23423423424");
        Dictionary<Client, List<Account>> data = storage.AddAccount(client);
        List<Account> accounts = data[client];
        Account account = accounts.First();

        // Act
        Client updatedClient = storage.DeleteAccount(client, account);

        // Assert
        Assert.IsFalse(updatedClient.IdAccounts.Contains(account.AccountId));
    }
}