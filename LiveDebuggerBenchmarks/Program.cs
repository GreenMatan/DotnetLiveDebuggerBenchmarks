using BenchmarkDotNet.Running;
using LiveDebugger.Benchmarks;
using System.Linq;

namespace LiveDebugger
{
    internal partial class Program
    {
        static void Main(string[] args) => BenchmarkRunner.Run<LiveDebuggerBenchmarks>();
        //static void Main(string[] args) => BenchmarkCodeGenerator.Generate(2000);
    }
}
