﻿using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }
        //В контроллере реализуем методы:

        //получения клиента по идентификатору,
        [HttpGet("{ClientId}")]
        public IActionResult GetClient(int ClientId)
        {
            Client client = _clientService.GetClient(ClientId);

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
            List<Client> clients = _clientService.GetAllClients();

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
            _clientService.AddClient(client);
            return Ok();
        }

        //изменение клиента по идентификатору,
        [HttpPatch("{ClientId}")]
        public IActionResult UpdateClient(int clientId, [FromBody] Client client)
        {
            _clientService.UpdateClient(clientId, client);
            return Ok();
        }

        //удаления клиента,
        [HttpDelete("{ClientId}")]
        public IActionResult RemoveClient(int clientId)
        {
            Client client = _clientService.GetClient(clientId);

            if (client != null)
            {
                _clientService.RemoveClient(clientId);
                return Ok();
            }
            else
            {
                return NotFound($"Client with ID {clientId} not found");
            }
        }
    }
}
