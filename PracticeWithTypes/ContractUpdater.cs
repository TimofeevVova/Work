using Models;

namespace PracticeWithTypes
{
    public class ContractUpdater
    {
        public static void UpdateEmployeeContract(Employee employee)
        {
            // Создаем контракт на основе данных сотрудника.
            string contract = $"44534536";

            // Присваиваем контракт свойству "Contract" сотрудника.
            employee.Contract = contract;
        }

        public static void UpdateCurrency(ref Currency currency, double newExchangeRate)    // нужен ref для изменения реального значения а не копии
        {
            // Меняем значение свойства "ExchangeRate" структуры "Currency" на новое значение.
            currency.ExchangeRate = newExchangeRate;
        }
    }
}