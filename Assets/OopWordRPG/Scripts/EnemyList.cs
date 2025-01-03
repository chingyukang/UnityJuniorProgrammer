using System.Collections.Generic;
using UnityEngine;

namespace OopWordRPG {
    [CreateAssetMenu(menuName = "Enemy/EnemyList")]
    public class EnemyList : ScriptableObject {
        [SerializeField] List<Enemy> enemyList;

        public Enemy GetRandomEnemy() {
            return enemyList[Random.Range(0, enemyList.Count)];
        }
    }
}