

public struct MatrixBox
{

    public float[] x;
    public float[] y;

    public void init(float x1, float x2, float y1, float y2)
    {
        x = new float[2];
        y = new float[2];

        x[0] = x1;
        x[1] = x2;
        y[0] = y1;
        y[1] = y2;
    }


    public static bool isIntersecting(MatrixBox a, MatrixBox b)
    {
        if (isIntersectingInX(a, b) && isIntersectingInY(a, b))
        {
            return true;
        }
        return false;
    }

    private static bool isIntersectingInX(MatrixBox a, MatrixBox b)
    {
        if(a.x[0] >= b.x[0])
        {
            if(a.x[0] <= b.x[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(b.x[0] <= a.x[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private static bool isIntersectingInY(MatrixBox a, MatrixBox b)
    {
        if (a.y[0] >= b.y[0])
        {
            if (a.y[0] <= b.y[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (b.y[0] <= a.y[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
}

