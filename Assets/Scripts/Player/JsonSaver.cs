using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;

namespace Player
{
    public class JsonSaver : MonoBehaviour
    {
        private string json;
        private int _childCount;

        private void Start()
        {
            if (File.Exists(Application.dataPath + "/GameDetails.json"))
            {
                json = File.ReadAllText(Application.dataPath + "/GameDetails.json");
                JsonUtility.FromJsonOverwrite(Decompress(json), this);
            }
        }

        public void SaveProgress()
        {
            json = JsonUtility.ToJson(this);
            string compress = Compress(json);
            File.WriteAllText(Application.dataPath + "/GameDetails.json", compress);
        }

        private void OnApplicationQuit()
        {
            SaveProgress();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                SaveProgress();
        }

        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }

                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);

                }

                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
    }
}
