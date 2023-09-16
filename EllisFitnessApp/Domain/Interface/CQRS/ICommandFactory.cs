namespace Domain.Logger.Interface.CQRS;

public interface ICommandFactory<T, K>
{
    Task<ICommand<T, K>> CreateInsertCommand();

    ICommand<T, K> CreateUpdateCommand();

    ICommand<T, K> CreateDeleteCommand();

}