namespace OopWordRPG {
    // INHERITANCE
    public class Slime : Enemy {
        // ENCAPSULATION
        public override string Name => "¥vµÜ©i";
        override public int GenExp { get; } = 10;
        public override int Level { get; protected set; } = 1;

        // POLYMORPHISM
        protected override void Init(int p_level = 1) {
            IsDead = false;
            Level = p_level;
            Hp = 30;
            Mp = 10;
            Atk = 5;
            Def = 5;
            Spd = 2;
        }
    }
}
