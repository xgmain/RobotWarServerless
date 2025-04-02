namespace RobotWarServerless.Models
{
    public class RobotInput
    {
        public string ArenaSize { get; set; }
        public List<RobotInstruction> Robots { get; set; } = new List<RobotInstruction>();
    }
}
