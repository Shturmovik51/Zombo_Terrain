using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ZomboTerrain
{
    [CustomEditor(typeof(CreateBuff))]
    public class CreateBuffEditor : Editor
    {
        private CreateBuff _createBuff;
        private void Awake()
        {
            _createBuff = (CreateBuff)target;
        }

        private void OnSceneGUI()
        {
            if (!_createBuff.IsAcrive)
                return;

            if(Event.current.button == 0 && Event.current.type == EventType.MouseDown)
            {               
                var mousePos = Event.current.mousePosition;                
                Ray ray = Camera.current.ScreenPointToRay(new Vector3(mousePos.x, Camera.current.pixelHeight - mousePos.y));

                if(Physics.Raycast(ray, out var hit))
                {
                    _createBuff.InstantiateBuff(hit.point);
                    SetObjectDirty(_createBuff.gameObject);
                }
            }

            Selection.activeObject = _createBuff.gameObject;
        }

        public void SetObjectDirty(GameObject obj)
        {
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(obj);
                EditorSceneManager.MarkSceneDirty(obj.scene);
            }
        }
    }
}