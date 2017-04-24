using UnityEngine;

namespace dZineCore.Utility.SceneHelpers
{
    /// <summary>
    /// Closes the application when the escape button is pressed.
    /// </summary>
    public class EscapeQuitter : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Quit();
        }
    }
}
