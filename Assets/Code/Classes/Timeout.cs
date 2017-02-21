using System;
using System.Diagnostics;

// created by JBT
/// <summary>
/// A timer class. When created, will count down from the provided time value to zero, which can then be checked in code.
/// </summary>
public class Timeout
{
    private int timeAllowed;
    private Stopwatch stopwatch;

    /// <summary>
    /// Creates a timout object. use to keep track of how long phases are going
    /// </summary>
    /// <param name="time">time in seconds of the timeout</param>
    public Timeout(int time)
    {
        if (time < 1)
        {
            throw new ArgumentOutOfRangeException("Must be at least one second");
        }

        timeAllowed = time;

        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    /// <summary>
    /// Get the seconds remaining before a timeout
    /// </summary>
    public int SecondsRemaining
    {
        get
        {
            return Math.Max(timeAllowed - stopwatch.Elapsed.Seconds, 0);
        }
    }

    /// <summary>
    /// check if the timeout has reached its time allowed
    /// </summary>
    public bool Finished
    {
        get
        {
            return timeAllowed - stopwatch.Elapsed.Seconds < 0;
        }
    }
}