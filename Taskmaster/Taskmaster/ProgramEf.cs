using System;

#if NET9_0 && !ANDROID && !IOS && !MACCATALYST && !WINDOWS
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("EF Core entry point");
    }
}

#endif