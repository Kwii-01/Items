using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using UnityEngine;

namespace SavingSystem {
    [CreateAssetMenu(menuName = "SavingSystems/PlayerPrefs")]
    public class PPSaver : ASaver {
        public override T LoadJson<T>(string key) => this.FromJson<T>(this.LoadString(key));
        public override IEnumerable<T> LoadManyJson<T>(string key) => this.FromJsonArray<T>(this.LoadString(key)).AsEnumerable();

        public override void SaveJson<T>(string key, T data) => this.SaveString(key, this.ToJson(data));
        public override void SaveJson<T>(string key, T[] data) => this.SaveString(key, this.ToJsonArray(data));

        public override bool HasKey(string key) => PlayerPrefs.HasKey(key);

        public override void SaveInt(string key, int data) => PlayerPrefs.SetInt(key, data);
        public override int LoadInt(string key, int defaultValue = 0) => PlayerPrefs.GetInt(key, defaultValue);

        public override void SaveFloat(string key, float data) => PlayerPrefs.SetFloat(key, data);
        public override float LoadFloat(string key, float defaultValue = 0) => PlayerPrefs.GetFloat(key, defaultValue);

        public override void SaveString(string key, string data) => PlayerPrefs.SetString(key, data);
        public override string LoadString(string key, string defaultValue = "") => PlayerPrefs.GetString(key, defaultValue);

        public void DeleteKey(string key) => PlayerPrefs.DeleteKey(key);
        public void DeleteAll() => PlayerPrefs.DeleteAll();
    }
}