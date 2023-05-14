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

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))//LMB
            {
                switch (currentState)
                {

                    //zaznaczanie jednostek
                    case Selection.UNITS:
                        {

                        }
                        break;

                    //rekrutowanie jednostek
                    case Selection.BUILDING:
                        {

                        }
                        break;

                    //cokolwiek innego
                    case Selection.NONE:
                        {

                        }
                        break;

                    //nieprzewidziany
                    default:
                        {

                        }
                        break;
                }
            }
        }
       
    }