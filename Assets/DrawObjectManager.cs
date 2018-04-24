using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawObjectManager  {



    public DrawObjectManager()
    {
        allLines = new List<DrawObject>();
        allCircle = new List<DrawObject>();


        mouseCircle = new RainDrop();
    }


    RainDrop mouseCircle;
    List<DrawObject> allLines;

    public DrawObject GetFreeRainLine()
    {

        for (int i = 0; i < allLines.Count; i++)
        {

            if (!allLines[i].IsAvailable)
                return allLines[i];

        }

        RainLine tmpLine = new RainLine();
        allLines.Add(tmpLine);

        return tmpLine;



    }

    public DrawObject GetFreeRainDrop()
    {

        for (int i = 0; i < allCircle.Count; i++)
        {

            if (!allCircle[i].IsAvailable)
                return allCircle[i];

        }

        RainDrop tmpLine = new RainDrop();
        allCircle.Add(tmpLine);

        return tmpLine;



    }



    public void  GenOneRainLine()
    {

        Vector3 tmpPos = Vector3.zero;

        tmpPos.x =  Random.Range(0,1.0f);

        tmpPos.y = 1;

        Vector2 direct = Vector2.zero;

        direct.x =  0.5f;

        RainLine tmpLine = (RainLine)GetFreeRainLine();
        tmpLine.Reset(tmpPos,direct);

        tmpLine.callBack = new UnityEngine.Events.UnityAction<Vector3>(RainWillDisappear);


     //   Debug.Log("line Count==");


    }


    List<DrawObject> allCircle;

    public void GenOneCircle(Vector3  tmpPos)
    {

       

        tmpPos.x *= Random.Range(0, 100) * 0.01f;

        RainDrop tmpDrop = (RainDrop)GetFreeRainDrop();

        tmpDrop.Reset(tmpPos);



    }
   public  void GenOneCircle()
    {

        Vector3 tmpPos = Vector3.zero;

        tmpPos.x = Random.Range(0, 100)*0.01f;

        tmpPos.y = 0.5f;



        RainDrop tmpDrop = (RainDrop)GetFreeRainDrop();

        tmpDrop.Reset(tmpPos);

      //  RainDrop tmpDrop = new RainDrop(tmpPos);

        //allCircle.Add(tmpDrop);

    }




    public void RandomGenLine()
    {

    

        GenOneRainLine();

    }

    public void DrawMouse()
    {

        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mouseCircle.DrawFixedCircle(viewPos,0.05f);
    }

    public void InputGenDrop()
    {


        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

       // Debug.Log("view Pos=="+viewPos);

        for (int i = 0; i < allLines.Count; i++)
        {

            if (!allLines[i].IsCollision(viewPos,0.1f))
            {
                
                int tmpNum = Random.Range(3,6);

                Vector3 tmpPos = ((RainLine)allLines[i]).GetCollisionPos();

                for (int j = 0; j < tmpNum; j++)
                {

                    GenOneCircle(tmpPos);
                }
            }
        }
    }


    public void RainWillDisappear(Vector3  tmpPos)
    {
        GenOneCircle(tmpPos);
    }

    public void Draw()
    {


        for (int i = 0; i < allCircle.Count; i++)
        {

            allCircle[i].Draw();
        }


        for (int i = 0; i < allLines.Count; i++)
        {

            allLines[i].Draw();
        }

        if (mouseCircle != null)
            mouseCircle.Draw();

    }

    public void Update()
    {


        for (int i = 0; i < allCircle.Count; i++)
        {

            allCircle[i].update();
        }


        for (int i = 0; i < allLines.Count; i++)
        {

            allLines[i].update();
        }


    }


	
}
