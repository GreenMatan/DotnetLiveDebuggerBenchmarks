# .NET Live Debugger Method Probe Benchmarking
These benchmarks compares the two bytecode instrumentation strategies of implementing the Method Probe in .NET as explained in the RFC.  
Each strategy is tested by value and by ref leading to four methods of measurments (OptionA_ByValue, OptionA_ByRef, OptionB_ByValue and OptionB_ByRef).

[.NET Live Debugger Bytecode Instrumentation for Method Probes RFC](https://docs.google.com/document/d/15atktdzQNgOSG81oC4_YJ67QVVzMcAXakBz1G-aXZ4g/edit#heading=h.a2uvr77xbrp)

## Benchmarks Explained
In these benchmarks we are measuring the instrumentations explained in the RFC by pseudo-instrumenting a method that receives 100, 1000 and 2000 `System.Numeric.BigInteger` arguments.

## Results

### Method Probe with 100 Arguments Benchmark
|                                 Method |       Mean |     Error |     StdDev |     Median |   Gen 0 | Code Size |  Gen 1 | Allocated |
|--------------------------------------- |-----------:|----------:|-----------:|-----------:|--------:|----------:|-------:|----------:|
|        OptionA_ByValue_100_Arguments_Benchmark |   6.079 ־¼s | 0.2363 ־¼s |  0.6780 ־¼s |   5.959 ־¼s |  1.2970 |     22 KB | 0.0153 |      8 KB |
|  OptionA_ByRef_100_Arguments_Benchmark |   5.913 ־¼s | 0.2690 ־¼s |  0.7890 ־¼s |   5.759 ־¼s |  1.2970 |     14 KB | 0.0153 |      8 KB |
|        OptionB_ByValue_100_Arguments_Benchmark |   4.560 ־¼s | 0.1349 ־¼s |  0.3915 ־¼s |   4.543 ־¼s |  1.0300 |     22 KB |      - |      6 KB |
|  OptionB_ByRef_100_Arguments_Benchmark |   4.227 ־¼s | 0.1558 ־¼s |  0.4544 ־¼s |   4.168 ־¼s |  1.0300 |     24 KB |      - |      6 KB |

### Method Probe with 1000 Arguments Benchmark
|                                 Method |       Mean |     Error |     StdDev |     Median |   Gen 0 | Code Size |  Gen 1 | Allocated |
|--------------------------------------- |-----------:|----------:|-----------:|-----------:|--------:|----------:|-------:|----------:|
|       OptionA_ByValue_1000_Arguments_Benchmark |  89.186 ־¼s | 2.6012 ־¼s |  7.6697 ־¼s |  88.486 ־¼s | 12.6953 |    147 KB | 1.5869 |     78 KB |
| OptionA_ByRef_1000_Arguments_Benchmark |  91.354 ־¼s | 3.2139 ־¼s |  9.3241 ־¼s |  91.192 ־¼s | 12.6953 |    137 KB | 1.5869 |     78 KB |
|       OptionB_ByValue_1000_Arguments_Benchmark |  58.321 ־¼s | 1.7210 ־¼s |  5.0474 ־¼s |  57.864 ־¼s | 10.1318 |    153 KB |      - |     63 KB |
| OptionB_ByRef_1000_Arguments_Benchmark |  54.316 ־¼s | 1.3808 ־¼s |  4.0279 ־¼s |  54.404 ־¼s | 10.1318 |    122 KB |      - |     63 KB |

### Method Probe with 2000 Arguments Benchmark

|                                 Method |       Mean |     Error |     StdDev |     Median |   Gen 0 | Code Size |  Gen 1 | Allocated |
|--------------------------------------- |-----------:|----------:|-----------:|-----------:|--------:|----------:|-------:|----------:|
|       OptionA_ByValue_2000_Arguments_Benchmark | 209.441 ־¼s | 5.4804 ־¼s | 15.9867 ־¼s | 208.294 ־¼s | 25.3906 |    295 KB | 4.8828 |    156 KB |
| OptionA_ByRef_2000_Arguments_Benchmark | 207.990 ־¼s | 4.2951 ־¼s | 12.5968 ־¼s | 207.691 ־¼s | 25.3906 |    275 KB | 4.8828 |    156 KB |
|       OptionB_ByValue_2000_Arguments_Benchmark | 188.412 ־¼s | 3.6874 ־¼s |  9.1143 ־¼s | 186.000 ־¼s |       - |    319 KB |      - |    125 KB |
| OptionB_ByRef_2000_Arguments_Benchmark | 114.040 ־¼s | 2.4639 ־¼s |  7.2648 ־¼s | 112.066 ־¼s | 20.3857 |    244 KB |      - |    125 KB |
