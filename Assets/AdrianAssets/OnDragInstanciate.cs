﻿using UnityEngine;
using System.Collections;

public class OnDragInstanciate : MonoBehaviour
{
    public GameObject Prefab;
    public int InstantiateType;
    private string[] InstantiateTypeNames = { "Mine", "Scene" };
    public int wall_amount = 20;

    private float xstep = 1.732f;
    private float zstep = 1.5f;

    public bool showGui;

    void OnClick()
    {
        if (!PhotonNetwork.inRoom)
        {
            // only use PhotonNetwork.Instantiate while in a room.
            return;
        }

        switch (InstantiateType)
        {
            case 0:
                Vector3 wallposition = GetSpawnPosition(InputToEvent.inputHitPos.x, InputToEvent.inputHitPos.z);

                //instanciate walls aslong as there are fewer than 10 walls in the scene
                GameObject[] gol = GameObject.FindGameObjectsWithTag("Wall");
                if (gol.Length < wall_amount)
                {
                    PhotonNetwork.Instantiate(Prefab.name, wallposition + new Vector3(0, 15f, 0), Quaternion.identity, 0);
                }
                break;
            case 1:
                PhotonNetwork.InstantiateSceneObject(Prefab.name, InputToEvent.inputHitPos + new Vector3(0, 5f, 0), Quaternion.identity, 0, null);
                break;
        }
    }

    void OnGUI()
    {
        if (showGui)
        {
            GUILayout.BeginArea(new Rect(Screen.width - 180, 0, 180, 50));
            InstantiateType = GUILayout.Toolbar(InstantiateType, InstantiateTypeNames);
            GUILayout.EndArea();
        }
    }

    // instanciate all wallpieces on top of the hexagonal tiles by calculating which tile index in x- and z-axis it should be placed at
    // http://stackoverflow.com/questions/7705228/hexagonal-grids-how-do-you-find-which-hexagon-a-point-is-in 
    // BUT ALOT OF THINGS CHANGED BECAUSE OF DIFFERENCES BETWEEN JAVA AND UNITY AXIS AND ORIGOS
    // start to check what square it is then check whether or not you clickposition is in any of the triangles overlapping the squares
    Vector3 GetSpawnPosition(float x, float z)
    {
        //shift zinput because origo is not at the correct position compared to the hexagongrid
        float shiftedinput_z = z - 0.25f;
        float x_index_float;
        float xindex;
        float z_index_float = shiftedinput_z / zstep;
        float zindex = Mathf.Round(z_index_float);
        // is the z position in an odd z-row 
        bool zIsOdd = (zindex % 2) != 0;
        if (zIsOdd)
        {
            //offset odd z-rows with half a xstep
            x_index_float = (x - (xstep * 0.5f)) / xstep;
            xindex = Mathf.Round(x_index_float);
        }
        else
        {
            x_index_float = x / xstep;
            xindex = Mathf.Round(x_index_float);
        }

        //check position of click relative to box it is in
        float relZ = shiftedinput_z - (zindex * zstep);
        float relX;
        if (zIsOdd)
        {
            relX = (x - (xindex * xstep)) - (xstep * 0.5f);
        }
        else
        {
            relX = x - (xindex * xstep);
        }

        //check if click position is above the top edges of the hexagon (meaning its inside the hexagon tile above)
        float c = 0.5f;
        float m = c / (xstep * 0.5f);
        if ((relZ - 0.25f) > ((m * relX) + c)) //left edge
        {
            Debug.Log("Topleft!");
            zindex += 1;
            if (!zIsOdd)
            {
                xindex -= 1;
            }
        }
        else if ((relZ - 0.25f) > (-m * relX) + c) //right edge
        {
            Debug.Log("Topright!");
            zindex += 1;
            if (zIsOdd)
            {
                xindex += 1;
            }
        }

        Vector3 spawnpos = new Vector3(xstep * xindex, 0, zstep * zindex);
        if (zIsOdd)
        {
            spawnpos = new Vector3((xstep * xindex) + (xstep * 0.5f), 0, zstep * zindex);
        }

        return spawnpos;
    }

}
