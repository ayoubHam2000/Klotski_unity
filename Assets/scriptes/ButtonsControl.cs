using UnityEngine;
using UnityEngine.UI;

public class ButtonsControl : MonoBehaviour
{
    private Vector3[] startPositions = null;
    private Vector3[] oldPositions = null;
    private int oldScore = 0;
    private bool isSaved = false;

    [SerializeField] Text saveBtn = null;
    private Mouves movesText = null;


    private void Start()
    {
        movesText = FindObjectOfType<Mouves>();
        startPositions = getPositions();
    }

    private Vector3[] getPositions()
    {
        int i;
        Rules[] rulesBox = FindObjectsOfType<Rules>();
        Vector3[] positions;

        positions = new Vector3[10];
        for (i = 0; i < rulesBox.Length; i++)
        {
            positions[i] = rulesBox[i].gameObject.transform.position;
        }
        return positions;
    }

    public void savePositions()
    {
        oldPositions = getPositions();
        oldScore = movesText.mouves;
        isSaved = true;
        saveBtn.text = "load previous save";
    }

    public void loadPosition(bool isFromSaved)
    {
        Vector3[] positions;
        int i;
        Rules[] rulesBox = FindObjectsOfType<Rules>();

        if (isFromSaved)
        {
            if (!isSaved)
            {
                savePositions();
            }
            else
            {
                positions = oldPositions;
                movesText.specificScore(oldScore);
                isSaved = false;
                saveBtn.text = "save";
                for (i = 0; i < rulesBox.Length; i++)
                {
                    rulesBox[i].gameObject.transform.position = positions[i];
                }
            }

        }
        else
        {
            movesText.specificScore(0);
            positions = startPositions;
            for (i = 0; i < rulesBox.Length; i++)
            {
                rulesBox[i].gameObject.transform.position = positions[i];
            }
        }



    }
}
