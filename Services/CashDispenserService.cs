using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CashDispenserService // ограничение одновременных снятий со счета
    {
        private SemaphoreSlim _semaphore;
        ClientService clientService = new ClientService();

        public CashDispenserService(int maxConcurrentClients)
        {
            _semaphore = new SemaphoreSlim(maxConcurrentClients);
        }

        public async Task<bool> CashWithdrawalAsync(int clientId, double amount)
        {
            try
            {
                await _semaphore.WaitAsync(); // ждем разрешение на вход
                Console.WriteLine($"Клиент {clientId} начал обналичивание {amount}...");

                // метод списания
                clientService.WritingOffMoney(clientId,amount);
                

                Console.WriteLine($"Клиент {clientId} успешно обналичил {amount}.");
                return true;
            }
            finally
            {
                _semaphore.Release(); // Освобождаем разрешение
            }
        }
    }
}
