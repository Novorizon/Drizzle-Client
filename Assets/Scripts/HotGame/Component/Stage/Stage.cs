using DataBase;
using ECS;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Game
{
    public class Stage : IComponentData
    {
        public int id;//µ±Ç°Stage id
        public string name;
        public StageState state;

    }
}
