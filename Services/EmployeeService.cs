using Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService
    {
        ApplicationContext _dbContext;
        public EmployeeService()
        {
            _dbContext = new ApplicationContext();
        }


        // показать данные сотрудника
        public void ShowEmployeeData(Employee employee)
        {
            if (employee != null)
            {
                Console.WriteLine($"ID: {employee.EmployeeId} Имя: {employee.FirstName}, Фамилия: {employee.LastName} Адрес: {employee.Address}, ДатаРожд: {employee.DateOfBirth.ToString("yyyy-MM-dd")}, Отдел: {employee.Department}, ЗП: {employee.Salary}, Контракт: {employee.Contract}");
            }
            else { Console.WriteLine("Такого Id нет в базе данных"); }
        }

        // показать данные списка сотрудников
        public void ShowSomeEmployeesData(List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                ShowEmployeeData(employee);
            }
        }

        // получить сотрудника по идентификатору;
        public Employee GetEmployee(int employeeId)
        {
            return _dbContext.employeeData.FirstOrDefault(c => c.EmployeeId == employeeId);
        }

        // добавить нового сотрудника
        public void AddEmployee(Employee employee)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    employee.EmployeeId = 0;
                    _dbContext.employeeData.Add(employee);
                    _dbContext.SaveChanges();                   

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        
        // изменить сотрудника по идентификатору;
        public void UpdateEmployee(int employeeId, Employee employee)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Employee existingEmployee = _dbContext.employeeData.Find(employeeId);

                    if (existingEmployee != null)
                    {
                        //existingEmployee.EmployeeId = employee.EmployeeId != 0 ? employee.EmployeeId : existingEmployee.EmployeeId;
                        existingEmployee.FirstName = employee.FirstName != null ? employee.FirstName : existingEmployee.FirstName;
                        existingEmployee.LastName = employee.LastName != null ? employee.LastName : existingEmployee.LastName;
                        existingEmployee.DateOfBirth = employee.DateOfBirth != default(DateTime) ? employee.DateOfBirth : existingEmployee.DateOfBirth;
                        existingEmployee.Address = employee.Address != null ? employee.Address : existingEmployee.Address;
                        existingEmployee.PassportData = employee.PassportData != null ? employee.PassportData : existingEmployee.PassportData;
                        existingEmployee.Department = employee.Department != null ? employee.Department : existingEmployee.Department;
                        existingEmployee.Salary = employee.Salary;
                        existingEmployee.Contract = employee.Contract != null ? employee.Contract : existingEmployee.Contract;

                        _dbContext.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // удалить сотрудника по идентификатору.
        public void RemoveEmployee(int employeeId)
        {
            Employee employee = _dbContext.employeeData.FirstOrDefault(c => c.EmployeeId == employeeId);

            if (employee != null)
            {
                _dbContext.employeeData.Remove(employee);
                _dbContext.SaveChanges();
            }
            else { Console.WriteLine("Такого сотрудника нет в базе данных"); }
        }

        // фильтр
        public List<Employee> GetFilteredEmployees(Func<Employee, bool> filter, Func<Employee, object> orderBy, int pageNumber, int pageSize)
        {
            var query = _dbContext.employeeData.Where(filter);

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }
            // Применение пагинации
            var paginatedResult = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            // вывод данных в консоль
            ShowSomeEmployeesData(paginatedResult);

            return paginatedResult;
        }
    }
}
