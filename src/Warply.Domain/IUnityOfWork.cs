namespace Warply.Domain;

public interface IUnityOfWork
{
    Task Commit();
}