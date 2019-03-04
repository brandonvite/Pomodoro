using System;
namespace Pomodoro
{
    public class RootPageMenuItem
    {
        public RootPageMenuItem()
        {
            TargetType = typeof(RootPageDetail);
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }
    }
}
