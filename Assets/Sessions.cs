﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    [Serializable]
    public class Sessions
    {

        public int 游戏开始手牌数量 = 4;
        public int 发牌数 = 1;
        public int 出牌数量最大值 = 1;
        public int 出牌数量最小值 = 1;        
        public bool 牌堆2是否激活 = false; 
        public bool 混合牌堆1跟2 = false;
        public bool 循环发牌 = false;
    }
    public class CardInfo
    {

        public int pileIndex;
        public string name;
        public int id;//in the pile index
    }

