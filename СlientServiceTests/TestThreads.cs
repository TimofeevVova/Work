namespace Services.Tests
{
    internal class TestThreads
    {
        [Test]
        public async Task TestAsyncParallelTasks()
        {
            //1) ограничиваем пул потоков при помощи метода ThreadPool.SetMaxThreads 10-ю потоками;
            ThreadPool.SetMaxThreads(10, 10);

            // сохраняем текущий поток вывода
            var originalConsoleOut = Console.Out;

            // перенаправляем поток вывода в TestContext.Out
            Console.SetOut(TestContext.Out);

            var tasks = new List<Task>();

            //2) автоматизировать запуск 15 параллельных задач в цикле послезапуска каждой задачи вставить в цикл задержку Thread.Sleep(1000);
            for (int i = 0; i < 15; i++)
            {
                // создаем и запускаем асинхронную задачу
                tasks.Add(RunAsyncTask(i));
                // после запуска каждой задачи вставить в цикл задержку Thread.Sleep(1000);,
                await Task.Delay(1000);
            }

            // дожидаемся завершения всех асинхронных задач
            await Task.WhenAll(tasks);

            // возвращаем поток вывода обратно
            Console.SetOut(originalConsoleOut);
        }

        private async Task RunAsyncTask(int taskNumber)
        {
            //3) запуск и завершение задач отмечать выводом в консоль
            //(попутно выводить количество свободных потоков в пуле при помощи метода ThreadPool.GetAvailableThreads);
            Console.WriteLine($"Start Task {taskNumber}");

            int workerThreads, completionPortThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"Доступно рабочих потоков в начале: {workerThreads}");
            Console.WriteLine($"Доступно потоков ввода-вывода: {completionPortThreads}");

            //, втеле каждой задачи вставить задержку Thread.Sleep(20000);
            Thread.Sleep(2000);

            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"Доступно рабочих потоков в конце: {workerThreads}");
            Console.WriteLine($"Доступно потоков ввода-вывода: {completionPortThreads}");

            // Выводим информацию о завершении задачи
            Console.WriteLine($"End Task {taskNumber}");
        }
    }
}
