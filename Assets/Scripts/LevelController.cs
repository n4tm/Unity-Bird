using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    private float _auxTime;
    private static int _nextLevelIndex = 1;
    private Enemy[] _enemies;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    private void Update()
    {
        foreach (Enemy enemy in _enemies) if (enemy != null) return;
        
        if (_auxTime > 1.5)
        {
            if (_nextLevelIndex > 2) _nextLevelIndex = 0;
            _nextLevelIndex++;
            string nextLevelName = "Level_" + _nextLevelIndex;
            SceneManager.LoadScene(nextLevelName);
        }
        _auxTime+=Time.deltaTime;

    }
}
