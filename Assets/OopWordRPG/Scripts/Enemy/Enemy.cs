namespace OopWordRPG {
    // INHERITANCE ABSTRACTION
    public abstract class Enemy : Role {
        // ABSTRACTION
        public abstract int GenExp { get; }

        public void Initalize() => Init(Level);
        protected abstract void Init(int p_level);
    }
}