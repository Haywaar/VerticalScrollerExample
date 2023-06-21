using System.Collections.Generic;
using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals;
using UI;
using UI.Dialogs;
using UnityEngine;

/// <summary>
/// В этот класс мы прокинем все лоадеры в игре
/// Сейчас у нас есть загрузчик уровней и кораблей
/// Когда ВСЕ конфиги и ресурсы загружены, данный класс отправит ивент
/// Что игра готова AllDataLoadedSignal
/// </summary>
public class ConfigDataLoader : MonoBehaviour, IService
{
    private List<ILoader> _loaders;
    private EventBus _eventBus;

    private int _loadedSystems = 0;
    public void Init(List<ILoader> loaders)
    {
        _loaders = loaders;
    }

    /// <summary>
    /// Вообще Start нужен для синхронизации
    /// Чтобы загрузчик начал загружать ПОСЛЕ всех подписок
    /// </summary>
    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<DataLoadedSignal>(OnConfigLoaded);
        
        // Если загрузка требует некоторого времени
        // придётся показать экран Please Wait
        if (_loaders.Any(x => !x.IsLoadingInstant()))
        {
            WindowManager.ShowWindow<LoadingDialog>();
        }
        
        LoadAll();
    }

    private void OnConfigLoaded(DataLoadedSignal signal)
    {
        _loadedSystems++;
        
        _eventBus.Invoke(new LoadProgressChangedSignal(((float)_loadedSystems/_loaders.Count)));
        if (_loadedSystems == _loaders.Count)
        {
            _eventBus.Invoke(new AllDataLoadedSignal());
        }
    }

    private void LoadAll()
    {
        foreach (var loader in _loaders)
        {
            loader.Load();
        }
    }
}