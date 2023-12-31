﻿using Models;

namespace Services
{
    public class BankService
    {
        // Расчет зарплаты по доходам\расходам
        public static int SalaryCalculation(int profit, int expenses, List<Employee> employees)
        {
            int countDirectors = 0;
            foreach (Employee employee in employees)
            {
                if (employee.Department == "Директор")
                {
                    countDirectors++;
                }
            }
            int result = (profit - expenses) / countDirectors;
            return result;
        }

        // Конвертирование клиента в сотрудника
        public static Employee ConvertClientToEmployee(Client client)
        {
            Employee employee = new Employee()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                DateOfBirth = client.DateOfBirth,
                Address = client.Address,
                EmployeeId = client.ClientId,
                Department = "Отдел обучения",
                Salary = 500,
                Contract = "200"
            };
            return employee;
        }
    }
}