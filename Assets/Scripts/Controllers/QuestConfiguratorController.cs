using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PlatformerMVC
{
    public class QuestConfiguratorController
    {
        private QuestObjectView _singleQuestView;
        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _questObjectViews;

        private List<IQuestStory> _questStories;
        private QuestController _singleQuest;

        private Dictionary<QuestType, Func<IQuestModel>> _questFactories = new Dictionary<QuestType, Func<IQuestModel>>(1);
        private Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(2);

        public QuestConfiguratorController (QuestView questView)
        {
            _questObjectViews = questView._questObjects;
            _questStoryConfigs = questView._questStoryConfigs;
            _singleQuestView = questView._singleQuest;
        }

        public void Start()
        {
            _singleQuest = new QuestController(_singleQuestView, new CoinQuestModel());
            _singleQuest.Reset();
        }

    }
}
