using UnityEngine;

namespace OopWordRPG {
    // INHERITANCE ABSTRACTION
    public abstract class Enemy : Role {
        // ABSTRACTION
        [SerializeField] private int genExp;
        public int GenExp => genExp;

        public void Initalize() => Init(Level);
        protected virtual void Init(int p_level) {
            IsDead = false;
            Hp = MaxHp;
            Mp = MaxMp;
        }
    }
}