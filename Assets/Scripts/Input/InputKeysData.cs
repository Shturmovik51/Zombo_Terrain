using UnityEngine;

[CreateAssetMenu(menuName = "Database/InputKeysData", fileName = nameof(InputKeysData))]
public sealed class InputKeysData : ScriptableObject
{
    [SerializeField] private KeyCode _shoot;
    [SerializeField] private KeyCode _run;
    [SerializeField] private KeyCode _jump;
    [SerializeField] private KeyCode _reload;
    [SerializeField] private KeyCode _saveGame;
    [SerializeField] private KeyCode _loadGame;
    [SerializeField] private KeyCode _flashLight;
    [SerializeField] private KeyCode _pause;

    public KeyCode Shoot => _shoot;
    public KeyCode Run => _run;
    public KeyCode Jump => _jump;
    public KeyCode Reload => _reload;
    public KeyCode SaveGame => _saveGame;
    public KeyCode LoadGame => _loadGame;
    public KeyCode FlashLight => _flashLight;
    public KeyCode Pause => _pause;
}  
