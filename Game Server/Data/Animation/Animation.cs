using GameServer.Communication;

namespace GameServer.Data {
    public sealed class Animation {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sound { get; set; }
        public int[] Sprite { get; set; }
        public int[] Frames { get; set; }
        public int[] LoopCount { get; set; }
        public int[] LoopTime { get; set; }

        public Animation() {
            Name = string.Empty;
            Sound = "None.";

            Sprite = new int[Configuration.MaxAnimationLayer];
            Frames = new int[Configuration.MaxAnimationLayer];
            LoopCount = new int[Configuration.MaxAnimationLayer];
            LoopTime = new int[Configuration.MaxAnimationLayer];
        }
    }
}