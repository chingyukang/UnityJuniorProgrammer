using System.Collections;
using UnityEngine;

namespace OopWordRPG {
    // INHERITANCE
    public class Player : Role {
        private string playerName = "";
        public override string Name => playerName;

        // ENCAPSULATION
        public override int Level   { get; protected set; } = 1;
        public virtual  int MaxHp   { get; protected set; } = 100;
        public virtual  int MaxMp   { get; protected set; } = 50;
        public virtual  int MaxExp  { get; protected set; } = 50;
        public override int Hp      { get; protected set; } = 100;
        public override int Mp      { get; protected set; } = 50;
        public virtual  int Exp     { get; protected set; } = 0;
        public override int Atk     { get; protected set; } = 10;
        public override int Def     { get; protected set; } = 0;
        public override int Spd     { get; protected set; } = 5;

        private float resurrectionTime = 5f;
        private float GetResurrectionTime() => resurrectionTime + (Level / 10) * 5 + (Level % 10) * 0.5f;
        public float ResurrectionRemainingTime { get; private set; }

        public Player(string p_name) {
            playerName = p_name;
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
        }

        public IEnumerator GainExperience(Enemy p_killedEnemy) {
            Exp += p_killedEnemy.GenExp;
            string _gainExpMessage = null;
            if(Exp >= MaxExp) {
                yield return new WaitForSeconds(1f);
                LevelUp();
                _gainExpMessage = $"{Name} 擊敗 {p_killedEnemy.Name} ，獲得 {p_killedEnemy.GenExp} 經驗值，等級提升至 {Level} 等，目前經驗值 {Exp}/{MaxExp}";
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
        }

        private void Recover() {
            Hp = MaxHp;
            Mp = MaxMp;
        }
    }
}