using System;
using System.Collections;
using UnityEngine;

namespace OopWordRPG {
    [CreateAssetMenu(fileName = "Player", menuName = "Player")]
    // INHERITANCE
    public class Player : Role {
        public override bool IsPlayer => true;
        private string playerName = "";
        public override string Name => playerName;

        [NonSerialized] private int playerLevel = 1;
        [NonSerialized] private int playerMaxHp = 100;
        [NonSerialized] private int playerMaxMp = 50;
        [NonSerialized] private int playerMaxExp = 50;
        [NonSerialized] private int playerExp   = 0;
        [NonSerialized] private int playerAtk   = 10;
        [NonSerialized] private int playerDef   = 0;
        [NonSerialized] private int playerSpd   = 5;

        // ENCAPSULATION
        public override int Level   { get { return playerLevel; } protected set { playerLevel = value; } }
        public override int MaxHp   { get { return playerMaxHp; } protected set { playerMaxHp = value; } }
        public override int MaxMp   { get { return playerMaxMp; } protected set { playerMaxMp = value; } }
        public virtual  int MaxExp  { get { return playerMaxExp; } protected set { playerMaxExp = value; } }
        public virtual  int Exp     { get { return playerExp; } protected set { playerExp = value; } }
        public override int Atk     { get { return playerAtk; } protected set { playerAtk = value; } }
        public override int Def     { get { return playerDef; } protected set { playerDef = value; } }
        public override int Spd     { get { return playerSpd; } protected set { playerSpd = value; } }

        private float resurrectionTime = 5f;
        private float GetResurrectionTime() => resurrectionTime + (Level / 10) * 5 + (Level % 10) * 0.5f;
        public float ResurrectionRemainingTime { get; private set; }

        public void InitPlayer(string p_name) {
            playerName = p_name;
            Hp = MaxHp;
            Mp = MaxMp;
        }

        public IEnumerator Resurrection(Role p_killBy) {
            ResurrectionRemainingTime = GetResurrectionTime();
            string _resurrectionMessage = $"{Name} 被 {p_killBy.Name} 擊敗，等待 {ResurrectionRemainingTime} 秒復活";
            Debug.Log(_resurrectionMessage);

            while(true) {
                yield return null;
                ResurrectionRemainingTime -= Time.deltaTime;
                if(ResurrectionRemainingTime <= 0) {
                    ResurrectionRemainingTime = 0;
                    break;
                }
            }

            Recover();
            IsDead = false;
            OnRecover?.Invoke();
        }

        public IEnumerator GainExperience(Enemy p_killedEnemy) {
            Exp += p_killedEnemy.GenExp;
            string _gainExpMessage = null;
            if(Exp >= MaxExp) {
                LevelUp();
                _gainExpMessage = $"{Name} 擊敗 {p_killedEnemy.Name} ，獲得 {p_killedEnemy.GenExp} 經驗值，等級提升至 {Level} 等，目前經驗值 {Exp}/{MaxExp}";
                yield return new WaitForSeconds(1f);
            } else {
                _gainExpMessage = $"{Name} 擊敗 {p_killedEnemy.Name} ，獲得 {p_killedEnemy.GenExp} 經驗值，目前經驗值 {Exp}/{MaxExp}";
            }
            Debug.Log($"<color=yellow>{_gainExpMessage}</color>");
            yield return null;
        }

        private void LevelUp() {
            Level++;
            MaxHp += 10;
            MaxMp += 5;
            Exp -= MaxExp;
            MaxExp = (int)(MaxExp * 1.1f);

            Recover();
            OnLevelUp?.Invoke();
        }

        private void Recover() {
            Hp = MaxHp;
            Mp = MaxMp;
        }
    }
}