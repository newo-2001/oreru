namespace Oreru.Domain;
public interface ISerializer<T>
{
    public Task Serialize(T obj, StreamWriter destination);
}
