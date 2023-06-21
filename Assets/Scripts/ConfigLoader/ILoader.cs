public interface ILoader
{
    public bool IsLoaded();
    public void Load();
    /// <summary>
    /// Требует ли загрузка времени, или мгновенна
    /// </summary>
    /// <returns></returns>
    public bool IsLoadingInstant();
}