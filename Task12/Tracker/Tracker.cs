using System.Threading.Tasks;

namespace LockingExecerises
{
    public class TrackerEx
    {
        public static TrackerResult Execute(int noToCreate, int noOfClicksForEach)
        {
            Tracker aTrackerInstance = null;
            Parallel.For(0, noToCreate, (indx) =>
            {
                var ourTracker = new Tracker();
                Parallel.For(0, noOfClicksForEach, (clickIndx) =>
                {
                    ourTracker.Click();
                });
                aTrackerInstance = ourTracker;
            });
            return new TrackerResult(aTrackerInstance.CreateCount, aTrackerInstance.TotalClicksAcrossAllTrackers, aTrackerInstance.TotalClicksForThisTracker);
        }
    }

    public class Tracker
    {
        private static int _createCount = 0;
        public static readonly object _createCountLock = new object();

        // Static lock is shared amond all instances of the class
        private static int _totalClicksAcrossAllTrackers = 0;
        public static readonly object _totalClicksAcrossAllTrackersLock = new object();

        // non-static lock is shared amond all instances of the class

        private int _totalClicksForThisTracker = 0;
        private  readonly object _totalClicksForThisTrackerLock = new object();

        public Tracker()
        {
            
            lock (_createCountLock)
            {
                _createCount++;
            }
        }

        public void Click()
        {
            lock (_totalClicksAcrossAllTrackersLock)
            {
                _totalClicksAcrossAllTrackers++;

            }

            lock (_totalClicksForThisTrackerLock)
            {
                _totalClicksForThisTracker++;
            }
        }

        public int CreateCount => _createCount;
        public int TotalClicksAcrossAllTrackers => _totalClicksAcrossAllTrackers;  // Todo
        public int TotalClicksForThisTracker => _totalClicksForThisTracker; // Todo
    }

    public class TrackerResult
    {
        public TrackerResult(int createCount, int totalClicksAcrossAllTrackers, int totalClicksForThisTracker)
        {
            TotalClicksForThisTracker = totalClicksForThisTracker;
            TotalClicksAcrossAllTrackers = totalClicksAcrossAllTrackers;
            CreateCount = createCount;
        }

        public int CreateCount { get; }
        public int TotalClicksAcrossAllTrackers { get; }
        public int TotalClicksForThisTracker { get; }
    }
}