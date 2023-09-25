
using UnityEngine;

public class StartingManager : MonoBehaviour
{
    //array of starting positions
    public Transform[] startPositions;
    public GameObject player;

    private void Start(){
        if(PlayerPrefs.HasKey("SelectedLocationIndex"))
        {
            int selectedLocationIndex = PlayerPrefs.GetInt("SelectedLocationIndex", 0);
            SetCharacterPosition(selectedLocationIndex);
        }
    }    

    public void SetCharacterPosition(int locationIndex)
    {
        //ensure selected position is within range
        if(locationIndex >= 0 && locationIndex < startPositions.Length)
        {
            //set player's position to the selected position
            Transform startPosition = startPositions[locationIndex];
            //set player's position
            player.transform.position = startPosition.position;
           
           
        }
    }
}
