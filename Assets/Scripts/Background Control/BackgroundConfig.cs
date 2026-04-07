using UnityEngine;

namespace BackgroundControl
{
    [CreateAssetMenu(fileName = "Background Config", menuName = "Scriptable Objects/Background Config")]
    public class BackgroundConfig : ScriptableObject
    {
        [SerializeField] private BackgroundInfo[] _infos;

        public BackgroundInfo[] Infos => _infos;

        public BackgroundInfo GetInfo(BackgroundType type)
        {
            foreach (var info in _infos)
            {
                if (info.Type == type)
                    return info;
            }

            Debug.LogWarning($"BackgroundConfig: No info found for type {type}");
            return default;
        }
    }
}