using Helpers;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Runtime.InteropServices;

namespace MainProgram
{
    public class ConnectionAndMemory : IDisposable
    {
        public static long TotalFreed { get; private set; }
        public static long TotalAllocated { get; private set; }

        private NpgsqlConnection _connection = new NpgsqlConnection(_dbContext.Database.GetDbConnection().ConnectionString);

        private IntPtr _chunkHandle; // адрес в неуправляемой памяти
        private int _chunkSize; // число выделенных байтов
        private bool _isFreed;

        private static ApplicationContext _dbContext= new ApplicationContext();

        public ConnectionAndMemory(int chunkSize)
        {
            var _connection = _dbContext.Database.GetDbConnection();
            //_connection.Open();
            _chunkSize = chunkSize;
            // Выделяем память из неуправляемой памяти процесса
            _chunkHandle = Marshal.AllocHGlobal(chunkSize);
            TotalAllocated += chunkSize;
        }

        private void ReleaseUnmanagedResources()
        {
            if (_isFreed) return;
            Marshal.FreeHGlobal(_chunkHandle);
            TotalFreed += _chunkSize;
            _isFreed = true;
        }


        public void DoWork() { } // Фиктивный метод. Подразумевается, что здесь вы работаете с ресурсами.
        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                _connection?.Dispose();
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }

        public void CreateConnectionsAndMemory(int count)
        {
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                var chunkSize = random.Next(4096);
                using (var connectionAndMemory = new
                ConnectionAndMemory(chunkSize))
                {
                    connectionAndMemory.DoWork();
                }
            }
        } 
    }
}
