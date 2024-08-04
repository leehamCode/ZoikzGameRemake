using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoikz.Tools
{
    public class LevelConfig
    {
        public int LevelIndex { get; set; }

        public int LowZoikzNum { get; set; } = 0;

        public int SlowZoikzNum { get; set; } = 0;

        public int HighZoikzNum { get; set; } = 0;

        public int FastZoikzNum { get; set; } = 0;

        public int FinalZoikzNum { get; set; } = 0;

        //SpawnGapTime
        public float WaitTime { get; set; } = 3;

        //Must <=10;
        public int OneTimeSpanCount { get; set; } = 1;
    }

    public class LevelVerify
    {
        public int LevelIndex { get; set; }

        public int LowZoikzNum { get; set; } = 0;

        public int SlowZoikzNum { get; set; } = 0;

        public int HighZoikzNum { get; set; } = 0;

        public int FastZoikzNum { get; set; } = 0;

        public int FinalZoikzNum { get; set; } = 0;

        public int TheTotalNum { get; set; } = 0;

        //SpawnGapTime
        //public float WaitTime { get; set; } = 3;

        //Must <=10;
        //public int OneTimeSpanCount { get; set; } = 1;
    }
}
