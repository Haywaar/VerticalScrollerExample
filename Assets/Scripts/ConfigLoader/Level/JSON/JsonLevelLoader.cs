using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Класс для загрузки уровней из JSON-а
/// </summary>
public class JsonLevelLoader : ILevelLoader
{
    private List<Level> _levelData;
    private bool _isLoaded;
    private string _fileName;
    public JsonLevelLoader(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<Level> GetLevels()
    {
        return _levelData;
    }

    public bool IsLoaded()
    {
        return _isLoaded;
    }

    public void Load()
    {
        LoadFile(_fileName);
    }

    private async void LoadFile(string fileName)
    {
        string url = string.Empty;
        url = "file://" + Application.dataPath + "/Resources/RemoteConfigs/" + fileName;
        UnityWebRequest request = UnityWebRequest.Get(url);
        await request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            var text = request.downloadHandler.text;
            _levelData = JsonConvert.DeserializeObject<List<Level>>(text);
            _isLoaded = true;

            var eventBus = ServiceLocator.Current.Get<EventBus>();
            eventBus.Invoke(new DataLoadedSignal(this));
        }
    }
    
    public bool IsLoadingInstant()
    {
        return true;
    }
}