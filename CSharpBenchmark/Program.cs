using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ATLProjectLib;
using BenchmarkDotNet.Attributes;
using CppInteropLibrary;

namespace CSharpBenchmark;

public partial class Program
{
    public static void Main()
    {
        // _ = BenchmarkRunner.Run<Program>();
        Console.WriteLine("-----IsWindows10OrGreater:");
        Console.WriteLine();
        Console.WriteLine("COM Interop....................");
        TestMetric(ComInteropTest);

        Console.WriteLine("P/Invoke.......................");
        TestMetric(PInvokeTest);

        Console.WriteLine("C++/CLI Interop................");
        TestMetric(CppInteropTest);
    }

    public static void TestMetric(Action<int, int> action)
    {
        const int calls = 1000000;
        const int executions = 1000000;
        Console.WriteLine("#1:");
        Console.WriteLine("    Calls: " + calls);
        Console.WriteLine("    Executions: " + 1);
        var time = TimeTask(() => action(calls, 1));
        Console.WriteLine("    Result: " + time);

        Console.WriteLine("#2:");
        Console.WriteLine("    Calls: " + 1);
        Console.WriteLine("    Executions: " + executions);
        time = TimeTask(() => action(1, executions));
        Console.WriteLine("    Result: " + time);
        Console.WriteLine();
    }

    public static TimeSpan TimeTask(Action task)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        task();
        stopWatch.Stop();
        return TimeSpan.FromMilliseconds(stopWatch.ElapsedMilliseconds);
    }

    private static readonly ATLSimpleObjectClass _atlClass = new();

    [Benchmark]
    [Arguments(1, 100000)]
    [Arguments(1000, 1)]
    public static void ComInteropTest(int calls, int executions)
    {
        for (var i = 0; i < calls; ++i)
            _atlClass.Test(executions);
    }

    [Benchmark]
    [Arguments(1, 100000)]
    [Arguments(1000, 1)]
    public static void PInvokeTest(int calls, int executions)
    {
        for (var i = 0; i < calls; ++i)
            _ = Test(executions);
    }

    [Benchmark]
    [Arguments(1, 100000)]
    [Arguments(1000, 1)]
    public static void CppInteropTest(int calls, int executions)
    {
        for (var i = 0; i < calls; ++i)
            _ = CppInteropClass.Test(executions);
    }

    [LibraryImport(@"C:\WorkSpace\InteropPerformanceTest\x64\Release\NativeCDll.dll")]
    // [LibraryImport(@"..\..\..\..\x64\Release\NativeCDll.dll")]
    private static partial int Test(int executions);
}
