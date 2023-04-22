using UnityEngine;

namespace EnemyScripts
{
    [CreateAssetMenu(fileName = "EnemyTemplate", menuName = "ScriptableObjects/Enemy", order = 1)]
    public class ScriptableEnemy : ScriptableObject
    {
        public string objectName;

        public string name;
        public string health;
        public Mesh mesh;
    }
}
