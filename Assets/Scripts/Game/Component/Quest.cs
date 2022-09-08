using DataBase;
using ECS;
using System.Collections.Generic;

namespace Game
{
    public class Quest : IComponentData
    {
        public int id;
        public string name;
        public QuestState state;
        public int stage;

        public List<ItemUnit> items;
    }
}
