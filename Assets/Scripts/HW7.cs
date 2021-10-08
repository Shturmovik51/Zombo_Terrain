using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.Debug;

namespace HomeWork
{
    public class HW7 : MonoBehaviour
    {
        [SerializeField] private string _message;        

        private List<object> _objectList;

        int ID1 = 5;
        int ID2 = 7;
        int ID3 = 5;

        float Speed1 = 56.5f;
        float Speed2 = 6.5f;
        float Speed3 = 56.9f;
        float Speed4 = 6.5f;

        string Name1 = "Boat";
        string Name2 = "Car";
        string Name3 = "Car";

        private void Start()
        {
            Log(_message.HowMuchChars());

            _objectList = new List<object>() { ID1, ID2, ID3, Speed1, Speed2, Speed3, Speed4, Name1, Name2, Name3 };

            Log(_objectList.HowMuchParameters());

            _objectList.LogElementsCount();
        }
    }

}
