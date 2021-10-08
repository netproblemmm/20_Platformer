using UnityEngine;

namespace PlatformerMVC
{
    public interface IQuestModel
    {
        bool TryComplete(GameObject Player);
    }
}

