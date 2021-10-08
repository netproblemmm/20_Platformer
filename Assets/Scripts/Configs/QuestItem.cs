using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs / Create QuestItemConfig", fileName = "QuestItemConfig", order = 0)]
public class QuestItem : ScriptableObject
{
    public int QuestId;
    public List<int> questItemIdCollection;
}
