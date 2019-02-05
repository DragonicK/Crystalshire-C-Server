namespace GameServer.Data {
    public sealed class EventPage {
        public byte CheckPlayerVariable { get; set; }
        public byte CheckSelfSwitch { get; set; }
        public byte CheckHasItem { get; set; }

        public int PlayerVariableNum { get; set; }
        public int SelfSwitchNum { get; set; }
        public int HasItemNum { get; set; }

        public int PlayerVariable { get; set; }

        public int GraphicX { get; set; }
        public int GraphicY { get; set; }
        public int Graphic { get; set; }
        public byte GraphicType { get; set; }

        public byte MoveType { get; set; }
        public byte MoveSpeed { get; set; }
        public byte MoveFrequency { get; set; }

        public byte WalkAnim { get; set; }
        public byte StepAnim { get; set; }
        public byte DirFix { get; set; }
        public byte WalkThrough { get; set; }

        public byte Priority { get; set; }
        public byte Trigger { get; set; }
        public int CommandCount { get; set; }
        public EventCommand[] Commands { get; set; }
    }
}