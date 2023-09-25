using UnityEngine.UI;
using UnityEngine;

public class StartPositionButton : MonoBehaviour
{
    public int positionIndex;
    public Canvas canvastoDeactivate;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SetStartPosition);
    }

    private void SetStartPosition()
    {
        //deactivate the canvas when button is clicked
        if(canvastoDeactivate != null)
        {
            canvastoDeactivate.enabled = false;
        }
       
        FindObjectOfType<StartingManager>().SetCharacterPosition(positionIndex);
    }
}
