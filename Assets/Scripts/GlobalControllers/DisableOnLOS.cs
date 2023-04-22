using System.Collections.Generic;
using CommonProperties;
using UnityEngine;

namespace GlobalControllers
{
    public class DisableOnLOS : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        
        private RaycastHit hit;
        private GameObject disabled;
        
        [SerializeField]
        private Camera mainCamera;

        private Color originalColor;

        private List<GameObject> disabledWalls;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            disabledWalls = new List<GameObject>();
        }

        private void FixedUpdate()
        {
            
           Ray ray = new Ray(mainCamera.transform.position, player.transform.position - mainCamera.transform.position);
            
           RaycastHit[] hits = Physics.RaycastAll(ray);
        
           List<GameObject> hitWalls = new List<GameObject>();
           
           foreach (RaycastHit hit in hits)
           {
               Debug.Log(hit.transform.gameObject.name);
               GameObject wall = hit.transform.gameObject;
               if (wall.GetComponent<Turnable>())
               {
                   hitWalls.Add(wall);
                   if (!disabledWalls.Contains(wall))
                   {
                       disabledWalls.Add(wall);
                       DisableWall(wall);
                   }
               }
           }

           for (int i = 0; i < disabledWalls.Count; i++)
           {
               if (!hitWalls.Contains(disabledWalls[i]))
               {
                   EnableWall(disabledWalls[i]);
                   disabledWalls.Remove(disabledWalls[i]);
               }
           }

           /*if (Physics.Raycast(ray, out hit))
           {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<Turnable>())
                {
                    GameObject wall = hitObject;
                    if(disabled != null)
                        EnableWall(disabled);
                    DisableWall(wall);
                }
                else if (disabled != null)
                    EnableWall(disabled);
                
               /* if (wall != disabled)
                {
                    if (wall.GetComponent<Turnable>())
                    {
                        if (disabled != null)
                            EnableWall(wall);
                        DisableWall(wall);
                    }
                    else
                    {
                        if(disabled != null)
                            EnableWall(disabled);
                    } 
                }*/

            //}
        }

        private void EnableWall(GameObject wall)
        {
            
            List<MeshRenderer> WallMeshes = new List<MeshRenderer>();
            WallMeshes.Add(wall.GetComponent<MeshRenderer>());
            
            for (int i = 0; i < wall.transform.childCount; i++)
            {
                WallMeshes.Add(wall.transform.GetChild(i).GetComponent<MeshRenderer>());
            }
            
            foreach (MeshRenderer WallMesh in WallMeshes)
            {
                WallMesh.enabled = true;
            }
            //wall.gameObject.SetActive(true);
            //wall.GetComponent<MeshRenderer>().material.color = originalColor;
            disabled = null;
        }

        private void DisableWall(GameObject wall)
        {
            disabled = wall;
            List<MeshRenderer> WallMeshes = new List<MeshRenderer>();
            WallMeshes.Add(wall.GetComponent<MeshRenderer>());
            
            for (int i = 0; i < wall.transform.childCount; i++)
            {
                WallMeshes.Add(wall.transform.GetChild(i).GetComponent<MeshRenderer>());
            }
            
            foreach (MeshRenderer WallMesh in WallMeshes)
            {
                WallMesh.enabled = false;
            }
            //wall.gameObject.SetActive(false);
            //Material originalMat = wall.GetComponent<MeshRenderer>().material;
            //originalColor = originalMat.color;
            //Color disabledColor = new Color(originalColor.r, originalColor.g, originalColor.b, opacity / 255.0f);
            //originalMat.color = disabledColor;
        }

        private void OnDrawGizmos()
        {
            if(player != null)
                Gizmos.DrawLine(mainCamera.transform.position, player.transform.position);
        }
    }
}