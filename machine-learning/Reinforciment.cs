using RLMatrix.Toolkit;

namespace Melene.MachineLearning;

[RLMatrixEnvironment]
public partial class Reinforciment
{

    public GPUMeasure LastGpuMeasure = new GPUMeasure("Unknown", 0, 0, 0, 0, 0, 0, 0, 0);
    public GPUMeasure? CurrentGpuMeasure;

    private PossibleActions actionTaken;
    private PossibleActions lastActionTaken;
    private float targetUsage = 80f;

    public float RecentAccuracy => total > 0 ? (float)correct / total * 100 : 0;

    private int correct = 0;

    private int total = 0;

    public Reinforciment()
    {
    }

    [RLMatrixObservation]
    public float[] getCurrentMeasure()
    {
        float gpuLoad = CurrentGpuMeasure?.GpuLoadPercent.GetValueOrDefault() ?? 0f;
        float gpuCoreClock = CurrentGpuMeasure?.GpuCoreClock ?? 0f;
        float gpuCoreTemperature = CurrentGpuMeasure?.GpuCoreTemperature ?? 0f;
        float memoryTemperature = CurrentGpuMeasure?.MemoryTemperature ?? 0f;
        float hotspotTemperature = CurrentGpuMeasure?.TemperatureHotspot ?? 0f;
        float powerDraw = CurrentGpuMeasure?.GpuPowerDrawWatts ?? 0f;

        return [
            gpuLoad,
            gpuCoreClock,
            gpuCoreTemperature,
            memoryTemperature,
            hotspotTemperature,
            powerDraw
        ];
    }

    [RLMatrixActionDiscrete(3)]
    public void pickActions(int action)
    {
        lastActionTaken = actionTaken;
        actionTaken = PossibleActionsExtensions.GetFromNumber(action);
    }

    private PossibleActions whatHappened()
    {
        if (CurrentGpuMeasure?.GpuLoadPercent == null)
        {
            return PossibleActions.KeepClock;
        }

        if (LastGpuMeasure.GpuLoadPercent < CurrentGpuMeasure.GpuLoadPercent)
        {
            return PossibleActions.IncreaseClock;
        }
        if (LastGpuMeasure.GpuLoadPercent > CurrentGpuMeasure.GpuLoadPercent)
        {
            return PossibleActions.LowerClock;
        }
        return PossibleActions.KeepClock;
    }

    private bool actionTakenWasBetterThanTheOsSelected()
    {
        if (CurrentGpuMeasure?.GpuLoadPercent == null)
        {
            return false;
        }
        if (lastActionTaken == PossibleActions.IncreaseClock && CurrentGpuMeasure.GpuLoadPercent > targetUsage)
        {
            return true;
        }
        if (lastActionTaken == PossibleActions.LowerClock && CurrentGpuMeasure.GpuLoadPercent < targetUsage)
        {
            return true;
        }
        return false;
    }

    private bool isActionTakenEqualToOsSelected()
    {
        return lastActionTaken == whatHappened();
    }

    private bool wasGoodAction()
    {
        if (lastActionTaken == PossibleActions.KeepClock && isKeepClockWithinTolerance())
        {
            return true;
        }

        if (gpuLoadPercentIsCloserToTargetThanBefore() && isActionTakenEqualToOsSelected())
        {
            return true;
        }
        if (!gpuLoadPercentIsCloserToTargetThanBefore() && !isActionTakenEqualToOsSelected())
        {
            return actionTakenWasBetterThanTheOsSelected();
        }
        return false;
    }

    private bool isKeepClockWithinTolerance()
    {
        if (CurrentGpuMeasure?.GpuLoadPercent == null)
        {
            return false;
        }

        float currentLoad = CurrentGpuMeasure.GpuLoadPercent.GetValueOrDefault();
        float lastLoad = LastGpuMeasure.GpuLoadPercent.GetValueOrDefault();

        return Math.Abs(currentLoad - lastLoad) <= 2f;
    }

    private bool gpuLoadPercentIsCloserToTargetThanBefore()
    {
        if (CurrentGpuMeasure?.GpuLoadPercent == null)
        {
            return false;
        }

        float currentLoad = CurrentGpuMeasure.GpuLoadPercent.GetValueOrDefault();
        float lastLoad = LastGpuMeasure.GpuLoadPercent.GetValueOrDefault();

        return Math.Abs(currentLoad - targetUsage) < Math.Abs(lastLoad - targetUsage);
    }

    internal void updateMeasure(GpuCollector gpuCollector)
    {
        LastGpuMeasure = CurrentGpuMeasure ?? LastGpuMeasure;
        CurrentGpuMeasure = gpuCollector.GetGpuMeasures().First(g => g.GpuName.Contains("Rx 7600", StringComparison.OrdinalIgnoreCase));
    }

    [RLMatrixDone]
    public bool isDone()
    {
        return false;
    }

    [RLMatrixReset]
    public void reset()
    {
        CurrentGpuMeasure = null;
        LastGpuMeasure = new GPUMeasure("Unknown", 0, 0, 0, 0, 0, 0, 0, 0);
        actionTaken = PossibleActions.KeepClock;
        lastActionTaken = PossibleActions.KeepClock;
    }

    [RLMatrixReward]
    public float getReward()
    {
        total++;
        if (wasGoodAction())
        {
            correct++;
            return 1;
        }
        return -1;
    }
}