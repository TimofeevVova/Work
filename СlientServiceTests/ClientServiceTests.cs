using Services;

namespace СlientServiceTests
{
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
        public void WritingOffMoney_Good()
        {
            // Arrange
            double balanse = clientService.GetBalanse(49);

            // Act
            clientService.WritingOffMoney(49, 100);
            double newBalanse = clientService.GetBalanse(49);

            // Assert
            Assert.AreEqual(balanse - 100, newBalanse);
        }

        
    }
}