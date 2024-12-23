using System;

namespace OopWordRPG {
    public abstract class Role {
        public bool IsDead { get; protected set; } = false;

        public abstract string Name { get; }
        public virtual int Level    { get; protected set; } = 1;
        public virtual int Hp       { get; protected set; } = 30;
        public virtual int Mp       { get; protected set; } = 10;
        public virtual int Atk      { get; protected set; } = 1;
        public virtual int Def      { get; protected set; } = 1;
        public virtual int Spd      { get; protected set; } = 1;

        public virtual int TryAttack() {
            return Attack();
        }
        protected virtual int Attack() {
            return Atk;
        }
        protected virtual int Skill() {
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
            if (_finalDamage < 0) { _finalDamage = 0; }
            Hp -= _finalDamage;
            if(Hp <= 0) { Hp = 0; IsDead = true; }
            return $"{Name} 受到 {p_from.Name} 的攻擊，造成 {_finalDamage} 點傷害，剩餘 {Hp} 點生命";
        }
    }
}