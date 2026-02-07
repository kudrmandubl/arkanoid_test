using System;
using System.IO;
using System.Linq;
using DataStorage.Data;
using DataStorage.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DataStorage.Implementations.Systems
{
    ///  <inheritdoc />
    public class DataStorageSystem : IDataStorageSystem
    {
        private BaseStorageData[] _storagesData;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DataStorageSystem()
        {
        }

        ///  <inheritdoc />
        public void Save<T>() where T : BaseStorageData
        {
            try
            {
                var storageData = GetStorageData<T>();
                string json = JsonUtility.ToJson(storageData, true);
                var path = GetSaveFilePath<T>();
                if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    File.WriteAllText(path, json);
                }
                else if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    PlayerPrefs.SetString(path, json);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"При сохранении {typeof(T)}, получена ошибка {e.Message}");
            }
        }

        ///  <inheritdoc />
        public async UniTask LoadAsync()
        {
            _storagesData = new BaseStorageData[]
            {
                await LoadAsync<PlayerStorageData>(),
            }; 
        }

        ///  <inheritdoc />
        private async UniTask<T> LoadAsync<T>() where T : BaseStorageData, new()
        {
            try
            {
                var path = GetSaveFilePath<T>();
                var json = string.Empty;

                if ((Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer) &&  File.Exists(path))
                {
                    json = await File.ReadAllTextAsync(path);
                }
                else if (Application.platform == RuntimePlatform.WebGLPlayer && PlayerPrefs.HasKey(path))
                {
                    json = PlayerPrefs.GetString(path);
                }
                else
                {
                    Debug.LogWarning($"Загружаемый файл не найден {typeof(T)}");
                    return new T();
                }

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogWarning($"Загружаемый файл найден, но пуст {typeof(T)}");
                    return new T();
                }

                T data = JsonUtility.FromJson<T>(json);
                //Debug.LogWarning($"Загружены данные с типом {typeof(T)} path = {path} json = {json}");
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"При загрузке {typeof(T)}, получена ошибка {e.Message}");
            }
            return new T();
        }

        ///  <inheritdoc />
        public T GetStorageData<T>() where T : BaseStorageData
        {
            var data = _storagesData.FirstOrDefault(x => x is T);
            if(data == null)
            {
                Debug.LogError($"Не удалось получить хранимые данные с типом {typeof(T)}");
            }
            
            return (T) data;
        }

        ///  <inheritdoc />
        public void ClearAll()
        {
            _storagesData = new BaseStorageData[]
            {
                new PlayerStorageData(),
            };

            Save<PlayerStorageData>();
        }

        /// <summary>
        /// Получить путь файла сохранения
        /// </summary>
        /// <typeparam name="T">Тип хранимых данных</typeparam>
        /// <returns>Путь файла сохранения</returns>
        private string GetSaveFilePath<T>() where T : BaseStorageData
        {
            return Path.Combine(Application.persistentDataPath, $"{typeof(T)}.json");
        }
    }
}
