namespace OopWordRPG {
    public abstract class Enemy : Role {
        public abstract int GenExp { get; }

        public void Initalize() => Init(Level);
        protected abstract void Init(int p_level);
    }
}