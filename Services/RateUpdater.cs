using Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class RateUpdater : IHostedService, IDisposable
    {
        ApplicationContext _dbContext;
        public RateUpdater()
        {
            _dbContext = new ApplicationContext();
        }
        double persent = 25;

        private Timer _timer; // Таймер для периодического выполнения задачи

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DoWork(null); // Вызываем DoWork сразу при старте, если сегодня первый день месяца

            var now = DateTime.Now;
            var nextRun = new DateTime(now.Year, now.Month, 1).AddMonths(1); // Вычисляем следующий запуск в начале следующего месяца
            var timeUntilNextRun = nextRun - now; // Вычисляем время до следующего запуска

            _timer = new Timer(DoWork, null, timeUntilNextRun, TimeSpan.FromDays(30)); // Запускаем таймер

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            ClientService clientService = new ClientService();
            AddBalansePercent(persent);
            
            Console.WriteLine("Процентная ставка начислена!");
            _timer.Change(TimeSpan.FromDays(30), Timeout.InfiniteTimeSpan); // Устанавливаем время до следующего запуска
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0); // Останавливаем таймер
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose(); // Освобождаем ресурсы таймера
        }

        public void AddBalansePercent(double percent)
        {
            List<Account> accounts = _dbContext.accountData.ToList();

            double coefficient = 1 + (percent / 100.0);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (Account account in accounts)
                    {
                        if (account != null)
                        {
                            account.Amount += account.Amount * coefficient;
                        }
                        else
                        {
                            Console.WriteLine("Аккаунт не найден");
                        }
                    }
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Произошла ошибка при обновлении баланса: {ex.Message}");
                }
            }
        }

    }
}
