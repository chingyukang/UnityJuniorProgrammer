using System;
using UnityEngine;

namespace OopWordRPG {
    // ABSTRACTION
    public abstract class Role : ScriptableObject {
        public bool IsDead { get; protected set; } = false;
        public virtual bool IsPlayer { get; } = false;

        [SerializeField] Sprite roleSprite;
        [SerializeField] private string roleName = "無名";
        [SerializeField] private int level = 1;
        [SerializeField] private int maxHp = 100;
        [SerializeField] private int maxMp = 50;
        [SerializeField] private int atk = 1;
        [SerializeField] private int def = 1;
        [SerializeField] private int spd = 1;

        // ENCAPSULATION
        public virtual Sprite RoleSprite  { get { return roleSprite; } }
        public virtual string Name  { get { return roleName; } }
        public virtual int Level    { get { return level; } protected set { level = value; } }
        public virtual int MaxHp    { get { return maxHp; } protected set { maxHp = value; } }
        public virtual int MaxMp    { get { return maxMp; } protected set { maxMp = value; } }
        public virtual int Hp       { get ; protected set; }
        public virtual int Mp       { get ; protected set; }
        public virtual int Atk      { get { return atk; } protected set { atk = value; } }
        public virtual int Def      { get { return def; } protected set { def = value; } }
        public virtual int Spd      { get { return spd; } protected set { spd = value; } }

        public Action OnAttack;
        public Action OnSkill;
        public Action OnDefense;
        public Action OnTakeDamage;
        public Action OnDead;
        public Action OnLevelUp;
        public Action OnRecover;

        public virtual int TryAttack() {
            return Attack();
        }
        protected virtual int Attack() {
            OnAttack?.Invoke();
            return Atk;
        }
        protected virtual int Skill() {
            OnSkill?.Invoke();
            return (int)(Atk * 2f);
        }

        /// <summary>
        /// 角色被攻擊受到傷害
        /// </summary>
        /// <param name="p_from">攻擊方名稱</param>
        /// <param name="p_damage">攻擊方傷害</param>
        /// <returns>造成傷害的結果訊息</returns>
        public virtual string TakeDamage(Role p_from, int p_damage) {
            int _finalDamage = p_damage - Def;
            Hp -= _finalDamage;

            if(_finalDamage < 0) {
                _finalDamage = 0;
                OnDefense?.Invoke();
            } else {
                OnTakeDamage?.Invoke();
            }

            if(Hp <= 0) { 
                Hp = 0; 
                IsDead = true;
                OnDead?.Invoke();
            }
            return $"{Name} 受到 {p_from.Name} 的攻擊，造成 {_finalDamage} 點傷害，剩餘 {Hp} 點生命";
        }
    }
}