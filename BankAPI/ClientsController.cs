using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Models;

namespace BankAPI
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        ClientService clientService = new ClientService();

        //В контроллере реализуем методы:

        //получения клиента по идентификатору,
        [HttpGet("{ClientId}")]
        public IActionResult GetClient(int ClientId)
        {
            Client client = clientService.GetClient(ClientId);

            if (client == null)
            {
                return NotFound($"Client with ID {ClientId} not found");
            }
            return Ok(client); // Вернуть успешный статус HTTP
        }

        // получить всех клиентов
        [HttpGet]
        public IActionResult GetAllClients()
        {
            List<Client> clients = clientService.GetAllClients();

            if (clients == null)
            {
                return NotFound($"Clients not found");
            }
            return Ok(clients); // Вернуть успешный статус HTTP
        }
               

        //добавления нового клиента,
        [HttpPost]
        public IActionResult AddClient([FromBody] Client client)
        {
            clientService.AddClient(client);
            return Ok();
        }

        //изменение клиента по идентификатору,
        [HttpPatch("{clientId}")]
        public IActionResult UpdateClient(int clientId, [FromBody] Client client)
        {
            clientService.UpdateClient(clientId, client);
            return Ok();
        }

        //удаления клиента,
        [HttpDelete("{clientId}")]
        public IActionResult RemoveClient(int clientId)
        {
            Client client = clientService.GetClient(clientId);

            if (client != null)
            {
                clientService.RemoveClient(clientId);
                return Ok();
            }
            else
            {
                return NotFound($"Client with ID {clientId} not found");
            }
        }
    }
}
