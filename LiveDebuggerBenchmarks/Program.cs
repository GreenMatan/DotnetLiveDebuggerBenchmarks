using BenchmarkDotNet.Running;
using LiveDebuggerPlayground.Benchmarks;
using System.Linq;

namespace LiveDebuggerPlayground
{
    internal partial class Program
    {
        static void Main(string[] args) => BenchmarkRunner.Run<LiveDebuggerBenchmarks>();
        //static void Main(string[] args) => BenchmarkCodeGenerator.Generate(2000);
    }
}
