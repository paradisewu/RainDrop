using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.EventSystems;

using UnityEngine.Events;

public class DrawObject
{

    private bool isAvailable;
    public DrawObject()
    {
        
        Reset();
    }

    protected void Reset()
    {

        drawColor = Color.red;

        isAvailable = true;
    }

    protected Vector3 pos;

    public Vector3 Pos
    {
        get
        {
            return pos;
        }

        set
        {
            pos = value;
        }
    }


    public Color DrawColor
    {
        get
        {
            return drawColor;
        }

        set
        {
            drawColor = value;
        }
    }

    public bool IsAvailable
    {
        get
        {
            return isAvailable;
        }

        set
        {
            isAvailable = value;
        }
    }

    protected Color drawColor;



    public virtual void update()
    { }

    public virtual void Draw()
    { }


    public virtual bool IsCollision(Vector3 inputPos, float radus)
    {
        return false;
    }

}

public class RainLine  : DrawObject
{



    public Vector3 RelationPos
    {
        get
        {
            return relationPos ;
        }

        set
        {
            relationPos = value;
        }
    }




    private Vector3 relationPos;

    private float maxSpeedx;

    private float speedX;

    private float randoSpeed;


    public UnityAction<Vector3> callBack = null;


    public RainLine():base()
    {

    }

    public void Reset(Vector3 tmpBein, Vector2 inputPos)
    {
        base.Reset();
        Initial(tmpBein);

        ReflushPos(inputPos);
    }

   // private Vector3 beginPos;
    public RainLine(Vector3  tmpBein,Vector2 inputPos):base()
    {

       
       
        Initial(tmpBein);

        ReflushPos(inputPos);
    }

    public void Initial(Vector3  tmpBegin)
    {
        pos = tmpBegin;

        speedX = 0;

        float tmp = -Random.Range(10, 100) * 0.001f * 0.5f;
         relationPos = new Vector3(tmp, tmp, 0);


        randoSpeed = -Random.Range(5,10)*0.1f;


    }

    public void ReflushPos(Vector2  inputPos)
    {
        maxSpeedx = (inputPos.x -   0.25f) / ( 0.5f);

        speedX = speedX + (maxSpeedx -speedX) ;

       

    }


    public override bool IsCollision(Vector3 inputPos ,float  radus)
    {
        // return base.IsCollision(inputPos);


        if (!this.IsAvailable)
            return true;



        float tmpDistance = Vector3.Distance(inputPos, this.pos+relationPos);


        if (tmpDistance < radus + this.relationPos.sqrMagnitude)
        {

            this.IsAvailable = false;
        }




        return  this.IsAvailable;

    }


    public Vector3 GetCollisionPos()
    {

        return pos + RelationPos;
    }


   

    public override void update()
    {

        if (!this.IsAvailable)
            return;

        pos.x = pos.x + randoSpeed * speedX*Time.deltaTime;
        pos.y = pos.y +randoSpeed * Time.deltaTime;




        if (this.pos.y < 0.1f)
        {
            if(callBack != null)
            callBack(this.pos + relationPos);

            this.IsAvailable = false;
        }


    }

    public  override void Draw()
    {

        if (!this.IsAvailable)
            return;

        GL.Color(drawColor);

        GL.Begin(GL.LINES);

        GL.Vertex(pos);

        //Debug.Log("pos=="+pos);


       // Debug.Log("RelationPos==" + RelationPos);
        GL.Vertex(pos + RelationPos);

         
        GL.End();
    }




}


public class RainDrop :DrawObject
{

    float radius;

    Vector2 speed = Vector2.zero;

    public RainDrop():base()
    {


    }

    public void DrawFixedCircle(Vector3 beginPos,float tmpRadius )
    {

        this.pos = beginPos;

        radius = tmpRadius;

    }

    public void Reset(Vector3 beginPos)
    {
        base.Reset();

        this.pos = beginPos;

        speed.x = Random.Range(5, 10) * 0.1f;

        speed.y = -Random.Range(5, 20) * 0.01f;


        radius = Random.Range(1, 100) * 0.00015f;
    }


    public RainDrop( Vector3  beginPos)
    {

        Reset(beginPos);
    }


    int numbers = 100;

    Vector3 tmpPos;

    float tmpAngle;


    float gravity = 9.8f;
    public override void update()
    {

        if (! this.IsAvailable)
            return;

        this.pos.x = this.pos.x - speed.x * 0.8f*Time.deltaTime;


        speed.y = speed.y + gravity*Time.deltaTime;

        this.pos.y = this.pos.y - speed.y *Time.deltaTime ;


        if (this.pos.y < -0.1f)
        {
            this.IsAvailable = false;
        }


    }
    public override void Draw()
    {

        if (!IsAvailable)
            return;



        GL.Color(drawColor);

        GL.Begin(GL.LINES);



        for (int i = 1; i < numbers; i++)
        {
            tmpAngle = 2 * Mathf.PI *( i-1) / numbers;

            tmpPos.x = radius* Mathf.Cos(tmpAngle);

            tmpPos.y = radius * Mathf.Sin(tmpAngle);
            GL.Vertex(tmpPos +this.pos);



            tmpAngle = 2 * Mathf.PI * i / numbers;

            tmpPos.x = radius * Mathf.Cos(tmpAngle);

            tmpPos.y = radius * Mathf.Sin(tmpAngle);
            GL.Vertex(tmpPos+ this.pos);


          
        }

       

       


        GL.End();



    }


}