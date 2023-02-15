namespace Test1.Examples
{
    public class SmartHomeController
    {
        public DateTime LastMotionTime { get; private set; }

        public void ActuateLights(bool motionDetected)
        {
            DateTime time = DateTime.Now;

            if (motionDetected)
            {
                LastMotionTime = time;
            }

            string timeOfDay = Examples.GetTimeOfDay(time);
            if (motionDetected && (timeOfDay == "Evening" || timeOfDay == "Night"))
            {
                BackyardLightSwitcher.Instance.TurnOn();
            }

            else if (time.Subtract(LastMotionTime) > TimeSpan.FromMinutes(1) || (timeOfDay == "Morning" || timeOfDay == "Noon"))
            {
                BackyardLightSwitcher.Instance.TurnOff();
            }
        }
    }
}
