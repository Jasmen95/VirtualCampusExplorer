using UnityEngine;
using UnityEngine.UI;
using MTAssets.EasyMinimapSystem;

public class MinimapController : MonoBehaviour
{
    public Canvas minimapCanvas;
    public RectTransform minimapRectTransform;
    public Button closeButton;
    private bool isPaused = false; // Track game pause state
     private bool isCanvasActive = false;
    

    private void Start()
    {
        // Disable the minimap canvas initially
        minimapCanvas.gameObject.SetActive(false);

        //Register the button's click event
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    public void OnCloseButtonClick()
    {
        // Hide the minimap canvas when the button is clicked
        hideMinimap();
        
    }

     public void showMinimap()
    {
        if (!isPaused)
        {
            if(!isCanvasActive)
            {
                 minimapCanvas.gameObject.SetActive(true);
                 isCanvasActive = true;
                 if(isCanvasActive){
                    Time.timeScale = 0f; //pause the game
                 }
            }
            isPaused = true;
        }
    }

    public void hideMinimap()
    {
        if (isPaused)
        {
            if(isCanvasActive)
            {
                minimapCanvas.gameObject.SetActive(false);
                 isCanvasActive = false;
                 if(!isCanvasActive){
                    Time.timeScale = 1f; //resume the game
                 }
            }
            isPaused = false;
        }
    }

    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;

            // Convert mouse position to canvas-local coordinates
            Vector2 localPosition = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(minimapRectTransform, mousePosition, null, out localPosition);
            

        // Check for touch input
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            // Convert touch position to canvas-local coordinates
            Vector2 localPosition = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(minimapRectTransform, touchPosition, null, out localPosition);
        }*/
            // Check if the click is on the minimap
            if (minimapRectTransform.rect.Contains(localPosition))
            {
                
                    // Show the minimap canvas when clicked on the minimap
                    minimapCanvas.gameObject.SetActive(true);
                   
                
            }
           
        }

       
        // Check for 'M' key press to toggle the minimapCanvas and pause/resume game
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isPaused)
            {
                showMinimap();
            }
            else
            {
                hideMinimap();
            }
        }
    }
}
