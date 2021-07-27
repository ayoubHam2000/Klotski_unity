
using UnityEngine;

public class Rules : MonoBehaviour
{

    [SerializeField] GameObject newtestBox = null;
    private struct Bound
    {
        public int left;
        public int right;
        public int top;
        public int bottom;
    };



    private int breakUnit = 2;
    private float redus = 0;
    private bool onClicked = false;
    private Camera cam = null;

    private int nbrBoxs = 15;
    private Transform[] boxs = null;
    private MatrixBox[] matrixBoxs = null;
    private MatrixBox thisMatrixBox;
    private Transform[] walls = null;

    private Vector3 mousPos;
    private Bound bound;

    private GameObject testBox = null;
    private TestBox testBoxCom = null;
    private Mouves textMouves = null;

    void Start()
    {
        cam = Camera.main;
        walls = FindObjectOfType<Setting>().walls;

        initTestBox();
        getOtherBoxes();
        getBoxMatrix();
        initBound();
        
        textMouves = FindObjectOfType<Mouves>();


    }

    private void initTestBox()
    {
        testBox = Instantiate(newtestBox, transform.position, Quaternion.identity);
        testBox.transform.localScale = transform.localScale;
        testBox.SetActive(false);
        testBoxCom = testBox.GetComponent<TestBox>();
    }

    private void initBound()
    {
        int div = 2;
        bound.left = (int)transform.localScale.x / div;
        bound.right = - (int)transform.localScale.x / div;
        bound.top = (int)transform.localScale.y / div;
        bound.bottom = - (int)transform.localScale.y / div;
    }

    private void getOtherBoxes()
    {
        int i;
        int j;
        Rules[] rulesBox = FindObjectsOfType<Rules>();
        boxs = new Transform[nbrBoxs];
        j = 0;

        for (i = 0; i < rulesBox.Length; i++)
        {
            if(rulesBox[i].transform != transform)
            {
                boxs[j] = rulesBox[i].gameObject.transform;
                j++;
            }
        }

        for(i = 0; i < walls.Length; i++)
        {
            boxs[j] = walls[i];
            j++;
        }
    }

    private void getBoxMatrix()
    {
        int i;

        matrixBoxs = new MatrixBox[nbrBoxs];
        
        for (i = 0; i < nbrBoxs; i++)
        {
            Transform theBox = boxs[i];

            float x1 = theBox.position.x - theBox.localScale.x/2 - redus;
            float x2 = theBox.position.x + theBox.localScale.x/2 + redus;
            float y1 = theBox.position.y - theBox.localScale.y/2 - redus;
            float y2 = theBox.position.y + theBox.localScale.y/2 + redus;
            matrixBoxs[i].init(x1, x2, y1, y2);
        }
        getThisMatrixBox();
    }

    private void getThisMatrixBox()
    {
        Vector3 tr = testBox.transform.position;
        float x1 = tr.x - transform.localScale.x / 2 - redus;
        float x2 = tr.x + transform.localScale.x / 2 + redus;
        float y1 = tr.y - transform.localScale.y / 2 - redus;
        float y2 = tr.y + transform.localScale.y / 2 + redus;

        //Debug.Log(name + x1 +"," + x2 +"," + y1 +"," + y2);
        thisMatrixBox.init(x1, x2, y1, y2);
    }
    // Update is called once per frame
    void Update()
    {
        if (onClicked) {
            mousPos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouve();
        }
    }

    private void mouve()
    {
        Vector3 thePos = transform.position;
        Vector3 diffrentPos = mousPos - thePos;

        //Debug.Log(diffrentPos);
        if (diffrentPos.x > bound.left && Mathf.Abs(diffrentPos.x)  > Mathf.Abs(diffrentPos.y) )
        {
            thePos.x += breakUnit;
        }else if (diffrentPos.x  < bound.right && Mathf.Abs(diffrentPos.x)  > Mathf.Abs(diffrentPos.y) )
        {
            thePos.x -= breakUnit;
        }else if (diffrentPos.y> bound.top)
        {
            thePos.y += breakUnit;
        }
        else if(diffrentPos.y < bound.bottom)
        {
            thePos.y -= breakUnit;
        }

        testBox.transform.position = thePos;
    }




    //------------------------------------------------------------------
    //detect mouse click
    private void OnMouseDown()
    {
       // Debug.Log("OnMouseDown");
        onClicked = true;
        testBox.SetActive(true);
    }

    private void OnMouseUp()
    {
        //Debug.Log("OnMouseUp");
        onClicked = false;
        mouveAllowed();
        testBox.SetActive(false);
    }

    private void mouveAllowed()
    {
        if (isAllowed())
        {
            textMouves.changeText();
            transform.position = testBox.transform.position;
            getThisMatrixBox();
        }
    }

    private bool isAllowed()
    {
        int i;
        getBoxMatrix();
        for (i = 0; i < matrixBoxs.Length; i++)
        {
            if(MatrixBox.isIntersecting(thisMatrixBox, matrixBoxs[i]))
            {
                //Debug.Log(matrixBoxs[i].x[0] + "," + matrixBoxs[i].x[1]);
                //Debug.Log(matrixBoxs[i].y[0] + "," + matrixBoxs[i].y[1]);
                return false;
            }
        }
        return true;
    }


}
