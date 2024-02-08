using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SavingSystem {
    public abstract class ASaver : ScriptableObject {
        //INT
        public abstract void SaveInt(string key, int data);
        public abstract int LoadInt(string key, int defaultValue = 0);

        //FLOAT
        public abstract void SaveFloat(string key, float data);
        public abstract float LoadFloat(string key, float defaultValue = 0);

        //STRING
        public abstract void SaveString(string key, string data);
        public abstract string LoadString(string key, string defaultValue = "");

        public abstract void SaveJson<T>(string key, T data);
        public abstract void SaveManyJson<T>(string key, T[] data);

        public abstract T LoadJson<T>(string key);
        public abstract IEnumerable<T> LoadManyJson<T>(string key);

        public abstract bool HasKey(string key);

        protected T FromJson<T>(string json) => JsonUtility.FromJson<T>(json);
        protected IEnumerable<T> FromJsonArray<T>(string json) => JsonUtility.FromJson<Wrapper<T>>(json)?.Values;

        protected string ToJson<T>(T data) => JsonUtility.ToJson(data);
        protected string ToJsonArray<T>(T[] data) => JsonUtility.ToJson(new Wrapper<T> { Values = data });
    }

    [Serializable]
    public class Wrapper<T> {
        public T[] Values;
    }
}
