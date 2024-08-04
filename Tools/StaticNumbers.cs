using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoikz.Tools
{
    /// <summary>
    /// This class is Used for test number,
    /// </summary>
    public class StaticNumbers
    {
        


        /// <summary>
        /// The Important CREDIT Resources
        /// </summary>
        public static int CREDIT { get; set; } = 120;

        /// <summary>
        /// THE_HP
        /// </summary>
        public static int LIVES { get; set; } = 10;


        public static int CURRENT_LEVEL = 3;

        public static int SCORE { get; set; } = 0;

        public static int LEVEL1_WAVE { get; set; } = 30;

        public static int LEVEL2_WAVE { get; set; } = 40;

        public static int LEVEL3_WAVE { get; set; } = 60;

        public static float LEVEL_GAP { get; set; } = 6.0f;

        public static List<LevelConfig> level3config = LoadLevel3Config();

        public static List<LevelVerify> level3_verify = GetLevel3_Verify();

        #region some useful status

        //public static bool IsSellOrUpdate = false;


        #endregion


        #region Weapon Number

        public static int MGUN_DAMAGE { get; set; } = 5;

        public static int MGUN_PRICE { get; set; } =  6;

        public static int MGUN_UPDATE_FEE { get; set; } = 6;

        public static int MGUN_SELL_FEE { get; set; } = 4;


        public static int GLUE_PRICE { get; set; } = 12;

        public static int CARNON_PRICE { get; set; } = 25;

        public static int DGUN_PRICE { get; set; } = 35;

        public static int FIRE_PRICE { get; set; } = 80;

        public static int ELEC_PRICE { get; set; } = 150;

        #endregion

        #region EnemyData

        public static int LOW_ZOIKZ_HP { get; set; } = 40;

        public static float LOW_ZOIKZ_SPEED { get; set; } = 0.0005f;

        public static int LOW_ZOIKZ_CREDIT { get; set; } = 1;

        public static int LOW_ZOIKZ_SCORE { get; set; } = 2;

        public static int FAST_ZOIKZ_HP { get; set; } = 70;


        public static float FAST_ZOIKZ_SPEED { get; set; } = 0.001f;

        public static int FAST_ZOIKZ_CREDIT { get; set; } = 10;

        public static int FAST_ZOIKZ_SCORE { get; set; } = 8;

        public static int SLOW_ZOIKZ_HP { get; set; } = 100;

        public static float SLOW_ZOIKZ_SPEED { get; set; } = 0.0003f;

        public static int SLOW_ZOIKZ_CREDIT { get; set; } = 4;

        public static int SLOW_ZOIKZ_SCORE { get; set; } = 5;

        public static int HIGH_ZOIKZ_HP { get; set; } = 300;

        public static float HIGH_ZOIKZ_SPEED { get; set; } = 0.0003f;

        public static int HIGH_ZOIKZ_CREDIT { get; set; } = 20;

        public static int HIGH_ZOIKZ_SCORE { get; set; } = 15;

        public static float FINAL_ZOIKZ_SPEED { get; set; } = 0.0003f;

        public static int FINAL_ZOIKZ_HP { get; set; } = 1000;

        public static int FINAL_ZOIKZ_CREDIT { get; set; } = 50;

        public static int FINAL_ZOIKZ_SCORE { get; set; } = 45;
        #endregion


        public static List<LevelConfig> LoadLevel3Config()
        {
            FileStream fs = new FileStream("level3config.json", FileMode.Open, FileAccess.Read);

            byte[] buffer = new byte[fs.Length];

            fs.Read(buffer, 0, buffer.Length);

            string JSON = System.Text.Encoding.UTF8.GetString(buffer);

            List<LevelConfig> levelConfigs = JsonConvert.DeserializeObject<List<LevelConfig>>(JSON);

            return levelConfigs.OrderBy(it=>it.LevelIndex).ToList();
        }
    
        public static List<LevelVerify> GetLevel3_Verify()
        {
            List<LevelVerify> list = new List<LevelVerify>();
            for(int i = 0;i < LEVEL3_WAVE; i++){
                LevelConfig item = level3config[i];
                LevelVerify temp = new LevelVerify() { 
                    LevelIndex = i+1, LowZoikzNum = item.LowZoikzNum, SlowZoikzNum = item.SlowZoikzNum, FastZoikzNum = item.FastZoikzNum, HighZoikzNum = item.HighZoikzNum, FinalZoikzNum = item.FinalZoikzNum, TheTotalNum = 0 
                };
                list.Add(temp);
            }

            return list;
        }
    
    }

    public class TempCount
    {
        public float value { get; set; }

        public int Count { get; set; }
    }
}
