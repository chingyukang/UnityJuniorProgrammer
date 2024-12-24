namespace OopWordRPG {
    // INHERITANCE
    public class BigSlime : Slime {
        // ENCAPSULATION
        public override string Name => "¤j¥vµÜ©i";
        override public int GenExp { get; } = 15;
        public override int Level { get; protected set; } = 2;

        // POLYMORPHISM
        protected override void Init(int p_level = 2) {
            base.Init(p_level);
            Hp += 10;
            Def += 2;
            Spd -= 1;
        }
    }
}