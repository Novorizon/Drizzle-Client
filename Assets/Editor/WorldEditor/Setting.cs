using DataBase;
using Mono.Data.Sqlite;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WorldEditor
{

    public partial class WorldEditor
    {

        [Button("Export"), ShowIf("IsSettings"), PropertySpace(SpaceBefore = 10)]
        public void Export()
        {


        }


    }
}