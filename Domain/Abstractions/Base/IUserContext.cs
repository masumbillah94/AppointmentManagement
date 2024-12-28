namespace Domain.Abstractions.Base
{
    public interface IUserContext
    {
        long UserId { get; }
        string UserName { get; }
    }
}
