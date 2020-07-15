using System;
using BenchmarkDotNet.Attributes;
using dotnet_trilha;

public class BenchmarkTupplePatterns {
    DateTime time;

    [Params(637300368000000000,637300476000000000, 637303824000000000, 637303932000000000)]
    public long ticks;

    [Params(1, 2, 3)]
    public int ratio;

    [GlobalSetup]
    public void Setup()
    {
        time = new DateTime(ticks);
    }

    [Benchmark]
    public decimal NestedIfs() => RideApp.CalculateMultiplier_Version_1(time, ratio);

    [Benchmark]
    public decimal ComplicatedIfs() => RideApp.CalculateMultiplier_Version_2(time, ratio);

    [Benchmark]
    public decimal GroupedComplicatedIfs() => RideApp.CalculateMultiplier_Version_3(time, ratio);

    [Benchmark]
    public decimal DirectTuplePattern() => RideApp.CalculateMultiplier_Version_4(time, ratio);

    [Benchmark]
    public decimal OptmizedTuplePattern() => RideApp.CalculateMultiplier_Version_5(time, ratio);

    [Benchmark]
    public decimal SwitchCase() => RideApp.CalculateMultiplier_Version_6(time, ratio);
}

public class BenchmarkTuppleSwap {

    [GlobalSetup]
    public void Setup()
    {
    }

    [Benchmark]
    [Arguments(int.MaxValue, int.MinValue)]
    [Arguments(int.MaxValue, 0)]
    [Arguments(0, int.MinValue)]
    [Arguments(0, 0)]
    [Arguments(654, 456)]
    public void SwitchVariablesBefore(int a, int b) {
        var temp = b;
        b = a;
        a = temp;
    }


    [Benchmark]
    [Arguments(int.MaxValue, int.MinValue)]
    [Arguments(int.MaxValue, 0)]
    [Arguments(0, int.MinValue)]
    [Arguments(0, 0)]
    [Arguments(654, 456)]
    public void SwitchVariablesNow(int a, int b) {
        (a,b) = (b,a);
    }
}