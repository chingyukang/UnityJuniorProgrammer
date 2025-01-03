using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OopWordRPG {
    public class GameManager : MonoBehaviour {

        [SerializeField] private Player player;
        private Enemy currentEnemy = null;
        [SerializeField] private EnemyList enemyPool;
        [SerializeField] private RoleCardCtrl playerCardCtrl;
        [SerializeField] private RoleCardCtrl enemyCardCtrl;

        private int currentFightRound = 0;

        private WaitForSeconds oneTimesInterval = new WaitForSeconds(1f);
        private WaitForSeconds twoTimesInterval = new WaitForSeconds(0.5f);
        private WaitForSeconds threeTimesInterval = new WaitForSeconds(0.3f);
        private List<WaitForSeconds> fightIntervals = new List<WaitForSeconds>();

        private int fightIntervalLevel = 0;
        private WaitForSeconds fightInterval;

        // Start is called before the first frame update
        void Start() {
            CreatePlayer();
            InitFightIntervals();

            StartCoroutine(FightLoop());
        }

        private void CreatePlayer() {
            player.InitPlayer("頭上兩點光");
            playerCardCtrl.InitCard(player);
            playerCardCtrl.ShowCard();
        }

        private void CreateEnemy() {
            currentEnemy = enemyPool.GetRandomEnemy();
            currentEnemy.Initalize();
            enemyCardCtrl.InitCard(currentEnemy);
            enemyCardCtrl.ShowCard();
            string _createEnemyMessage = $"{player.Name}Lv.{player.Level} 遭遇敵人 {currentEnemy.Name}Lv.{currentEnemy.Level}！";
            Debug.Log($"<color=red>{_createEnemyMessage}</color>");
        }

        private IEnumerator FightLoop() {
            while(true) {
                CreateEnemy();
                yield return fightInterval;
                yield return StartCoroutine(StartFight());
            }
        }

        private IEnumerator StartFight() {
            currentFightRound = 0;
            Role _first, _second;
            if(player.Spd >= currentEnemy.Spd) {
                _first = player;
                _second = currentEnemy;
            } else {
                _first = currentEnemy;
                _second = player;
            }

            string _attackResult = null;
            while(true) {
                currentFightRound++;
                int _damage = _first.TryAttack();
                _attackResult = _second.TakeDamage(_first, _damage);
                Debug.Log(_attackResult);
                if(_second.IsDead) { break; }

                yield return fightInterval;

                _damage = _second.TryAttack();
                _attackResult = _first.TakeDamage(_second, _damage);
                Debug.Log(_attackResult);
                if(_first.IsDead) { break; }

                yield return fightInterval;
            }

            // 戰鬥結束
            yield return fightInterval;
            yield return fightInterval;

            if(player.IsDead) {
                // 玩家死亡等待復活
                yield return StartCoroutine(player.Resurrection(currentEnemy));
            } else {
                // 玩家勝利獲得經驗
                yield return StartCoroutine(player.GainExperience(currentEnemy));
            }
        }

        private void InitFightIntervals() {
            fightIntervals.Clear();
            fightIntervals.Add(oneTimesInterval);
            fightIntervals.Add(twoTimesInterval);
            fightIntervals.Add(threeTimesInterval);

            fightIntervalLevel = 0;
            fightInterval = fightIntervals[fightIntervalLevel];
        }

        private void OnGUI() {
            if(GUI.Button(new Rect(10, 10, 100, 50), $"x{fightIntervalLevel+1}")) {
                fightIntervalLevel++;
                fightIntervalLevel %= fightIntervals.Count;
                fightInterval = fightIntervals[fightIntervalLevel];
            }
        }
    }
}