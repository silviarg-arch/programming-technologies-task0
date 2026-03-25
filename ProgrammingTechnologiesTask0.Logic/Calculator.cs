using ProgrammingTechnologiesTask0.Data;

namespace ProgrammingTechnologiesTask0.Logic;

public class Calculator
{
    private readonly NumberRepository repository;

    public Calculator()
    {
        repository = new NumberRepository();
    }

    public int AddToBaseNumber(int value)
    {
        return repository.GetBaseNumber() + value;
    }
}