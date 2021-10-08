using System.Collections;
using System.Collections.Generic;
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

    public KeyCode Shoot => _shoot;
    public KeyCode Run => _run;
    public KeyCode Jump => _jump;
    public KeyCode Reload => _reload;
    public KeyCode SaveGame => _saveGame;
    public KeyCode LoadGame => _loadGame;
}  
