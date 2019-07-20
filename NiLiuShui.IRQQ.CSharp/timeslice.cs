using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NiLiuShui.IRQQ.CSharp
{
    /// <summary>
    /// 时间片控制
    /// </summary>
    static class Timeslice
    {
        private static Timer secTimer, updateTimer;
        public static void Open()
        {
            secTimer = new Timer(1000);
            secTimer.AutoReset = true;
            secTimer.Enabled = true;
            updateTimer = new Timer(50);
            updateTimer.AutoReset = true;
            updateTimer.Enabled = true;
        }

        public static void AddSecondEvent(ElapsedEventHandler handler)
        {
            secTimer.Elapsed -= handler;
            secTimer.Elapsed += handler;
        }

        public static void AddUpdateEvent(ElapsedEventHandler handler)
        {
            updateTimer.Elapsed -= handler;
            updateTimer.Elapsed += handler;
        }

        public static void Close()
        {
            secTimer = null;
            updateTimer = null;
        }
    }
}
