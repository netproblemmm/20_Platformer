using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlatformerMVC
{
    public class ResettableQuestStoryController : IQuestStory
{
        private readonly List<IQuest> _questCollection;
        private int _currentIndex;

        public bool IsDone => _questCollection.All(value => value.IsCompleted);

        public ResettableQuestStoryController(List<IQuest> questCollection)
        {
            _questCollection = questCollection;
            ResetAllQuest();
        }

        private void Subscribe()
        {
            foreach (IQuest quest in _questCollection)
            {
                quest.Completed += OnQuestCompleted;
            }
        }

        private void Unsubscribe()
        {
            foreach (IQuest quest in _questCollection)
            {
                quest.Completed -= OnQuestCompleted;
            }
        }

        private void OnQuestCompleted(object sender, IQuest quest)
        {
            int index = _questCollection.IndexOf(quest);
            if (_currentIndex == index)
            {
                _currentIndex++;
                if (IsDone)
                {
                    Debug.Log("FIN!");
                }
            }
            else
            {
                ResetAllQuest();
            }
        }

        private void ResetAllQuest()
        {
            _currentIndex = 0;
            foreach (IQuest quest in _questCollection)
            {
                quest.Reset();
            }
        }

        void Dispose()
        {
            Unsubscribe();
            foreach (var quest in _questCollection)
            {
                quest.Dispose();
            }
        }
    }
}

