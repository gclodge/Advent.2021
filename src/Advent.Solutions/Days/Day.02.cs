using System.Linq;

namespace Advent.Solutions.Days
{
    public class Submarine
    {
        public int HorizontalPosition { get; set; } = 0;
        public int Depth { get; set; } = 0;
        public int Aim { get; set; } = 0;

        public int Value => Depth * HorizontalPosition;

        public List<string> Commands { get; set; }

        bool UseAim { get; } = false;

        public Submarine(IEnumerable<string> commands, bool useAim = false)
        {
            this.Commands = commands.ToList();
            this.UseAim = useAim;
        }

        public void ChartCourse()
        {
            foreach (var command in this.Commands)
            {
                HandleCommand(command);
            }
        }

        void HandleCommand(string command)
        {
            var cmd = Parse(command);
            switch (cmd.dir)
            {
                case "forward":
                    HorizontalPosition += cmd.mag;
                    if (UseAim) { Depth += (cmd.mag * Aim); }
                    break;
                case "up":
                    if (UseAim) { Aim -= cmd.mag; }
                    else { Depth -= cmd.mag; }
                    break;
                case "down":
                    if (UseAim) { Aim += cmd.mag; }
                    else { Depth += cmd.mag; }
                    break;
                default:
                    throw new NotImplementedException($"Unknown command: {command}");
            }
        }

        static (string dir, int mag) Parse(string command)
        {
            var parts = command.Split(' ');
            return (parts[0].ToLower(), int.Parse(parts[1]));
        }
    }
}
