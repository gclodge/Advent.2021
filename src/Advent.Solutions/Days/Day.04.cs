namespace Advent.Solutions.Days
{
    internal record Position(int X, int Y);

    internal class BingoBoard
    {
        const int BoardSize = 5;

        public int[] Numbers { get; }

        public bool HasWinner { get; private set; } = false;

        public HashSet<int> Marked { get; private set; }
        public Dictionary<int, List<Position>> ValueMap { get; private set; }

        int[] RowCount = new int[BoardSize];
        int[] ColCount = new int[BoardSize];

        public BingoBoard(IEnumerable<string> lines)
        {
            this.Marked = new HashSet<int>();
            this.Numbers = lines.SelectMany(x => x.Split(' '))
                         .Where(x => !string.IsNullOrWhiteSpace(x))
                         .Select(int.Parse)
                         .ToArray();

            this.ValueMap = GetValueMap(Numbers);
        }

        public void Mark(int num)
        {
            if (!ValueMap.ContainsKey(num))
                return;

            Marked.Add(num);
            foreach (var pos in ValueMap[num])
            {
                RowCount[pos.Y]++;
                ColCount[pos.X]++;
            }

            HasWinner = CheckForWinners();
        }

        public int GetScore()
        {
            int sum = Numbers.Where(x => !Marked.Contains(x)).Sum();
            return sum * Marked.Last();
        }

        bool CheckForWinners()
        {
            return (RowCount.Any(x => x == BoardSize) || ColCount.Any(x => x == BoardSize));
        }

        static Dictionary<int, List<Position>> GetValueMap(int[] nums)
        {
            var map  = new Dictionary<int, List<Position>>();
            foreach (int i in Enumerable.Range(0, nums.Length))
            {
                if (!map.ContainsKey(nums[i]))
                {
                    map.Add(nums[i], new List<Position>());
                }
                map[nums[i]].Add(GetPosition(i));
            }
            return map;
        }

        static Position GetPosition(int index)
        {
            return new Position(index % BoardSize, index / BoardSize);
        }
    }

    public class Bingo
    {
        IEnumerable<int> Numbers { get; }
        IEnumerable<BingoBoard> Boards { get; }

        public List<int> Scores { get; private set; }

        public Bingo(IEnumerable<string> lines)
        {
            this.Numbers = lines.First().Split(',').Select(int.Parse).ToList();
            this.Boards = ParseBoards(lines.Skip(2)); //< Skip the leading two lines
            this.Scores = new List<int>();
        }

        public void Play()
        {
            foreach (var num in Numbers)
            {
                foreach (var board in Boards.Where(b => !b.HasWinner))
                {
                    board.Mark(num);

                    if (board.HasWinner)
                    {
                        Scores.Add(board.GetScore());
                    }
                }
            }
        }

        static IEnumerable<BingoBoard> ParseBoards(IEnumerable<string> lines)
        {
            var res = new List<BingoBoard>();
            var curr = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    res.Add(new BingoBoard(curr));
                    curr = new List<string>();
                }
                curr.Add(line);
            }

            if (curr.Count > 0)
            {
                res.Add(new BingoBoard(curr));
            }

            return res;
        }
    }
}
