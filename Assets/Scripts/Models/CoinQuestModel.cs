using UnityEngine;

namespace PlatformerMVC
{
    public class CoinQuestModel : IQuestModel
    {
        private const string TargetTag = "Player";

        public bool TryComplete(GameObject player)
        {
            return player.CompareTag(TargetTag);
        }
    }
}

