using _Project.Scripts.Tools;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class AboutUsUI : MonoBehaviour
    {
        [SerializeField] private Transform tabs;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button backButton;
        [SerializeField] private SceneReference mainMenuScene;

        private Transform[] _allTabs;
        private int _currActiveIndex;
        private int _tabsCount;
        private void Awake()
        {
            GrabAllTabs();
            nextButton.onClick.AddListener(NextTab);
            previousButton.onClick.AddListener(PreviousTab);
            backButton.onClick.AddListener(()=>Loader.Load(mainMenuScene));
        }

        private void Start()
        {
            ChangeActiveTab(0);
        }

        private void ChangeActiveTab(int index)
        {
            _currActiveIndex = index;
            for (var i = 0; i < _allTabs.Length; i++)
            {
                _allTabs[i].gameObject.SetActive(i == _currActiveIndex);
            }
        }

        private void PreviousTab()
        {
            int newIndex = _currActiveIndex - 1;
            if (newIndex<0)
            {
                newIndex += _tabsCount;
            }
            ChangeActiveTab(newIndex);
        }

        private void NextTab()
        {
            int newIndex = (_currActiveIndex + 1) % _tabsCount;
            ChangeActiveTab(newIndex);
        }

        private void GrabAllTabs()
        {
            var childCount = tabs.childCount;
            _allTabs = new Transform[childCount];
            _tabsCount = childCount;
            int i = 0;
            foreach (Transform tab in tabs)
            {
                _allTabs[i++] = tab;
            }
        }
    }
}