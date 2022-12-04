using System.Collections;
using UnityEngine;
using Zenject;

namespace CMCR
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIManagerConfig _uiManagerConfig;

        private GameBootstrapper _gameBootstrapper;
        private Level _level;
        private UIRoot _uiRoot;
        private AlliesGroup _alliesGroup;
        private Boss _boss;

        private LevelStartUIPanel _levelStartUIPanel;
        private HudUIPanel _hudUIPanel;
        private GameOverUIPanel _gameOverUIPanel;
        private LevelFinishUIPanel _levelFinishUIPanel;

        [Inject]
        private void Construct(GameBootstrapper gameBootstrapper, Level level, UIRoot uiRoot, AlliesGroup alliesGroup, Boss boss)
        {
            _gameBootstrapper = gameBootstrapper;
            _level = level;
            _uiRoot = uiRoot;
            _alliesGroup = alliesGroup;
            _boss = boss;
        }

        private void Awake()
        {
            _levelStartUIPanel = _uiRoot.GetUIPanel<LevelStartUIPanel>();
            _hudUIPanel = _uiRoot.GetUIPanel<HudUIPanel>();
            _gameOverUIPanel = _uiRoot.GetUIPanel<GameOverUIPanel>();
            _levelFinishUIPanel = _uiRoot.GetUIPanel<LevelFinishUIPanel>();
        }

        private void OnEnable()
        {
            _levelStartUIPanel.LevelStartButtonPressed += _level.StartLevel;
            _hudUIPanel.ReloadButtonPressed += _level.ReloadLevel;
            _gameOverUIPanel.PlayAgainButtonPressed += _level.ReloadLevel;
            _levelFinishUIPanel.NextLevelButtonPressed += _level.ReloadLevel;

            _gameBootstrapper.GameStarted += ShowLevelStartUIPanel;
            _alliesGroup.AllAlliesDied += ShowGameOverUIPanel;
            _boss.Died += ShowLevelFinishUIPanel;
        }

        private void OnDisable()
        {
            _levelStartUIPanel.LevelStartButtonPressed -= _level.StartLevel;
            _hudUIPanel.ReloadButtonPressed -= _level.ReloadLevel;
            _gameOverUIPanel.PlayAgainButtonPressed -= _level.ReloadLevel;
            _levelFinishUIPanel.NextLevelButtonPressed -= _level.ReloadLevel;
            
            _gameBootstrapper.GameStarted -= ShowLevelStartUIPanel;
            _alliesGroup.AllAlliesDied -= ShowGameOverUIPanel;
        }
        
        private void ShowLevelStartUIPanel()
        {
            _levelStartUIPanel.Show();
        }

        private void ShowGameOverUIPanel()
        {
            StartCoroutine(ShowGameOverUIPanelRoutine());
        }

        private void ShowLevelFinishUIPanel()
        {
            StartCoroutine(ShowLevelFinishUIPanelRoutine());
        }

        private IEnumerator ShowLevelFinishUIPanelRoutine()
        {
            yield return new WaitForSeconds(_uiManagerConfig.LevelFinishUIPanelDelay);
            _levelFinishUIPanel.Show();
        }

        private IEnumerator ShowGameOverUIPanelRoutine()
        {
            yield return new WaitForSeconds(_uiManagerConfig.GameOverUIPanelDelay);
            _gameOverUIPanel.Show();
        }
    }
}