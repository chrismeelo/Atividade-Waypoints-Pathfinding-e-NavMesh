using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { UNI, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    //Array onde ficam os pontos
    public GameObject[] waypoints;

    //Array onde fica os links dos pontos
    public Link[] links;

    //Variavel que instancia o graph no script
    public Graph graph = new Graph();


    void Start()
    {
        //Verifica se no waypoint tem gameobjetcs
        if (waypoints.Length > 0)
        {
            //para cada objeto no array de waypoints
            foreach (GameObject wp in waypoints)
            {
                //faz a adicão de um nó
                graph.AddNode(wp);
            }

            //para cada GameObject no array dos l inks
            foreach (Link l in links)
            {
                //faz a adição de um edge
                graph.AddEdge(l.node1, l.node2);

                //verifica se a direção é igual a BI
                if (l.dir == Link.direction.BI)
                    //Adiciona uma Edge
                    graph.AddEdge(l.node2, l.node1);
            }
        }
    }

    void Update()
    {
        graph.debugDraw();
    }
}

