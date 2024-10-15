using System.Collections.Generic;

namespace TinyOS.Hardware;

/// <summary>
/// Represents a touch screen device that can be calibrated
/// </summary>
public interface ICalibratableTouchscreen : ITouchScreen
{
    /// <summary>
    /// Returns <b>true</b> if the touchscreen has been calibrated, otherwise <b>false</b>
    /// </summary>
    public bool IsCalibrated { get; }

    /// <summary>
    /// Sets the calibration data for the touchscreen
    /// </summary>
    /// <param name="data">The calibration point</param>
    public void SetCalibrationData(IEnumerable<CalibrationPoint> data);
}
