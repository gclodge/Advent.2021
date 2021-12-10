using System.Text;

namespace Advent.Solutions.Days
{
    record CorruptedSyntax(string Source, char FirstBad, int Score);
    record CompletedSyntax(string Source, string Addition, long Score);

    public class SyntaxSolver
    {
        static readonly Dictionary<char, int> CorruptionScore = new()
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 },
        };

        static readonly Dictionary<char, int> CompletionScore = new()
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 },
        };

        static readonly Dictionary<char, char> OpenToClose = new()
        {
            { '{', '}' },
            { '[', ']' },
            { '(', ')' },
            { '<', '>' },
        };

        List<string> Inputs;

        List<CorruptedSyntax> Corrupted;
        List<CompletedSyntax> Completed;

        public int Corruption => Corrupted.Sum(x => x.Score);
        public long Completion => Completed.OrderBy(x => x.Score).ElementAt(Completed.Count() / 2).Score;

        public SyntaxSolver(IEnumerable<string> inputs)
        {
            Inputs = inputs.ToList();
            Corrupted = new();
            Completed = new();

            foreach (var input in Inputs)
            {
                CheckInput(input);
            }
        }

        void CheckInput(string input)
        {
            var stack = new Stack<char>();
            foreach (int i in Enumerable.Range(0, input.Length))
            {
                char c = input[i];
                if (OpenToClose.ContainsKey(c))
                {
                    stack.Push(c);
                }
                else //< Have a closing brace
                {
                    var lastOpen = stack.Pop();
                    if (c != OpenToClose[lastOpen])
                    {
                        //< Corrrrruption
                        Corrupted.Add(new CorruptedSyntax(input, c, CorruptionScore[c]));
                        return;
                    }
                }
            }

            //< Already have what's left
            Completed.Add(CompleteInput(input, stack));
        }

        static CompletedSyntax CompleteInput(string input, Stack<char> stack)
        {
            var sb = new StringBuilder();
            long score = 0;
            while (stack.Count > 0)
            {
                char curr = stack.Pop();
                char closer = OpenToClose[curr];

                sb.Append(closer);

                score *= 5;
                score += CompletionScore[closer];
            }

            return new CompletedSyntax(input, sb.ToString(), score);
        }
    }
}
