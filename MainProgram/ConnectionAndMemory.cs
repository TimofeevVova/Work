using Helpers;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram
{
    public class ConnectionAndMemory
    {
        public static long TotalFreed { get; private set; }
        public static long TotalAllocated { get; private set; }
        //private NpgsqlConnection _connection;
        private IntPtr _chunkHandle; // адрес в неуправляемой памяти
        private int _chunkSize; // число выделенных байтов



        private ApplicationContext _dbContext= new ApplicationContext();

        public ConnectionAndMemory(int chunkSize)
        {
            var _connection = _dbContext.Database.GetDbConnection();
            _connection.Open();
            _chunkSize = chunkSize;
            // Выделяем память из неуправляемой памяти процесса
            _chunkHandle = Marshal.AllocHGlobal(chunkSize);
            TotalAllocated += chunkSize;
        }
        public void DoWork() { } // Фиктивный метод. Подразумевается, что здесь вы работаете с ресурсами.

        public void CreateConnectionsAndMemory(int count)
        {
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                var chunkSize = random.Next(4096);
                var connectionAndMemory = new
                ConnectionAndMemory(chunkSize);
                connectionAndMemory.DoWork();
            }
        }

        


    }
}
