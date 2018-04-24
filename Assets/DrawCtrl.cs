using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCtrl : MonoBehaviour {


    DrawObjectManager manager;
	// Use this for initialization
	void Start () {

        manager = new DrawObjectManager();


        CreateLineMaterial();
    }


    static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }


    public void OnRenderObject()
    {
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
       // GL.MultMatrix(transform.localToWorldMatrix);

        GL.LoadOrtho();

        manager.Draw();


        ////GL.Begin(GL.LINES);

        //GL.Vertex(Vector2.zero);

        //GL.Vertex(Vector2.one *0.5f);


        //GL.End();


        //GL.Begin(GL.LINES);

        //GL.Vertex(Vector2.one*0.5f);

        //GL.Vertex(Vector2.one * 0.7f);


        //GL.End();





        GL.PopMatrix();




    }


    // Update is called once per frame
    void Update () {


        if (Input.GetKey(KeyCode.A))
        {
            manager.GenOneRainLine();
        }

        if (Input.GetKey(KeyCode.B))
        {
            manager.GenOneCircle();
        }

        manager.RandomGenLine();
        manager.InputGenDrop();

        manager.DrawMouse();

       manager.Update();

    }
}
