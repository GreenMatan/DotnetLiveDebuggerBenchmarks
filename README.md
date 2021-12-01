# .NET Live Debugger Method Probe Benchmarking
These benchmarks compare the two bytecode instrumentation strategies for implementing the Method Probe in .NET as explained in the [RFC](https://docs.google.com/document/d/15atktdzQNgOSG81oC4_YJ67QVVzMcAXakBz1G-aXZ4g/edit#heading=h.a2uvr77xbrp).  
Each instrumentation strategy is tested in two variations: passing the arguments by value and by ref (the latter approach is similar to the one used [here](https://github.com/DataDog/dd-trace-dotnet/pull/2092)).

[.NET Live Debugger Bytecode Instrumentation for Method Probes RFC](https://docs.google.com/document/d/15atktdzQNgOSG81oC4_YJ67QVVzMcAXakBz1G-aXZ4g/edit#heading=h.a2uvr77xbrp)

## Benchmarks Explained
In these benchmarks we are measuring the instrumentation techniques as explained in the RFC by pseudo-instrumenting a method that receives 100, 1000 or 2000 `System.Numeric.BigInteger` arguments.

## Results

### Method Probe with 100 Arguments Benchmark
|                                 Method |       Mean |     Error |     StdDev |     Median |   Gen 0 | Code Size |  Gen 1 | Allocated |
|--------------------------------------- |-----------:|----------:|-----------:|-----------:|--------:|----------:|-------:|----------:|
|  OptionA_ByValue_100_Arguments_Benchmark |   5.062 us | 0.1722 us | 0.4942 us |   5.059 us |  1.0300 |     22 KB |      - |      6 KB |
|    OptionA_ByRef_100_Arguments_Benchmark |   4.560 us | 0.1387 us | 0.3957 us |   4.550 us |  1.0300 |     14 KB |      - |      6 KB |
|  OptionB_ByValue_100_Arguments_Benchmark |   4.427 us | 0.1405 us | 0.4097 us |   4.401 us |  1.0338 |     22 KB |      - |      6 KB |
|    OptionB_ByRef_100_Arguments_Benchmark |   3.964 us | 0.1152 us | 0.3324 us |   3.923 us |  1.0338 |     24 KB |      - |      6 KB |

### Method Probe with 1000 Arguments Benchmark
|                                 Method |       Mean |     Error |     StdDev |     Median |   Gen 0 | Code Size |  Gen 1 | Allocated |
|--------------------------------------- |-----------:|----------:|-----------:|-----------:|--------:|----------:|-------:|----------:|
| OptionA_ByValue_1000_Arguments_Benchmark |  71.851 us | 1.4279 us | 3.6602 us |  70.708 us | 10.1318 |    147 KB | 0.1221 |     63 KB |
|   OptionA_ByRef_1000_Arguments_Benchmark |  72.171 us | 1.4084 us | 3.6104 us |  71.925 us | 10.1318 |    137 KB | 0.1221 |     63 KB |
| OptionB_ByValue_1000_Arguments_Benchmark |  57.183 us | 1.2964 us | 3.7816 us |  56.035 us | 10.1318 |    153 KB |      - |     63 KB |
|   OptionB_ByRef_1000_Arguments_Benchmark |  53.013 us | 1.0360 us | 1.4181 us |  52.764 us | 10.1929 |    122 KB |      - |     63 KB |

### Method Probe with 2000 Arguments Benchmark

|                                 Method |       Mean |     Error |     StdDev |     Median |   Gen 0 | Code Size |  Gen 1 | Allocated |
|--------------------------------------- |-----------:|----------:|-----------:|-----------:|--------:|----------:|-------:|----------:|
| OptionA_ByValue_2000_Arguments_Benchmark | 180.951 us | 3.4748 us | 6.7773 us | 179.610 us | 20.2637 |    295 KB |      - |    125 KB |
|   OptionA_ByRef_2000_Arguments_Benchmark | 180.393 us | 3.5930 us | 7.0922 us | 179.638 us | 20.2637 |    275 KB |      - |    125 KB |
| OptionB_ByValue_2000_Arguments_Benchmark | 125.898 us | 2.4994 us | 4.8748 us | 124.146 us | 20.3857 |    319 KB |      - |    125 KB |
|   OptionB_ByRef_2000_Arguments_Benchmark | 113.620 us | 2.2569 us | 3.5797 us | 113.232 us | 20.3857 |    244 KB |      - |    125 KB |
