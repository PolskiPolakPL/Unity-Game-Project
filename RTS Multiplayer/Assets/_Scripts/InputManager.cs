using UnityEngine;

    public class InputManager : MonoBehaviour
    {
        private static InputManager instance;
        public static InputManager Instance { get { return instance; } }

        public Selection currentState;

        private void Awake()
        {
            //je¿eli istnieje instancja tej klasy, zniszcz tê instancjê
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }
    }