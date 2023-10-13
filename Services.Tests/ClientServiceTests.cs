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
    ClientService clientService = new ClientService();

    [Test]
    public void AddBalanse_Good()
    {
        // Arrange
        double balanse = clientService.GetBalanse(49);

        // Act
        clientService.AddBalanse(49, 100);
        double newBalanse = clientService.GetBalanse(49);

        // Assert
        Assert.AreEqual(balanse + 100, newBalanse);
    }

    [Test]
    public void AddBalanse_Mines()
    {
        // Arrange
        double balanse = clientService.GetBalanse(49);

        // Act
        clientService.AddBalanse(49, -100);
        double newBalanse = clientService.GetBalanse(49);

        // Assert
        Assert.AreEqual(balanse + 100, newBalanse);

    }


    [Test]
    public void Add()
    {
        // Arrange
        double balanse = 4;

        // Act        
        double newBalanse = 5;

        // Assert
        Assert.AreEqual(balanse + 1, newBalanse);

    }
}